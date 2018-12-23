using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ViewModel
{
    public class CreateRequestViewModel
    {
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string ModelNumber { get; set; }
        [Required]
        public string ProblemDetails { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string CustomerContact { get; set; }
        [Required]
        public double Expense { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        public string PinCode { get; set; }


    }
}
