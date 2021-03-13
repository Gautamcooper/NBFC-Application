using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBFC_App___dev.Models
{
    public class AgreementInfo
    {
        public string id { get; set; }
        public string number { get; set; }

        public string startedon { get; set; }
        public string expiredon { get; set; }

        public string tenure { get; set; }

        public string status { get; set; }

        public string product { get; set; }

        public string debtamount { get; set; }

        public string balanceddebtamount     { get; set; }

      

        public string overpaymentamount { get; set; }

        public string islatepaymentfeeapplied { get; set; }

        public string olddebtamount { get; set; }

        public string isextensionapplied { get; set; }

        public string application { get; set; }

        public string contact { get; set; }


        public string loantype { get; set; }

        


    }
}