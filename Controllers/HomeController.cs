using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NBFC_App___dev.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Configuration;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net;

namespace NBFC_App___dev.Controllers
{
    public class HomeController : Controller
    {
        public List<string> Authentication()
        {
            string bpmcsrf = "";
            string bpmloader = "";
            string aspxauth = "";
            string username = "";
            var client = new RestClient("http://localhost:92/ServiceModel/AuthService.svc/Login");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"UserName\": \"Supervisor\",\r\n    \"UserPassword\": \"Supervisor\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            foreach (var c in response.Cookies)
            {
                if (c.Name.ToString() == "BPMCSRF")
                {
                    bpmcsrf = c.Value.ToString();
                }
                else if (c.Name.ToString() == "BPMLOADER")
                {
                    bpmloader = c.Value.ToString();
                }
                else if (c.Name.ToString() == ".ASPXAUTH")
                {
                    aspxauth = c.Value.ToString();
                }
                else if (c.Name.ToString() == "UserName")
                {
                    username = c.Value.ToString();
                }
            }

            List<string> Cookies = new List<string>();
            Cookies.Add(bpmcsrf);
            Cookies.Add(bpmloader);
            Cookies.Add(aspxauth);
            Cookies.Add(username);

            return Cookies;
        }

        public JObject GET_Object(string url)
        {
            List<string> GetCookies = Authentication();
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("BPMCSRF", GetCookies[0]);
            request.AddCookie(".ASPXAUTH", GetCookies[2]);
            request.AddCookie("BPMCSRF", GetCookies[0]);
            request.AddCookie("BPMLOADER", GetCookies[1]);
            request.AddCookie("UserName", GetCookies[3]);

            IRestResponse response = client.Execute(request);
            JObject ParsedObject = JObject.Parse(response.Content);
            return ParsedObject;
        }

        public ActionResult Products()
        {
            string url = "http://localhost:92/0/odata/UsrProducts?$select=Id,Name,UsrNotes&$expand=UsrProductType($select=Name)";

            JObject ParsedResponse = GET_Object(url);
            List<Products> product_list = new List<Products>();
            foreach (var v in ParsedResponse["value"])
            {
                Products prd = new Products()
                {
                    id = v["Id"].ToString(),
                    name = v["Name"].ToString(),
                    type = v["UsrProductType"]["Name"].ToString()
                };

                product_list.Add(prd);
            }

            ViewData["ProductsData"] = product_list;
            return View();
        }

        public ActionResult ProductsInfo(string Id)
        {
            string url = string.Format("http://localhost:92/0/odata/UsrSpecificationOfProducts?$select=UsrValueDecimal,UsrValueInteger&$expand=UsrParameter($select=Name)&$filter=UsrProducts/Id eq {0}",Id);

            JObject ParsedResponse = GET_Object(url);
            List<ProductsInfo> productinfo_list = new List<ProductsInfo>();
            foreach (var v in ParsedResponse["value"])
            {
                ProductsInfo prdinfo = new ProductsInfo()
                {
                    parameter= v["UsrParameter"]["Name"].ToString(),
                    rate = v["UsrValueDecimal"].ToString(),
                    days = v["UsrValueInteger"].ToString()
                };

                productinfo_list.Add(prdinfo);
            }

            ViewData["ProductsInfoData"] = productinfo_list;
            return View();
        }
        public ActionResult Agreements()
        {
            string dbconn = ConfigurationManager.AppSettings["dbconn"];
            string connectionString = dbconn;
            SqlConnection sqlCnctn = new SqlConnection(connectionString);
            sqlCnctn.Open();
            string strQry = "Select * from UserInfo where session = '" + Session["Name"] + "'";
            SqlDataAdapter sda = new SqlDataAdapter(strQry, sqlCnctn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string pannumber = row["pannumber"].ToString();
                List<string> GetCookies = Authentication();

                string url = string.Format("http://localhost:92/0/odata/UsrAgreements?$select=Id,UsrName,UsrTSSignedOn,UsrTSValidFrom,UsrTSExpiresOn,UsrApprovedTenureInMonths,UsrApprovedTenureInDays&$filter=UsrContact/UsrPANNumber eq '{0}'&$expand=UsrAgreementStatus($select=Name),UsrProducts($select=Name)", pannumber);

                JObject ParsedResponse = GET_Object(url);

                List<Agreements> list = new List<Agreements>();
                

                foreach (var v in ParsedResponse["value"])
                {
                    Agreements agr = new Agreements()
                    {
                        id = v["Id"].ToString(),
                        status = v["UsrAgreementStatus"]["Name"].ToString(),
                        number = v["UsrName"].ToString(),
                        startedon = v["UsrTSValidFrom"].ToString(),
                        expiredon = v["UsrTSExpiresOn"].ToString(),
                        product = v["UsrProducts"]["Name"].ToString(),
                        tenure = v["UsrApprovedTenureInMonths"].ToString() == "0" ? v["UsrApprovedTenureInDays"].ToString() + " Days" : v["UsrApprovedTenureInMonths"].ToString() + " Months"
                    };

                    list.Add(agr);
                }

                ViewData["AgreementData"] = list;
                return View();
            }
            else
            {
                Response.Redirect("~/index.aspx");
                return null;
            }



        }

