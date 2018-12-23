using RMS.DAL;
using RMS.Utilities;
using RMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairTrackerBL
{
    public class ReportBL
    {
        /// <summary>
        /// To get necessary details to show graphs
        /// </summary>
        /// <param name="userId">id of logged in user</param>
        /// <returns>details required to show all 3 graphs</returns>
        public ReportViewModel GetReportDetails(int userId)
        {
            using (RMSDbContext context = new RMSDbContext())
            {
                var reportDetails = new ReportViewModel();
                reportDetails.AverageResponseTime = CalculateAvgResponseTime(context);

                //To get the request details created by logged in user
                var myRequestDetails = context.USP_GetMyRequsetCountForReport(userId)
                    .Select(s => new ChartItemViewModel
                    {
                        Lable = s.CreatedDate,
                        Value = s.RequestCount ?? 0
                    }).ToList();
                //Transfer request details into form required for Chart
                reportDetails.BarChartDetails = new ChartsViewModel()
                {
                    ChartData = myRequestDetails.Select(rd => rd.Value).ToList(),
                    ChartLable = myRequestDetails.Select(rd => rd.Lable).ToList()
                };

                var requestStatusCountDetails = new List<ChartItemViewModel>();
                var pendingItemDetails = GetRequestCountByStatus(context, (int)StatusEnum.Pending);
                requestStatusCountDetails.Add(pendingItemDetails);
                var completedItemDetails = GetRequestCountByStatus(context, (int)StatusEnum.Completed);
                requestStatusCountDetails.Add(completedItemDetails);
                var approvedItemDetails = GetRequestCountByStatus(context, (int)StatusEnum.Approved);
                requestStatusCountDetails.Add(approvedItemDetails);
                var closedItemDetails = GetRequestCountByStatus(context, (int)StatusEnum.Closed);
                requestStatusCountDetails.Add(closedItemDetails);

                //convert status - request count into form required for Chart
                reportDetails.DoughnutChartDetails = new ChartsViewModel()
                {
                    ChartData = requestStatusCountDetails.Select(s => s.Value).ToList(),
                    ChartLable = requestStatusCountDetails.Select(s => s.Lable).ToList()
                };

                return reportDetails;
            }
        }

        //To get average response time 
        private double? CalculateAvgResponseTime(RMSDbContext context)
        {
            double? averageResponseTime;
            var responseTimeList = context.USP_GetRequestStartEndDate().Select(r => (r.ClosedOn - r.CreatedOn).TotalDays).ToList();
            if (responseTimeList.Count > 0)
                averageResponseTime = responseTimeList.Sum() / responseTimeList.Count;
            else
                averageResponseTime = null;
            return averageResponseTime;
        }

        //to get individual request count based on input status id 
        private ChartItemViewModel GetRequestCountByStatus(RMSDbContext context, int statusId)
        {
            var requestCount = context.USP_GetRequestCountByStatus(statusId)
            .Select(s => new ChartItemViewModel()
            {
                Lable = s.StatusName,
                Value = s.RequestCount ?? 0
            }).SingleOrDefault();
            return requestCount;
        }
    }
}
