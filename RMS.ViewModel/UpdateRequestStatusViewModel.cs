using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ViewModel
{
    public class UpdateRequestStatusViewModel
    {
        [Required]
        public int RequestId { get; set; }
        public string Comment { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
