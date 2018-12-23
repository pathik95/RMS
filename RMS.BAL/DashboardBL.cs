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
    public class DashboardBL
    {
        /// <summary>
        /// To get request list attended by logged in user
        /// </summary>
        /// <param name="userId">id of logged in user</param>
        /// <param name="pageNumber">page number </param>
        /// <returns>request list</returns>
        public DashboardViewModel GetMyAttendedRepairRequest(int userId, int pageNumber)
        {
            using (RMSDbContext context = new RMSDbContext())
            {
                var requestList = GetRequestListForDashboar(context, pageNumber, userId, null);
                return requestList;
            }
        }

        /// <summary>
        /// To get pending requests list 
        /// </summary>
        /// <param name="pageNumber">pagenumber</param>
        /// <returns>request list</returns>
        public DashboardViewModel GetPendingRequests(int pageNumber)
        {
            using (RMSDbContext context = new RMSDbContext())
            {
                var requestList = GetRequestListForDashboar(context, pageNumber, null, (int)StatusEnum.Pending);
                return requestList;
            }
        }

        /// <summary>
        /// To get completed requests list
        /// </summary>
        /// <param name="pageNumber">[page number</param>
        /// <returns>request list</returns>
        public DashboardViewModel GetCompletedRequests(int pageNumber)
        {
            using (RMSDbContext context = new RMSDbContext())
            {
                var requestList = GetRequestListForDashboar(context, pageNumber, null, (int)StatusEnum.Completed);
                return requestList;
            }
        }

        /// <summary>
        /// To get approved requests list
        /// </summary>
        /// <param name="pageNumber">page number</param>
        /// <returns></returns>
        public DashboardViewModel GetApprovedRequests(int pageNumber)
        {
            using (RMSDbContext context = new RMSDbContext())
            {
                var requestList = GetRequestListForDashboar(context, pageNumber, null, (int)StatusEnum.Approved);
                return requestList;
            }
        }

        private DashboardViewModel GetRequestListForDashboar(RMSDbContext context, int pageNumber, int? userId, int? statusId)
        {
            var pageSize = Constants.ITEMS_PER_PAGE;
            var requestList = context.USP_GetRequestsForDashboard(userId, statusId).
                    Select(s => new RequestDetailsForDashboardViewModel()
                    {
                        CompanyName = s.CompanyName,
                        CreatedOn = s.CreatedOn.ToString(Constants.DATE_FORMAT),
                        CustomerName = s.CustomerName,
                        ModelNumber = s.ModelNumber,
                        RequestId = s.RequestId,
                        Status = s.StatusName
                    }).ToList();

            var dashboardViewModel = new DashboardViewModel()
            {
                RequestsList = requestList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                TotalRequestsCount = requestList.Count,
                ItemsPerPage = pageSize
            };

            return dashboardViewModel;
        }
    }
}
