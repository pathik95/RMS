using RepairTrackerBL;
using RMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RepairTracker.Controllers
{

    [Authorize(Roles = Constants.CLERK_ROLE)]
    public class ReportController : ApiController
    {
        private ReportBL reportBL;

        /// <summary>
        /// Public contructor
        /// </summary>
        public ReportController()
        {
            reportBL = new ReportBL();
        }

        /// <summary>
        /// To get details required to show graphs on reports page
        /// </summary>
        /// <param name="userId">id of logged in user</param>
        /// <returns></returns>
        [ActionName("Get")]
        public IHttpActionResult GetReportDetails(int userId)
        {
            var reportDetails = reportBL.GetReportDetails(userId);
            return Json(reportDetails);
        }

    }
}
