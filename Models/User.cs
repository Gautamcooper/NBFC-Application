using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBFC_App___dev.Models
{
    public class User
    {
        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Fullname { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public string aadharnumber { get; set; }
        public string pannumber { get; set; }
        public string maritalstatus { get; set; }
        public string employmenttype { get; set; }
        public string fathername { get; set; }
        public string spousename { get; set; }

        public string currentstreet { get; set; }

        public string currentlandmark { get; set; }

        public string currentbuilding { get; set; }

        public string currentcity { get; set; }

        public string currentstate { get; set; }

        public string currentpin { get; set; }

        public string currentcountry { get; set; }

        public string panfirstname { get; set; }

        public string panmiddlename { get; set; }

        public string panlastname { get; set; }

        public string panfathername { get; set; }

        public string panbirthdate { get; set; }
        public string uploadedvalue { get; set; }
        public string step1 { get; set; }
    }
}