        public ActionResult AgreementInfo(string Id)
        {
            string url = string.Format("http://localhost:92/0/odata/UsrAgreements({0})?$select=Id,UsrName,UsrTSValidFrom,UsrTSExpiresOn,UsrApprovedTenureInMonths,UsrApprovedTenureInDays,UsrTotalDebtAmount,UsrBalancedDebtAmount,UsrOverpaymentDebtAmount,UsrIsLatePaymentFeeApplied,UsrOldDebtAmount,UsrIsExtensionApplied&$expand=UsrAgreementStatus($select=Name),UsrProducts($select=Name),UsrTSApplication($select=UsrName),UsrContact($select=Name),UsrLoanType($select=Name)", Id);

            JObject ParsedResponse = GET_Object(url);
            AgreementInfo agrInfo = new AgreementInfo()
            {
                id = ParsedResponse["Id"].ToString(),
                status = ParsedResponse["UsrAgreementStatus"]["Name"].ToString(),
                number = ParsedResponse["UsrName"].ToString(),
                startedon = ParsedResponse["UsrTSValidFrom"].ToString(),
                expiredon = ParsedResponse["UsrTSExpiresOn"].ToString(),
                product = ParsedResponse["UsrProducts"]["Name"].ToString(),
                tenure = ParsedResponse["UsrApprovedTenureInMonths"].ToString() == "0" ? ParsedResponse["UsrApprovedTenureInDays"].ToString() + " Days" : ParsedResponse["UsrApprovedTenureInMonths"].ToString() + " Months",
                contact = ParsedResponse["UsrContact"]["Name"].ToString(),
                debtamount = ParsedResponse["UsrTotalDebtAmount"].ToString(),
                loantype = ParsedResponse["UsrLoanType"]["Name"].ToString(),
                balanceddebtamount = ParsedResponse["UsrBalancedDebtAmount"].ToString(),
                overpaymentamount = ParsedResponse["UsrOverpaymentDebtAmount"].ToString(),
                isextensionapplied = ParsedResponse["UsrIsExtensionApplied"].ToString(),
                islatepaymentfeeapplied = ParsedResponse["UsrIsLatePaymentFeeApplied"].ToString(),
                olddebtamount = ParsedResponse["UsrOldDebtAmount"].ToString(),
                application = ParsedResponse["UsrTSApplication"]["UsrName"].ToString()
            };

            //Displaying Operations on Aggreements
            string operationurl = string.Format("http://localhost:92/0/odata/UsrOperations?$select=UsrAmount&$filter=UsrAgreement/Id eq {0}&$expand=UsrType($select=Name),UsrCategory($select=Name)", Id);
            JObject OperationResponse = GET_Object(operationurl);

            List<Operations> Oprlist = new List<Operations>();


            foreach (var v in OperationResponse["value"])
            {
                Operations oprn = new Operations()
                {
                    amount = v["UsrAmount"].ToString(),
                    type = v["UsrType"]["Name"].ToString(),
                    category = v["UsrCategory"]["Name"].ToString()
                };

                Oprlist.Add(oprn);
            }

            ViewData["OperationMessage"] = Oprlist;

            //Displaying EMI Records on Aggreements

            if (ParsedResponse["UsrLoanType"]["Name"].ToString() == "Long Term Loan")
            {
                string emiurl = string.Format("http://localhost:92/0/odata/UsrEMIRecords?$select=UsrDueDate,UsrStartDate,UsrAmount,UsrIsLatePaymentFeeApplied,UsrOldAmount,UsrIsExtensionFeeApplied,UsrExtensionDueDate&$filter=UsrAgreement/Id eq {0}&$expand=UsrEMIType($select = Name), UsrPaymentGate($select = UsrName)", Id);
                JObject emiResponse = GET_Object(emiurl);

                List<EMI_Records> emi_list = new List<EMI_Records>();


                foreach (var v in emiResponse["value"])
                {
                    EMI_Records emir = new EMI_Records()
                    {
                        amount = v["UsrAmount"].ToString(),
                        duedate= v["UsrDueDate"].ToString(),
                        startdate = v["UsrStartDate"].ToString(),
                        islatepaymentfeeapplied = v["UsrIsLatePaymentFeeApplied"].ToString() == "false" ? " " : v["UsrIsLatePaymentFeeApplied"].ToString(),
                        isextensionfeeapplied = v["UsrIsExtensionFeeApplied"].ToString() == "false" ? " ": v["UsrIsExtensionFeeApplied"].ToString(),
                        extensionduedate = v["UsrExtensionDueDate"].ToString(),
                        emitype = v["UsrEMIType"]["Name"].ToString(),
                        paymentrecord = v["UsrPaymentGate"]["UsrName"].ToString(),
                        oldamount = v["UsrOldAmount"].ToString()

                    };

                    emi_list.Add(emir);
                }

                ViewData["EMIRecordsMessage"] = emi_list;

            }


            ViewData["Message"] = agrInfo;
            return View();

            
        
        }

