using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Utilities
{
    public class Constants
    {
        public const string ENCRYPTION_KEY = "AAECAwQFBgcICQoLDA0ODw==";
        public const char TOKEN_SEPARATOR = ':';
        public const string DATE_FORMAT = "dd-MMM-yyyy";
        public const string DATE_TIME_FORMAT = "dd-MMM-yyyy hh:mm";
        public const string CLERK_ROLE = "Clerk";
        public const string TECHNICIAN_ROLE = "Technician";
        public const string QUALITY_CONTROL_ROLE = "Quality Control";
        public const string SHIPPING_ROLE = "Shipping";
        public const int ITEMS_PER_PAGE = 10;

    }

    public enum StatusEnum
    {
        Pending = 1,
        Completed,
        Approved,
        Closed
    }

}
