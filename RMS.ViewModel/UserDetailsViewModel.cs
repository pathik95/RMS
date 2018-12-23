using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ViewModel
{
    public class UserDetailsViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string RoleName { get; set; }
        public string EmailId { get; set; }
        public string AuthorizationToken { get; set; }
    }
}