        public ActionResult Applications()
        {
            string dbconn = ConfigurationManager.AppSettings["dbconn"];
            string connectionString = dbconn;
            SqlConnection sqlCnctn = new SqlConnection(connectionString);
            sqlCnctn.Open();
            string strQry = "Select * from UserInfo where session = '" + Session["Name"] + "'";
            SqlDataAdapter sda = new SqlDataAdapter(strQry, sqlCnctn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string pannumber = row["pannumber"].ToString();
                List<string> GetCookies = Authentication();

                string url = string.Format("http://localhost:92/0/odata/UsrApplications?$select=Id,UsrName,CreatedOn,UsrRequestedTermInDays,UsrRequestedAmount,UsrRequestedTermInMonths&$filter=UsrContact/UsrPANNumber eq '{0}'&$expand=UsrApplicationStatus($select=Name),UsrRequestedProduct($select=Name)", pannumber);

                JObject ParsedResponse = GET_Object(url);
               

                List<Applications> list = new List<Applications> () ;
                
                
                foreach (var v in ParsedResponse["value"])
                {
                    Applications app = new Applications()
                    {
                        id = v["Id"].ToString(),
                        status = v["UsrApplicationStatus"]["Name"].ToString(),
                        number = v["UsrName"].ToString(),
                        createdOn = v["CreatedOn"].ToString(),
                        requestedamount = v["UsrRequestedAmount"].ToString(),
                        product = v["UsrRequestedProduct"]["Name"].ToString(),
                        requestedterm = v["UsrRequestedTermInMonths"].ToString() == "0" ? v["UsrRequestedTermInDays"].ToString()+" Days": v["UsrRequestedTermInMonths"].ToString()+" Months"
                    };

                    list.Add(app);
                }

                ViewData["ApplicationData"] = list;
                return View();
            }
            else
            {
                Response.Redirect("~/index.aspx");
                return null;
            }
            

           
        }
        public ActionResult ApplicationInfo(string Id)
        {

            List<string> GetCookies = new List<string>();
            string url = string.Format("http://localhost:92/0/odata/UsrApplications({0})?$select=Id,UsrName,CreatedOn,UsrRequestedTermInDays,UsrRequestedAmount,UsrRequestedTermInMonths,UsrCoApplicantName,UsrApprovedTermInMonths,UsrApprovedTermInDays,UsrApprovedAmount,UsrAccountNumber&$expand=UsrApplicationStatus($select=Name),UsrRequestedProduct($select=Name),UsrContact($select=Name),UsrIndustryType($select=Name),UsrReasonForLoan($select=Name),UsrBankName($select=Name)",Id);

            JObject ParsedResponse = GET_Object(url);



            ApplicationInfo appInfo = new ApplicationInfo()
            {
                id = ParsedResponse["Id"].ToString(),
                status = ParsedResponse["UsrApplicationStatus"]["Name"].ToString(),
                number = ParsedResponse["UsrName"].ToString(),
                createdOn = ParsedResponse["CreatedOn"].ToString(),
                requestedamount = ParsedResponse["UsrRequestedAmount"].ToString(),
                approvedamount = ParsedResponse["UsrApprovedAmount"].ToString(),
                accountnumber = ParsedResponse["UsrAccountNumber"].ToString(),
                contact = ParsedResponse["UsrContact"]["Name"].ToString(),
                occupation = ParsedResponse["UsrIndustryType"]["Name"].ToString(),
                product = ParsedResponse["UsrRequestedProduct"]["Name"].ToString(),
                reasonforloan = ParsedResponse["UsrReasonForLoan"]["Name"].ToString(),
                bankname = ParsedResponse["UsrBankName"]["Name"].ToString(),
                coapplicantname = ParsedResponse["UsrCoApplicantName"].ToString(),
                approvedterm = ParsedResponse["UsrApprovedTermInMonths"].ToString() == "0" ? ParsedResponse["UsrApprovedTermInDays"].ToString() + " Days" : ParsedResponse["UsrApprovedTermInMonths"].ToString() + " Months",
                requestedterm = ParsedResponse["UsrRequestedTermInMonths"].ToString() == "0" ? ParsedResponse["UsrRequestedTermInDays"].ToString() + " Days" : ParsedResponse["UsrRequestedTermInMonths"].ToString() + " Months",
            };


            if (ParsedResponse["UsrApplicationStatus"]["Name"].ToString() == "Rejected")
            {
                string rejectionreasonurl = string.Format("http://localhost:92/0/odata/UsrApplicationRejectionReasonRecords?$select=UsrRejectionReasonId&$filter=UsrApplication/Id eq {0}&$expand=UsrRejectionReason($select=Name)", Id);
                JObject RejectionResponse = GET_Object(rejectionreasonurl);

                List<RejectionReasonsDetail> list = new List<RejectionReasonsDetail>();


                foreach (var v in RejectionResponse["value"])
                {
                    RejectionReasonsDetail rrdetail = new RejectionReasonsDetail()
                    {
                        id = v["UsrRejectionReasonId"].ToString(),
                        name = v["UsrRejectionReason"]["Name"].ToString(),
                    };

                    list.Add(rrdetail);
                }

                ViewData["RejectionMessage"] = list;
            }

            ViewData["Message"] = appInfo;
            return View();

        }

