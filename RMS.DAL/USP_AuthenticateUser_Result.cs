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
    
    public partial class USP_AuthenticateUser_Result
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public System.Guid SecurityToken { get; set; }
        public string DisplayName { get; set; }
    }
}
