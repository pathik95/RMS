using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ViewModel
{
    public class RequestDetailsViewModel
    {
        public int RequestId { get; set; }
        public string CompanyName { get; set; }
        public string ModelNumber { get; set; }
        public string ProblemDetails { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public decimal? Expense { get; set; }
        public string CustomerAddress { get; set; }
        public List<RequestStatusLogViewModel> RequestChangeLog { get; set; }
    }

    public class RequestStatusLogViewModel
    {
        public string ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string UpdatedStatus { get; set; }
        public string Comments { get; set; }
    }

}
