//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class RequestStatusChangeLog
    {
        public int LogId { get; set; }
        public int RequestId { get; set; }
        public int StatusId { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string Comments { get; set; }
    
        public virtual RequestStatusMaster RequestStatusMaster { get; set; }
        public virtual UserMaster UserMaster { get; set; }
        public virtual RepairRequestDetails RepairRequestDetails { get; set; }
    }
}
