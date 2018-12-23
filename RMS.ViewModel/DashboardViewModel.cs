using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ViewModel
{
    public class DashboardViewModel
    {
        public int TotalRequestsCount { get; set; }
        public int ItemsPerPage { get; set; }
        public List<RequestDetailsForDashboardViewModel> RequestsList { get; set; }
    }
}
