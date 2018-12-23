using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ViewModel
{
    public class RequestDetailsForDashboardViewModel
    {
        public int RequestId { get; set; }
        public string CompanyName { get; set; }
        public string ModelNumber { get; set; }
        public string CreatedOn { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }  
    }
}
