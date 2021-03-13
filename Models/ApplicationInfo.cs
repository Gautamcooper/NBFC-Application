using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBFC_App___dev.Models
{
    public class ApplicationInfo
    {
        public string id { get; set; }
        public string status { get; set; }
        public string number { get; set; }
        public string createdOn { get; set; }
        public string requestedterm { get; set; }

        public string requestedamount { get; set; }

        public string product { get; set; }

        public string coapplicantname { get; set; }

        public string approvedterm { get; set; }

        public string approvedamount { get; set; }

        public string accountnumber { get; set; }

        public string contact { get; set; }

        public string occupation { get; set; }
        public string reasonforloan { get; set; }

        public string bankname { get; set; }

        
    }
}