        public ActionResult About()
        {
            string dbconn = ConfigurationManager.AppSettings["dbconn"];
            string connectionString = dbconn;
            //string connectionString = @"Data Source=DESKTOP-HLC3FB7\SQLEXPRESS;Initial Catalog=UserData;Integrated Security=false;User id=Admin;password=Admin@123";
            //string connectionString = @"Data Source=DESKTOP-CV6742D;Initial Catalog=UserData;Integrated Security=false;User id=Akshit;password=Akshit";
            SqlConnection sqlCnctn = new SqlConnection(connectionString);
            sqlCnctn.Open();
            string strQry = "Select * from UserInfo where session = '" + Session["Name"] + "'";
            SqlDataAdapter sda = new SqlDataAdapter(strQry, sqlCnctn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                User p = new User()
                {
                    Email = row["email"].ToString(),
                    Mobile = row["mobile"].ToString(),
                    Fullname = row["fullname"].ToString(),
                    firstname = row["firstname"].ToString(),
                    middlename = row["middlename"].ToString(),
                    lastname = row["lastname"].ToString(),
                    gender = row["gender"].ToString(),
                    aadharnumber = row["aadharnumber"].ToString(),
                    maritalstatus = row["maritalstatus"].ToString(),
                    employmenttype = row["employmenttype"].ToString(),
                    fathername = row["fathername"].ToString(),
                    spousename = row["spousename"].ToString(),
                    pannumber = row["pannumber"].ToString(),
                    currentstreet = row["currentstreet"].ToString(),
                    currentlandmark = row["currentlandmark"].ToString(),
                    currentbuilding = row["currentbuilding"].ToString(),
                    currentcity = row["currentcity"].ToString(),
                    currentstate = row["currentstate"].ToString(),
                    currentpin = row["currentpin"].ToString(),
                    currentcountry = row["currentcountry"].ToString(),
                    panfirstname = row["panfirstname"].ToString(),
                    panmiddlename = row["panmiddlename"].ToString(),
                    panlastname = row["panlastname"].ToString(),
                    panfathername = row["panfathername"].ToString(),
                    panbirthdate = row["panbirthdate"].ToString(),
                    uploadedvalue = row["uploadedvalue"].ToString(),
                    step1 = row["step1"].ToString(),
                    aadharfirstname = row["aadharfirstname"].ToString(),
                    aadharmiddlename = row["aadharmiddlename"].ToString(),
                    aadharlastname = row["aadharlastname"].ToString(),
                    aadharaddress = row["aadharaddress"].ToString(),
                    aadharbirthdate = row["aadharbirthdate"].ToString(),
                    birthdate = row["birthdate"].ToString()


                };
                ViewData["Message"] = p;
                return View();
            }
            else
            {
                Response.Redirect("~/index.aspx");
            }
            return null;
        }        

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("~/index.aspx");
            return null;
        }
        public ActionResult New_Loan()
        {
            Response.Redirect("~/personal.aspx");
            return null;
        }
        [HttpPost]         
        public ActionResult On_Save(User p)
        {
            string dbconn = ConfigurationManager.AppSettings["dbconn"];
            string connectionString = dbconn;
            SqlConnection sqlCnctn = new SqlConnection(connectionString);
            sqlCnctn.Open();
            string strQry = "Select * from UserInfo where session = '" + Session["Name"] + "'";
            SqlDataAdapter sda = new SqlDataAdapter(strQry, sqlCnctn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string firstname = p.firstname;
            string middlename = p.middlename;
            string lastname = p.lastname;
            string gender = p.gender;
            string aadharnumber = p.aadharnumber;
            string pannumber = p.pannumber;
            string maritalstatus = p.maritalstatus;
            string employmenttype = p.employmenttype;
            string spousename = p.spousename;
            string fathername = p.fathername;
            string currentstreet = p.currentstreet;
            string currentlandmark = p.currentlandmark;
            string currentbuilding = p.currentbuilding;
            string currentcity = p.currentcity;
            string currentstate = p.currentstate;
            string currentpin = p.currentpin;
            string currentcountry = p.currentcountry;
            string panfirstname = p.panfirstname;
            string panmiddlename = p.panmiddlename;
            string panlastname = p.panlastname;
            string panfathername = p.panfathername;
            string panbirthdate = p.panbirthdate;
            string aadharfirstname = p.aadharfirstname;
            string aadharmiddlename = p.aadharmiddlename;
            string aadharlastname = p.aadharlastname;
            string aadharaddress = p.aadharaddress;
            string aadharbirthdate = p.aadharbirthdate;
            string birthdate = p.birthdate;
            if (dt.Rows.Count > 0)
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand cmd;
                string sql = "Update UserInfo set firstname = '" + firstname + "',birthdate = '" + birthdate + "',aadharbirthdate = '" + aadharbirthdate + "',aadharaddress = '" + aadharaddress + "',aadharlastname = '" + aadharlastname + "',aadharmiddlename = '" + aadharmiddlename + "',aadharfirstname = '" + aadharfirstname + "',panbirthdate = '" + panbirthdate + "',panfathername = '" + panfathername + "',panlastname = '" + panlastname + "',panmiddlename = '" + panmiddlename + "',panfirstname = '" + panfirstname + "',currentcountry = '" + currentcountry + "',currentpin = '" + currentpin + "',currentstate = '" + currentstate + "',currentcity = '" + currentcity + "',currentbuilding = '" + currentbuilding + "',currentlandmark = '" + currentlandmark + "',currentstreet = '" + currentstreet + "',lastname = '" + lastname + "',middlename = '" + middlename + "',gender = '" + gender + "',aadharnumber = '" + aadharnumber + "',pannumber = '" + pannumber + "',maritalstatus = '" + maritalstatus + "',employmenttype = '" + employmenttype + "',fathername = '" + fathername + "',spousename = '" + spousename + "' where session = '" + Session["Name"].ToString() + "'";
                cmd = new SqlCommand(sql, sqlCnctn);
                adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);
                adapter.UpdateCommand.ExecuteNonQuery();
                cmd.Dispose();
            }
            else
            {
                Response.Redirect("~/index.aspx");
                return null;
            }
            return RedirectToAction("About");
        }

        public string OCR_Mechanism(string path)
        {
            var client = new RestClient("https://api.ocr.space/parse/image");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("apikey", "helloworld");
            request.AddHeader("isTable", "true");
            request.AddParameter("language", "eng");
            request.AddParameter("isOverlayRequired", "false");
            request.AddParameter("url", "http://dl.a9t9.com/ocrbenchmark/eng.png");
            request.AddParameter("iscreatesearchablepdf", "false");
            request.AddParameter("issearchablepdfhidetextlayer", "false");

            request.AddFile("filetype", path);
            IRestResponse response = client.Execute(request);

            var details = JObject.Parse(response.Content);

            JArray jarray = JArray.Parse(details["ParsedResults"].ToString());
            string responseResult = jarray[0]["ParsedText"].ToString();

            return responseResult;
        }

        public string OCR_PAN(string path)
        {
            

                string responseResult = OCR_Mechanism(path);

                string PANNumber_Regex = "[A-Z]{5}[0-9]{4}[A-Z]{1}";

                string DateOfBirth_Regex = "[0-3]{1}[0-9]{1}/[0-9]{2}/[0-9]{4}";

                List<string> list = new List<string>();
                String[] strArray = responseResult.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].Contains("Name"))
                    {
                        list.Add(strArray[i + 1]);
                    }
                }

                List<string> OutputList_PAN = new List<string>();
                OutputList_PAN.Add(list.ToArray()[0].ToString());  // Name
                OutputList_PAN.Add(list.ToArray()[1].ToString());   // Father's Name
                OutputList_PAN.Add((Regex.Matches(responseResult, PANNumber_Regex)[0]).ToString()); // PAN Number
                OutputList_PAN.Add((Regex.Matches(responseResult, DateOfBirth_Regex)[0]).ToString());  // Date of Birth

            return (String.Join(";", OutputList_PAN));
            
        }

        public string OCR_AadharFront(string path)
        {
            

                string responseResult = OCR_Mechanism(path);

                string AaadharNumber_Regex = "[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}";

                string DateOfBirth_Regex = "[0-3]{1}[0-9]{1}/[0-9]{2}/[0-9]{4}";

                String[] strArray = responseResult.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                List<string> OutputList_Aadhar= new List<string>();
            OutputList_Aadhar.Add(strArray[1]); // Aadhar Name 
            OutputList_Aadhar.Add(Regex.Matches(responseResult, DateOfBirth_Regex)[0].ToString()); // Aadhar DOB
            //OutputList_Aadhar.Add(((responseResult.Contains("Male") && !responseResult.Contains("Female")) ? "Male" : "Female"));  // Aaddhar Gender
            OutputList_Aadhar.Add(Regex.Matches(responseResult, AaadharNumber_Regex)[0].ToString()); // Aadhar Number

            return (String.Join(";", OutputList_Aadhar));

        }

        public string OCR_AadharBack(string path)
        {


            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://accurascan.com/api/v4/ocr");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Api-Key", "1615142527yNEfcvD5u3BTj5IsM8Gk6W4xZ1i9kdMhuSHHMgFv");
            //request.AddHeader("Cookie", "laravel_session=eyJpdiI6IjJqUkllcHA3ZFhuaEdZR1krN3pUVFE9PSIsInZhbHVlIjoiSVNMbDRNTDZYT1JRWW5UbjlSWnlTUTF0bXhQOStMOTVjM1lESTNDZEFTdlpocCtGSFVxeXNTall5ckFpOUY2WSIsIm1hYyI6ImExODEzNzZmNmY0MmQxNGVhMDdjMzcwNmYzZDQ1ZmM0NTZmYjRiOTVlM2Q2YmQzMDZlYmY0Y2Q3YjJmZmMzMzcifQ%3D%3D");
            request.AddCookie("Cookie", "laravel_session=eyJpdiI6IjJqUkllcHA3ZFhuaEdZR1krN3pUVFE9PSIsInZhbHVlIjoiSVNMbDRNTDZYT1JRWW5UbjlSWnlTUTF0bXhQOStMOTVjM1lESTNDZEFTdlpocCtGSFVxeXNTall5ckFpOUY2WSIsIm1hYyI6ImExODEzNzZmNmY0MmQxNGVhMDdjMzcwNmYzZDQ1ZmM0NTZmYjRiOTVlM2Q2YmQzMDZlYmY0Y2Q3YjJmZmMzMzcifQ%3D%3D");
            request.AddFile("scan_image", path);
            request.AddParameter("country_code", "IND");
            request.AddParameter("card_code", "ADHB");
            IRestResponse response = client.Execute(request);

            var details = JObject.Parse(response.Content);

            return details["data"]["OCRdata"]["Address"].ToString();

        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase files,HttpPostedFileBase files2 , HttpPostedFileBase files3)
        {
            var  pannumber = "";
            var  panfirstname = "";
            var  panmiddlename = "";
            var  panlastname = "";
            var  panfathername = "";
            var  panbirthdate = "";
             
            var  aadharfirstname = "";
            var  aadharlastname = "";
            var  aadharmiddlename = "";
            var aadharaddress = "";
            var aadharbirthdate = "";
            var aadharnumber = "";


            var path_PAN = "";
            var path_AadharFront = "";
            var path_AadharBack = "";
            int f1 = 0;
            int f2 = 0;
            int f3 = 0;
            //string connectionString = @"Data Source=DESKTOP-CV6742D;Initial Catalog=UserData;Integrated Security=false;User id=Akshit;password=Akshit";
            string dbconn = ConfigurationManager.AppSettings["dbconn"];
            string connectionString = dbconn;
            SqlConnection sqlCnctn = new SqlConnection(connectionString);
            sqlCnctn.Open();
            string strQry = "Select * from UserInfo where session = '" + Session["Name"] + "'";
            SqlDataAdapter sda = new SqlDataAdapter(strQry, sqlCnctn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string email = row["email"].ToString();
                
                if (files != null && files.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(files.FileName);
                    var extension = Path.GetExtension(files.FileName);
                    path_PAN = "D:\\Uploads\\PAN\\" + email + extension; 
                    files.SaveAs(path_PAN);
                    f1 = 1;
                }
                if (files2 != null && files2.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(files2.FileName);
                    var extension = Path.GetExtension(files2.FileName);
                    path_AadharFront = "D:\\Uploads\\AadharFront\\" + email + extension;
                    
                    files2.SaveAs(path_AadharFront);
                    f2 = 1;
                }
                if (files3 != null && files3.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(files3.FileName);
                    var extension = Path.GetExtension(files3.FileName);
                    path_AadharBack = "D:\\Uploads\\AadharBack\\" + email + extension;

                    files3.SaveAs(path_AadharBack);
                    f3 = 1;
                }

            }
            sqlCnctn.Close();

            if (files != null && files.ContentLength > 0)
            {
                string pan_details = OCR_PAN(path_PAN);
                String[] strArray_PAN = pan_details.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                String[] strArray_PAN_nameSplit = strArray_PAN[0].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                pannumber = strArray_PAN[2];

                if (strArray_PAN_nameSplit.Length == 1)
                {
                    panfirstname = strArray_PAN_nameSplit[0];
                    panmiddlename = "";
                    panlastname = "";
                }
                else if (strArray_PAN_nameSplit.Length == 2)
                {
                    panfirstname = strArray_PAN_nameSplit[0];
                    panmiddlename = "";
                    panlastname = strArray_PAN_nameSplit[1];
                }
                else
                {
                    panfirstname = strArray_PAN_nameSplit[0];
                    panmiddlename = strArray_PAN_nameSplit[1];
                    panlastname = strArray_PAN_nameSplit[2];
                };

                panfathername = strArray_PAN[1];
                var pan_birthdate = Convert.ToDateTime(strArray_PAN[3], System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);

                panbirthdate = pan_birthdate.ToString("yyyy-MM-dd");

            }

            if (files2 != null && files2.ContentLength > 0)
            {
                string AadharFront_details = OCR_AadharFront(path_AadharFront);
                String[] strArray_Aadhar = AadharFront_details.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                String[] strArray_Aadhar_name_split = strArray_Aadhar[0].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);


                if (strArray_Aadhar_name_split.Length == 1)
                {
                    aadharfirstname = strArray_Aadhar_name_split[0];
                    aadharmiddlename = "";
                    aadharlastname = "";
                }
                else if (strArray_Aadhar_name_split.Length == 2)
                {
                    aadharfirstname = strArray_Aadhar_name_split[0];
                    aadharmiddlename = "";
                    aadharlastname = strArray_Aadhar_name_split[1];
                }
                else
                {
                    aadharfirstname = strArray_Aadhar_name_split[0];
                    aadharmiddlename = strArray_Aadhar_name_split[1];
                    aadharlastname = strArray_Aadhar_name_split[2];
                };

                aadharnumber = strArray_Aadhar[2];

                var Aadhar_birthdate = Convert.ToDateTime(strArray_Aadhar[1], System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                aadharbirthdate = Aadhar_birthdate.ToString("yyyy-MM-dd");
            }

            if (files3 != null && files3.ContentLength > 0)
            {
                aadharaddress = OCR_AadharBack(path_AadharBack);
            }

            sqlCnctn.Open();
            strQry = "Select * from UserInfo where session = '" + Session["Name"] + "'";
            sda = new SqlDataAdapter(strQry, sqlCnctn);
            dt = new DataTable();
            sda.Fill(dt);           
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string uploadedval = row["uploadedvalue"].ToString();
                if (uploadedval == "false")
                {
                    if (f1 == 1 && f2 == 1 && f3 == 1)
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand cmd;
                        string sql = "Update UserInfo set uploadedvalue = 'true',aadharaddress = '" + aadharaddress + "', filepathAadharBack = '" + path_AadharBack + "',filepathAadharFront = '" + path_AadharFront + "',filepathPAN = '" + path_PAN + "', aadharnumber = '" + aadharnumber + "',aadharbirthdate = '" + aadharbirthdate + "',aadharmiddlename = '" + aadharmiddlename + "',aadharlastname = '" + aadharlastname + "',aadharfirstname = '" + aadharfirstname + "',pannumber = '" + pannumber + "', panbirthdate = '" + panbirthdate + "', panfathername = '" + panfathername + "', panlastname = '" + panlastname + "', panmiddlename = '" + panmiddlename + "', panfirstname = '" + panfirstname + "' where session = '" + Session["Name"].ToString() + "'";
                        cmd = new SqlCommand(sql, sqlCnctn);
                        adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);
                        adapter.UpdateCommand.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    else if (f1 == 1 && f2 == 0 && f3 == 0)
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand cmd;
                        string sql = "Update UserInfo set uploadedvalue = 'false',pannumber = '" + pannumber + "',filepathPAN = '" + path_PAN + "', panbirthdate = '" + panbirthdate + "', panfathername = '" + panfathername + "', panlastname = '" + panlastname + "', panmiddlename = '" + panmiddlename + "', panfirstname = '" + panfirstname + "' where session = '" + Session["Name"].ToString() + "'";
                        cmd = new SqlCommand(sql, sqlCnctn);
                        adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);
                        adapter.UpdateCommand.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    else if (f1 == 0 && f2 == 1 && f3 == 0)
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand cmd;
                        string sql = "Update UserInfo set uploadedvalue = 'false', aadharnumber = '" + aadharnumber + "',filepathAadharFront = '" + path_AadharFront + "',aadharbirthdate = '" + aadharbirthdate + "',aadharmiddlename = '" + aadharmiddlename + "',aadharlastname = '" + aadharlastname + "',aadharfirstname = '" + aadharfirstname + "' where session = '" + Session["Name"].ToString() + "'";
                        cmd = new SqlCommand(sql, sqlCnctn);
                        adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);
                        adapter.UpdateCommand.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    else if (f1 == 0 && f2 == 0 && f3 == 1)
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand cmd;
                        string sql = "Update UserInfo set uploadedvalue = 'false', filepathAadharBack = '" + path_AadharBack + "',aadharaddress = '" + aadharaddress + "' where session = '" + Session["Name"].ToString() + "'";
                        cmd = new SqlCommand(sql, sqlCnctn);
                        adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);
                        adapter.UpdateCommand.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
                else
                {
                    if (f1 == 1)
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand cmd;
                        string sql = "Update UserInfo set uploadedvalue = 'true',pannumber = '" + pannumber + "',filepathPAN = '" + path_PAN + "', panbirthdate = '" + panbirthdate + "', panfathername = '" + panfathername + "', panlastname = '" + panlastname + "', panmiddlename = '" + panmiddlename + "', panfirstname = '" + panfirstname + "' where session = '" + Session["Name"].ToString() + "'";
                        cmd = new SqlCommand(sql, sqlCnctn);
                        adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);
                        adapter.UpdateCommand.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    if (f2 == 1)
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand cmd;
                        string sql = "Update UserInfo set uploadedvalue = 'true', aadharnumber = '" + aadharnumber + "',filepathAadharFront = '" + path_AadharFront + "',aadharbirthdate = '" + aadharbirthdate + "',aadharmiddlename = '" + aadharmiddlename + "',aadharlastname = '" + aadharlastname + "',aadharfirstname = '" + aadharfirstname + "' where session = '" + Session["Name"].ToString() + "'";
                        cmd = new SqlCommand(sql, sqlCnctn);
                        adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);
                        adapter.UpdateCommand.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    if (f3 == 1)
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand cmd;
                        string sql = "Update UserInfo set uploadedvalue = 'true', filepathAadharBack = '" + path_AadharBack + "',aadharaddress = '" + aadharaddress + "' where session = '" + Session["Name"].ToString() + "'";
                        cmd = new SqlCommand(sql, sqlCnctn);
                        adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);
                        adapter.UpdateCommand.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
            }
            else
            {
                Response.Redirect("~/index.aspx");
                return null;
            }                        
            return RedirectToAction("About");
        }

        [HttpPost]
        public ActionResult Apply(FormCollection data)
        {
            string dbconn = ConfigurationManager.AppSettings["dbconn"];
            string connectionString = dbconn;
            SqlConnection sqlCnctn = new SqlConnection(connectionString);
            sqlCnctn.Open();
            string strQry = "Select * from UserInfo where session = '" + Session["Name"] + "'";
            SqlDataAdapter sda = new SqlDataAdapter(strQry, sqlCnctn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string numberofdependents = data["numberofdependents"];
                string coapplicantname = data["coapplicantname"];
                string coapplicantmobilephone = data["coapplicantmobilephone"];
                string coapplicantrelationship = data["coapplicantrelationship"];
                string bankifsccode = data["bankifsccode"];
                string bankaccountnumber = data["bankaccountnumber"];
                string bankname = data["bankname"];

                DataRow row = dt.Rows[0];
                //string Email = row["email"].ToString();
                //string Mobile = row["mobile"].ToString();
                //string Fullname = row["fullname"].ToString();
                string firstname = row["firstname"].ToString();
                string middlename = row["middlename"].ToString();
                string lastname = row["lastname"].ToString();
                string gender = row["gender"].ToString();
                string aadharnumber = row["aadharnumber"].ToString();
                string maritalstatus = row["maritalstatus"].ToString();
                string employmenttype = row["employmenttype"].ToString();
                string fathername = row["fathername"].ToString();
                string spousename = row["spousename"].ToString();
                //string pannumber = row["pannumber"].ToString();
                string currentstreet = row["currentstreet"].ToString();
                string currentlandmark = row["currentlandmark"].ToString();
                string currentbuilding = row["currentbuilding"].ToString();
                string currentcity = row["currentcity"].ToString();
                string currentstate = row["currentstate"].ToString();
                string currentpin = row["currentpin"].ToString();
                string currentcountry = row["currentcountry"].ToString();
                string panfirstname = row["panfirstname"].ToString();
                string panmiddlename = row["panmiddlename"].ToString();
                string panlastname = row["panlastname"].ToString();
                string panfathername = row["panfathername"].ToString();
                string panbirthdate = row["panbirthdate"].ToString();
                //string uploadedvalue = row["uploadedvalue"].ToString();
                string applicationgateId = row["applicationgateId"].ToString();
                string aadharfirstname = row["aadharfirstname"].ToString();
                string aadharlastname = row["aadharlastname"].ToString();
                string aadharmiddlename = row["aadharmiddlename"].ToString();
                string aadharaddress = row["aadharaddress"].ToString();
                string aadharbirthdate = row["aadharbirthdate"].ToString();
                string birthdate = row["birthdate"].ToString();
                string filepathPAN = row["filepathPAN"].ToString();
                string filepathAadharFront = row["filepathAadharFront"].ToString();
                string filepathAadharBack = row["filepathAadharBack"].ToString();
                string CorrectfilepathPAN = filepathPAN.Replace(@"\", @"\\");
                string CorrectfilepathAadharFront = filepathAadharFront.Replace(@"\", @"\\");
                string CorrectfilepathAadharBack = filepathAadharBack.Replace(@"\", @"\\");
                // Api hit for step-2 s

                List<string> GetCookies = Authentication();

                string url = string.Format("http://localhost:92/0/odata/UsrApplicationGate({0})", applicationgateId);
                var client2 = new RestClient(url);
                client2.Timeout = -1;
                var request2 = new RestRequest(Method.PATCH);
                request2.AddCookie(".ASPXAUTH", GetCookies[2]);
                request2.AddCookie("BPMCSRF", GetCookies[0]);
                request2.AddCookie("BPMLOADER", GetCookies[1]);
                request2.AddCookie("UserName", GetCookies[3]);
                request2.AddHeader("Content-Type", "application/json");
                request2.AddHeader("BPMCSRF", GetCookies[0]);
                // request2.AddHeader("Cookie", "BPMSESSIONID=51ldz41mtbuqxz1jjg3p4vwp; .ASPXAUTH=3E394F2748EFE7521FBE7F573EEC4CF7F4A8628E003960313141E2E503827EAB43C274FE658DF00F4734F66C5FC02B898EC7AA673C8E35C11BC37314EB02857CE3F09B65FECCB55F49DAC2F653BC7074E5FB2920831755CAFD58AEA3724B490D9FA19FE53419DAC6B4F9CC7ACCCC8D2F0C2446E9B50E3341EA01F28E9EDA821F758641FAA2AE4F1BFD5EB622C86837705B738802BA58A6326A1C02C14D94BBCB795085A1594FA0F8BD09997D5A6E28354F19E5A2D4F70EEB177C87656ADAB50CEE061F3C747DB70E9887C6899F63C98885FCAA990C9600E21A8A9C5217E227CCF95380B098CA43B690F126CE7DC266B6D036ECB6557418135B19F2AED8991589A7A15C49046D6C4C946D1C7718DA2144AC4F82246ED68E047D445D90C0AFA5DB62D8E6989B4FEC2FBA361992D29C665507E5A8DAF5E0A3C86B65A216AA32B7CF391D8B99AE5ED52A9632AF5E80924E5F0022396DE5AF7A27ECFDD2A3CF887613B6CDC919; BPMCSRF=RlMlsgX2n8C9JRjK1owIOu; BPMLOADER=zby4bc3aw2qebkrpakmcpunu; UserName=83|117|112|101|114|118|105|115|111|114");
                request2.AddParameter("application/json", "{\r\n    \r\n    \"UsrAction\" : \"2\",\r\n    \"UsrCoApplicantMobilePhone\": \"" + coapplicantmobilephone + "\",\r\n    \"UsrFilePathForAadharBack\": \"" + CorrectfilepathAadharBack + "\",\r\n    \"UsrFilePathForAadharFront\": \"" + CorrectfilepathAadharFront + "\",\r\n    \"UsrFilePathForPAN\": \"" + CorrectfilepathPAN + "\",\r\n    \"UsrBirthDate\": \"" + birthdate + "\",\r\n    \"UsrAadhaarDOB\": \"" + aadharbirthdate + "\",\r\n    \"UsrAadhaarAddress\": \"" + aadharaddress + "\",\r\n    \"UsrAadhaarNumber\": \"" + aadharnumber + "\",\r\n    \"UsrAadhaarFirstName\": \"" + aadharfirstname + "\",\r\n    \"UsrAadhaarMiddleName\": \"" + aadharmiddlename + "\",\r\n    \"UsrAadhaarLastName\": \"" + aadharlastname + "\",\r\n    \"UsrEmploymentTypeId\": \"" + employmenttype + "\",\r\n    \"UsrGivenName\": \"" + firstname + "\",\r\n    \"UsrMiddleName\": \"" + middlename + "\",\r\n    \"UsrSurname\": \"" + lastname + "\",\r\n    \"UsrGenderId\":\"" + gender + "\",\r\n    \"UsrFatherName\":\"" + fathername + "\",\r\n    \"UsrSpouseName\":\"" + spousename + "\",\r\n    \"UsrMaritalStatusId\":\"" + maritalstatus + "\",\r\n    \"UsrNumberOfDependents\":\"" + numberofdependents + "\",\r\n    \"UsrCoApplicantName\": \"" + coapplicantname + "\",\r\n    \"UsrCoApplicantRelationshipId\": \"" + coapplicantrelationship + "\",\r\n    \"UsrPANFirstName\":\"" + panfirstname + "\",\r\n    \"UsrPANMiddleName\":\"" + panmiddlename + "\",\r\n    \"UsrPANLastName\":\"" + panlastname + "\",\r\n    \"UsrPANFatherName\": \"" + panfathername + "\",\r\n    \"UsrPANBirthDate\": \"" + panbirthdate + "\",\r\n    \"UsrCurrentStreet\":\"" + currentstreet + "\",\r\n    \"UsrCurrentBuilding\":\"" + currentbuilding + "\",\r\n    \"UsrCurrentLandmark\":\"" + currentlandmark + "\",\r\n    \"UsrCurrentPIN\":\"" + currentpin + "\",\r\n    \"UsrCurrentStateId\":\"" + currentstate + "\",\r\n    \"UsrCurrentCityId\":\"" + currentcity + "\",\r\n    \"UsrCurrentCountryId\":\"" + currentcountry + "\",\r\n    \"UsrBankIFSCCode\" : \"" + bankifsccode + "\",\r\n    \"UsrBankAccountNumber\":\"" + bankaccountnumber + "\",\r\n    \"UsrBankNameId\": \"" + bankname + "\"     \r\n}\r\n\r\n", ParameterType.RequestBody);
                IRestResponse response2 = client2.Execute(request2);

                System.Threading.Thread.Sleep(5000);

                return RedirectToAction("Agreements");
            }
            else
            {
                Response.Redirect("~/index.aspx");
                return null;
            }
            
        }


    }
}