using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBFC_App___dev.Models
{
    public class Agreements
    {
        public string id { get; set; }
        public string number { get; set; }
        
        public string startedon { get; set; }
        public string expiredon { get; set; }

        public string tenure { get; set; }

        public string status { get; set; }

        public string product { get; set; }

        public string loantype { get; set; }

        public string productId { get; set; }



    }
}