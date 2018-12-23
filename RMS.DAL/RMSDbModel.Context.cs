﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMS.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class RMSDbContext : DbContext
    {
        public RMSDbContext()
            : base("name=RMSDbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<RoleMaster> RoleMaster { get; set; }
        public virtual DbSet<UserMaster> UserMaster { get; set; }
        public virtual DbSet<ErrorInfo> ErrorInfo { get; set; }
        public virtual DbSet<RequestStatusChangeLog> RequestStatusChangeLog { get; set; }
        public virtual DbSet<RequestStatusMaster> RequestStatusMaster { get; set; }
        public virtual DbSet<CityMaster> CityMaster { get; set; }
        public virtual DbSet<StateMaster> StateMaster { get; set; }
        public virtual DbSet<RepairRequestDetails> RepairRequestDetails { get; set; }
    
        public virtual int USP_AddUser(string userName, string password, string displayName, string emailId, string roleId)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var displayNameParameter = displayName != null ?
                new ObjectParameter("displayName", displayName) :
                new ObjectParameter("displayName", typeof(string));
    
            var emailIdParameter = emailId != null ?
                new ObjectParameter("emailId", emailId) :
                new ObjectParameter("emailId", typeof(string));
    
            var roleIdParameter = roleId != null ?
                new ObjectParameter("roleId", roleId) :
                new ObjectParameter("roleId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("USP_AddUser", userNameParameter, passwordParameter, displayNameParameter, emailIdParameter, roleIdParameter);
        }
    
        public virtual ObjectResult<USP_AuthenticateUser_Result> USP_AuthenticateUser(string userName, string password)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_AuthenticateUser_Result>("USP_AuthenticateUser", userNameParameter, passwordParameter);
        }
    
        public virtual ObjectResult<USP_GetRequestDetails_Result> USP_GetRequestDetails(Nullable<int> requestId)
        {
            var requestIdParameter = requestId.HasValue ?
                new ObjectParameter("requestId", requestId) :
                new ObjectParameter("requestId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_GetRequestDetails_Result>("USP_GetRequestDetails", requestIdParameter);
        }
    
        public virtual ObjectResult<USP_GetRequestStatusChangeLog_Result> USP_GetRequestStatusChangeLog(Nullable<int> requestId)
        {
            var requestIdParameter = requestId.HasValue ?
                new ObjectParameter("requestId", requestId) :
                new ObjectParameter("requestId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_GetRequestStatusChangeLog_Result>("USP_GetRequestStatusChangeLog", requestIdParameter);
        }
    
        public virtual ObjectResult<USP_GetRequestsForDashboard_Result> USP_GetRequestsForDashboard(Nullable<int> userId, Nullable<int> statusId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var statusIdParameter = statusId.HasValue ?
                new ObjectParameter("statusId", statusId) :
                new ObjectParameter("statusId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_GetRequestsForDashboard_Result>("USP_GetRequestsForDashboard", userIdParameter, statusIdParameter);
        }
    
        public virtual ObjectResult<USP_GetRequestCountByStatus_Result> USP_GetRequestCountByStatus(Nullable<int> statusId)
        {
            var statusIdParameter = statusId.HasValue ?
                new ObjectParameter("statusId", statusId) :
                new ObjectParameter("statusId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_GetRequestCountByStatus_Result>("USP_GetRequestCountByStatus", statusIdParameter);
        }
    
        public virtual ObjectResult<USP_GetRequestStartEndDate_Result> USP_GetRequestStartEndDate()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_GetRequestStartEndDate_Result>("USP_GetRequestStartEndDate");
        }
    
        public virtual ObjectResult<USP_GetMyRequsetCountForReport_Result> USP_GetMyRequsetCountForReport(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_GetMyRequsetCountForReport_Result>("USP_GetMyRequsetCountForReport", userIdParameter);
        }
    }
}