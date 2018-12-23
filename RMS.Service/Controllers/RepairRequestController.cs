using RMS.BAL;
using RMS.Utilities;
using RMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace RMS.Service.Controllers
{
    public class RepairRequestController : ApiController
    {
        private RepairRequestBL repairRequestBL;
        /// <summary>
        /// Public Contractor 
        /// </summary>
        public RepairRequestController()
        {
            repairRequestBL = new RepairRequestBL();
        }

        /// <summary>
        /// To create new repair request in db
        /// </summary>
        /// <param name="requestViewModel">details of the request</param>
        /// <returns>Ok result</returns>
        /// 
        [Authorize(Roles = Constants.CLERK_ROLE)]
        [ActionName("Create")]
        [HttpPost]
        public IHttpActionResult CreateRequest(CreateRequestViewModel requestViewModel)
        {
            repairRequestBL.CreateNewRepairRequest(requestViewModel);
            return Ok();
        }

        /// <summary>
        /// Method to get request details 
        /// </summary>
        /// <param name="requestId">id of the request</param>
        /// <returns>json object with request details</returns>
        [ActionName("Details")]
        [HttpGet]
        public IHttpActionResult GetRequestDetails(int requestId)
        {
            var requestDetails = repairRequestBL.GetRequestDetails(requestId);
            if (requestDetails != null)
                return Json(requestDetails);
            else
                return NotFound();
        }

        /// <summary>
        /// To mark repair request as complete. 
        /// </summary>
        /// <param name="updateRequestStatusViewModel">request details</param>
        /// <returns>ok result</returns>
        [Authorize(Roles = Constants.TECHNICIAN_ROLE)]
        [HttpPut]
        public IHttpActionResult CompleteRequest(UpdateRequestStatusViewModel updateRequestStatusViewModel)
        {
            repairRequestBL.CompleteRrquest(updateRequestStatusViewModel);
            return Ok();
        }
        /// <summary>
        /// To approve or reject completed request.
        /// </summary>
        /// <param name="approvalViewModel">approval details</param>
        /// <returns>Empty resposne with Status code 200</returns>
        [Authorize(Roles = Constants.QUALITY_CONTROL_ROLE)]
        [HttpPut]
        public IHttpActionResult ApproveorRejectRequest(ApprovalViewModel approvalViewModel)
        {
            repairRequestBL.ApproveOrRejectRequest(approvalViewModel);
            return Ok();
        }

        /// <summary>
        /// To close approved request.
        /// </summary>
        /// <param name="updateRequestStatusViewModel">reqeust details</param>
        /// <returns>ok result </returns>
        [Authorize(Roles = Constants.SHIPPING_ROLE)]
        [HttpPut]
        public IHttpActionResult CloseRequest(UpdateRequestStatusViewModel updateRequestStatusViewModel)
        {
            repairRequestBL.CloseRequest(updateRequestStatusViewModel);
            return Ok();
        }

        /// <summary>
        /// Method to get state list 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetStates()
        {
            var states = repairRequestBL.GetStates();
            return Json(states);
        }

        /// <summary>
        /// Method to get cityies based on selected state 
        /// </summary>
        /// <param name="stateId">id of the state selected</param>
        /// <returns></returns>     
        [HttpGet]
        public IHttpActionResult GetCities(int stateId)
        {
            var cities = repairRequestBL.GetCities(stateId);
            return Json(cities);
        }
    }
}
