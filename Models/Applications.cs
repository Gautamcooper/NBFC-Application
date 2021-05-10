using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBFC_App___dev.Models
{
    public class Applications
    {

        public string id { get; set; }
        public string status { get; set; }
        public string number { get; set; }
        public string createdOn { get; set; }
        public string requestedterm { get; set; }

        public string requestedamount { get; set; }

        public string product { get; set; }

        public string productId { get; set; }
    }
}