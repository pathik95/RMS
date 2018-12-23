using RepairTrackerBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RMS.Service.Controllers
{

    public class DashboardController : ApiController
    {
        private DashboardBL dashboardBL;

        /// <summary>
        /// Public contructor
        /// </summary>
        public DashboardController()
        {
            dashboardBL = new DashboardBL();
        }

        /// <summary>
        /// To get list of requests updated by logged in user 
        /// </summary>
        /// <param name="userId">logged in user id</param>
        /// <param name="pageNumber"> current page number </param>
        /// <returns>request list along with total count </returns>
        public IHttpActionResult GetMyRequests(int userId, int pageNumber)
        {
            var myRequests = dashboardBL.GetMyAttendedRepairRequest(userId, pageNumber);
            return Json(myRequests);
        }

        /// <summary>
        /// To get list of all pending requests 
        /// </summary>
        /// <param name="pageNumber">current page number</param>
        /// <returns>request list along with total count</returns>
        public IHttpActionResult GetPendingRequests(int pageNumber)
        {
            var pendingRequests = dashboardBL.GetPendingRequests(pageNumber);
            return Json(pendingRequests);
        }


        /// <summary>
        /// To get list of all completed requests 
        /// </summary>
        /// <param name="pageNumber">current page number</param>
        /// <returns>request list along with total count</returns>
        public IHttpActionResult GetCompletedRequests(int pageNumber)
        {
            var completedRequests = dashboardBL.GetCompletedRequests(pageNumber);
            return Json(completedRequests);
        }

        /// <summary>
        /// To get list of all approved requests 
        /// </summary>
        /// <param name="pageNumber">current page number</param>
        /// <returns>request list along with total count</returns>
        public IHttpActionResult GetApprovedRequests(int pageNumber)
        {
            var approvedRequests = dashboardBL.GetApprovedRequests(pageNumber);
            return Json(approvedRequests);
        }
    }
}
