using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ViewModel
{
    public class ApprovalViewModel : UpdateRequestStatusViewModel
    {
        [Required]
        public bool IsApproved { get; set; }
    }
}
