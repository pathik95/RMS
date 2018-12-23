using RMS.DAL;
using RMS.Utilities;
using RMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BAL
{
    public class RepairRequestBL
    {
        /// <summary>
        /// To create new repair request in database
        /// </summary>
        /// <param name="repairRequestDetails">details of request</param>
        public void CreateNewRepairRequest(CreateRequestViewModel repairRequestDetails)
        {
            using (RMSDbContext context = new RMSDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var requestDetails = new RepairRequestDetails()
                        {
                            CompanyName = repairRequestDetails.CompanyName,
                            CreatedBy = repairRequestDetails.CreatedBy,
                            CreatedOn = DateTime.Now,
                            CustomerContact = repairRequestDetails.CustomerContact,
                            CustomerName = repairRequestDetails.CustomerName,
                            ModelNumber = repairRequestDetails.ModelNumber,
                            ProblemDetails = repairRequestDetails.ProblemDetails,
                            StatusId = (int)StatusEnum.Pending,
                            CityId = repairRequestDetails.CityId,
                            CustomerAddress = repairRequestDetails.CustomerAddress,
                            CustomerPinCode = repairRequestDetails.PinCode,
                            Expense = Convert.ToDecimal(repairRequestDetails.Expense),
                            StateId = repairRequestDetails.StateId,

                        };
                        context.RepairRequestDetails.Add(requestDetails);
                        UpdateStatusLog(context, requestDetails.RequestId, requestDetails.StatusId, repairRequestDetails.CreatedBy, null);

                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// To get request details from database
        /// </summary>
        /// <param name="requestId">id of request</param>
        /// <returns>details of the request</returns>
        public RequestDetailsViewModel GetRequestDetails(int requestId)
        {
            using (RMSDbContext context = new RMSDbContext())
            {
                var requestDetails = context.USP_GetRequestDetails(requestId).Select(s => new RequestDetailsViewModel()
                {
                    CompanyName = s.CompanyName,
                    CreatedBy = s.DisplayName,
                    CreatedOn = s.CreatedOn.ToString(Constants.DATE_FORMAT),
                    CustomerContact = s.CustomerContact,
                    CustomerName = s.CustomerName,
                    ModelNumber = s.ModelNumber,
                    ProblemDetails = s.ProblemDetails,
                    RequestId = s.RequestId,
                    Status = s.StatusName,
                    StatusId = s.StatusId ?? 0,
                    CustomerAddress = s.FullAddress,
                    Expense = s.Expense
                }).SingleOrDefault();

                if (requestDetails != null)
                {
                    requestDetails.RequestChangeLog = context.USP_GetRequestStatusChangeLog(requestId).Select(cl =>
                    new RequestStatusLogViewModel()
                    {
                        Comments = cl.Comments,
                        ModifiedBy = cl.ModifiedBy,
                        ModifiedOn = cl.ModifiedOn.ToString(Constants.DATE_TIME_FORMAT),
                        UpdatedStatus = cl.StatusName
                    }).ToList();
                }

                return requestDetails;
            }
        }


        /// <summary>
        /// To update the request status as completed
        /// </summary>
        /// <param name="updateRequestStatusViewModel">details required to update status</param>
        public void CompleteRrquest(UpdateRequestStatusViewModel updateRequestStatusViewModel)
        {
            UpdateStatus(updateRequestStatusViewModel, (int)StatusEnum.Completed);
        }

        /// <summary>
        /// To approve or reject  request completed by Technician
        /// /// </summary>
        /// <param name="approvalViewModal">approval details </param>
        public void ApproveOrRejectRequest(ApprovalViewModel approvalViewModal)
        {
            if (approvalViewModal.IsApproved)
                UpdateStatus(approvalViewModal, (int)StatusEnum.Approved);
            else
                UpdateStatus(approvalViewModal, (int)StatusEnum.Pending, true);
        }

        /// <summary>
        /// To close requests approved by Quality Check team.
        /// </summary>
        /// <param name="updateRequestStatusViewModel">details required to update status</param>
        public void CloseRequest(UpdateRequestStatusViewModel updateRequestStatusViewModel)
        {
            UpdateStatus(updateRequestStatusViewModel, (int)StatusEnum.Closed);
        }

        /// <summary>
        /// Method to get cities list based on state id
        /// </summary>
        /// <returns></returns>
        public List<CityDropDownViewModel> GetCities(int stateId)
        {
            using (RMSDbContext context = new RMSDbContext())
            {
                var cityList = context.CityMaster.Where(c => c.StateId == stateId).Select(c => new CityDropDownViewModel()
                {
                    CityId = c.CityId,
                    StateId = c.StateId,
                    CityName = c.CityName
                }).ToList();

                return cityList;
            };
        }

        /// <summary>
        /// To get states list from db
        /// </summary>
        /// <returns></returns>
        public List<StateDropDownViewModel> GetStates()
        {
            using (RMSDbContext context = new RMSDbContext())
            {
                var stateList = context.StateMaster.Select(s => new StateDropDownViewModel()
                {
                    StateId = s.StateId,
                    StateName = s.StateName
                }).ToList();

                return stateList;
            }
        }

        //Common method to update status 
        private void UpdateStatus(UpdateRequestStatusViewModel updateRequestStatusViewModel, int statusIdToUpdate, bool allowPreviousStatus = false)
        {
            var updateStatusResponse = new UpdateStatusResponseViewModel();
            using (RMSDbContext context = new RMSDbContext())
            {
                var requestDetails = context.RepairRequestDetails.Where(s => s.RequestId == updateRequestStatusViewModel.RequestId).Single();
                if (!allowPreviousStatus && requestDetails.StatusId >= statusIdToUpdate)
                {
                    throw new Exception("Status of the request " + requestDetails.RequestId + " is already moved forward.");
                }
                requestDetails.StatusId = statusIdToUpdate;
                UpdateStatusLog(context, requestDetails.RequestId, statusIdToUpdate, updateRequestStatusViewModel.UserId, updateRequestStatusViewModel.Comment);
                context.SaveChanges();
            }
        }

        //insert log of status update in database
        private void UpdateStatusLog(RMSDbContext context, int requestId, int statusId, int modifiedBy, string comments)
        {
            var statusChangeLog = new RequestStatusChangeLog()
            {
                ModifiedBy = modifiedBy,
                ModifiedOn = DateTime.Now,
                RequestId = requestId,
                StatusId = statusId,
                Comments = comments
            };
            context.RequestStatusChangeLog.Add(statusChangeLog);
        }
    }
}
