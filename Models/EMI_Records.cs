using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBFC_App___dev.Models
{
    public class EMI_Records
    {
        public string repaid { get; set; }

        public string agreementid { get; set; }
        public string duedate { get; set; }
        public string startdate { get; set; }

        public string amount { get; set; }

        public string islatepaymentfeeapplied { get; set; }
        public string oldamount { get; set; }

        public string isextensionfeeapplied { get; set; }

        public string extensionduedate { get; set; }
        public string emitype { get; set; }

        public string paymentrecord { get; set; }

        public string paymentstatus { get; set; }
    }
}