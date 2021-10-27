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
            string apiurl = ConfigurationManager.AppSettings["apiurl"];
            string url = apiurl + "ServiceModel/AuthService.svc/Login";
            var client = new RestClient(url);
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
        public ActionResult Homepage()
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
                
                string email = dt.Rows[0]["email"].ToString();
                string mobile = dt.Rows[0]["mobile"].ToString();

                string apiurl = ConfigurationManager.AppSettings["apiurl"];
                string temp_url = "0/odata/UsrProducts?$top=6&$select=Id,UsrProductInfo,Name&$orderby=UsrNoOfAgreements desc";
                string url = apiurl + temp_url;
                JObject ParsedResponse = GET_Object(url);
                List<HomePage> topProductlist = new List<HomePage>();

                ViewData["top1_prId"] = ParsedResponse["value"][0]["Id"].ToString();
                ViewData["top1_prName"] = ParsedResponse["value"][0]["Name"].ToString() != "" ? ParsedResponse["value"][0]["Name"].ToString() : "Loan";
                ViewData["top1_prInfo"] = ParsedResponse["value"][0]["UsrProductInfo"].ToString() != "" ? ParsedResponse["value"][0]["UsrProductInfo"].ToString() : "1";

                ViewData["top2_prId"] = ParsedResponse["value"][1]["Id"].ToString();
                ViewData["top2_prName"] = ParsedResponse["value"][1]["Name"].ToString() != "" ? ParsedResponse["value"][1]["Name"].ToString() : "Loan";
                ViewData["top2_prInfo"] = ParsedResponse["value"][1]["UsrProductInfo"].ToString() != "" ? ParsedResponse["value"][1]["UsrProductInfo"].ToString() : "2";

                ViewData["top3_prId"] = ParsedResponse["value"][2]["Id"].ToString();
                ViewData["top3_prName"] = ParsedResponse["value"][2]["Name"].ToString() != "" ? ParsedResponse["value"][2]["Name"].ToString() : "Loan";
                ViewData["top3_prInfo"] = ParsedResponse["value"][2]["UsrProductInfo"].ToString() != "" ? ParsedResponse["value"][2]["UsrProductInfo"].ToString() : "3";

                ViewData["top4_prId"] = ParsedResponse["value"][3]["Id"].ToString();
                ViewData["top4_prName"] = ParsedResponse["value"][3]["Name"].ToString() != "" ? ParsedResponse["value"][3]["Name"].ToString() : "Loan";
                ViewData["top4_prInfo"] = ParsedResponse["value"][3]["UsrProductInfo"].ToString() != "" ? ParsedResponse["value"][3]["UsrProductInfo"].ToString() : "4";

                ViewData["top5_prId"] = ParsedResponse["value"][4]["Id"].ToString();
                ViewData["top5_prName"] = ParsedResponse["value"][4]["Name"].ToString() != "" ? ParsedResponse["value"][4]["Name"].ToString() : "Loan";
                ViewData["top5_prInfo"] = ParsedResponse["value"][4]["UsrProductInfo"].ToString() != "" ? ParsedResponse["value"][4]["UsrProductInfo"].ToString() : "5";

                ViewData["top6_prId"] = ParsedResponse["value"][5]["Id"].ToString();
                ViewData["top6_prName"] = ParsedResponse["value"][5]["Name"].ToString() != "" ? ParsedResponse["value"][5]["Name"].ToString() : "Loan";
                ViewData["top6_prInfo"] = ParsedResponse["value"][5]["UsrProductInfo"].ToString() != "" ? ParsedResponse["value"][5]["UsrProductInfo"].ToString() : "6";

                string strQry2 = "Select * from UserSTEP1Info where email = '" + email + "' and mobile = '" + mobile + "'";
                SqlDataAdapter sda2 = new SqlDataAdapter(strQry2, sqlCnctn);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    ViewBag.Step1Count = dt2.Rows.Count;
                }

                return View();
            }
            else 
            {
                Response.Redirect("~/index.aspx");
                return null;
            }
        }
        public ActionResult Products()
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
                string apiurl = ConfigurationManager.AppSettings["apiurl"];
                string url = apiurl + "0/odata/UsrProducts?$select=Id,Name,UsrNotes&$expand=UsrProductType($select=Name)";

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
            else
            {
                Response.Redirect("~/index.aspx");
                return null;
            }
        }

        public ActionResult ProductsInfo(string Id)
        {
            //Get Product Parameters
            string apiurl = ConfigurationManager.AppSettings["apiurl"];            
            string temp_url = string.Format("0/odata/UsrSpecificationOfProducts?$select=UsrValueDecimal,UsrValueInteger&$expand=UsrParameter($select=Name)&$filter=UsrProducts/Id eq {0}",Id);            
            string url = apiurl + temp_url;
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

            // Get Product Services
            string temp_productService = string.Format("0/odata/UsrProductServiceRecords?$select=UsrServices&$filter=UsrProduct/Id eq {0} &$expand=UsrServices($select=UsrName)", Id);
            string productServiceUrl = apiurl + temp_productService;
            JObject productservices = GET_Object(productServiceUrl);

            List<ProductServices> productservicelist = new List<ProductServices>();


            foreach (var v in productservices["value"])
            {
                ProductServices ps = new ProductServices()
                {

                    name = v["UsrServices"]["UsrName"].ToString(),
                };

                productservicelist.Add(ps);
            }

            //Get Product FAQ's
            string prdfaq = string.Format("0/odata/KnowledgeBase?$select=Name,NotHtmlNote&$filter=UsrProduct/Id eq {0}", Id);
            string prdfaqurl = apiurl + prdfaq;
            JObject prdfaqresponse = GET_Object(prdfaqurl);

            List<FAQ> prdfaqlist = new List<FAQ>();


            foreach (var v in prdfaqresponse["value"])
            {
                FAQ faq = new FAQ
                {
                    question = v["Name"].ToString(),
                    answer = v["NotHtmlNote"].ToString(),
                };
                prdfaqlist.Add(faq);
            }

            ViewData["productfaq"] = prdfaqlist;
            ViewData["ProductServices"] = productservicelist;
            ViewData["ProductId"] = Id;
            ViewData["ProductsInfoData"] = productinfo_list;            
            System.Web.HttpCookie ProductCookie = new System.Web.HttpCookie("ProductId");
            ProductCookie.Value = Id;
            ProductCookie.Expires = DateTime.Now.AddMinutes(5);
            Response.Cookies.Add(ProductCookie);
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
                string apiurl = ConfigurationManager.AppSettings["apiurl"];                
                string temp_url = string.Format("0/odata/UsrAgreements?$select=Id,UsrName,UsrSignedOn,UsrValidFrom,UsrApplicationid,UsrProductsId,UsrClosedOn,UsrApprovedTenureInMonths,UsrApprovedTenureInDays&$filter=UsrContact/UsrPANNumber eq '{0}'&$expand=UsrAgreementStatus($select=Name),UsrProducts($select=Name),UsrApplication($select=UsrName)", pannumber);                
                string url = apiurl + temp_url;
                JObject ParsedResponse = GET_Object(url);
                List<Agreements> list = new List<Agreements>();
                
                foreach (var v in ParsedResponse["value"])
                {
                    Agreements agr = new Agreements()
                    {
                        id = v["Id"].ToString(),
                        status = v["UsrAgreementStatus"]["Name"].ToString(),
                        number = v["UsrName"].ToString(),
                        startedon = v["UsrValidFrom"].ToString().Substring(0, v["UsrValidFrom"].ToString().LastIndexOf(" ") + 1),
                        expiredon = v["UsrClosedOn"].ToString().Substring(0, v["UsrClosedOn"].ToString().LastIndexOf(" ") + 1),
                        product = v["UsrProducts"]["Name"].ToString(),
                        tenure = v["UsrApprovedTenureInMonths"].ToString() == "0" ? v["UsrApprovedTenureInDays"].ToString() + " Days" : v["UsrApprovedTenureInMonths"].ToString() + " Months",
                        application = v["UsrApplication"]["UsrName"].ToString(),
                        applicationId = v["UsrApplicationId"].ToString(),
                        productId = v["UsrProductsId"].ToString()

                    };

                    list.Add(agr);
                }
                if (list.Count == 0)
                {
                    ViewData["AgreementData"] = null;
                }
                else
                {
                    ViewData["AgreementData"] = list;
                }
                
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
            string apiurl = ConfigurationManager.AppSettings["apiurl"];            
            string temp_url = string.Format("0/odata/UsrAgreements({0})?$select=Id,UsrName,UsrValidFrom,UsrClosedOn,UsrApprovedTenureInMonths,UsrApprovedTenureInDays,UsrTotalDebtAmount,UsrBalancedDebtAmount,UsrOverpaymentDebtAmount,UsrIsLatePaymentFeeApplied,UsrOldDebtAmount,UsrIsExtensionApplied&$expand=UsrAgreementStatus($select=Name),UsrProducts($select=Name),UsrApplication($select=UsrName),UsrContact($select=Name),UsrLoanType($select=Name)", Id);            
            string url = apiurl + temp_url;
            JObject ParsedResponse = GET_Object(url);
            AgreementInfo agrInfo = new AgreementInfo()
            {
                id = ParsedResponse["Id"].ToString(),
                status = ParsedResponse["UsrAgreementStatus"]["Name"].ToString(),
                number = ParsedResponse["UsrName"].ToString(),
                startedon = ParsedResponse["UsrValidFrom"].ToString().Substring(0, ParsedResponse["UsrValidFrom"].ToString().LastIndexOf(" ") + 1),
                expiredon = ParsedResponse["UsrClosedOn"].ToString().ToString().Substring(0, ParsedResponse["UsrClosedOn"].ToString().LastIndexOf(" ") + 1),
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
                application = ParsedResponse["UsrApplication"]["UsrName"].ToString()
            };

            //Displaying Operations on Aggreements                  
            string temp_operationurl = string.Format("0/odata/UsrOperations?$select=UsrAmount&$filter=UsrAgreement/Id eq {0}&$expand=UsrType($select=Name),UsrCategory($select=Name)", Id);
            string operationurl = apiurl + temp_operationurl;
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
                string temp_emiurl = string.Format("0/odata/UsrEMIRecords?$select=UsrAgreementId,UsrIsRepaid,UsrDueDate,UsrStartDate,UsrAmount,UsrIsLatePaymentFeeApplied,UsrOldAmount,UsrIsExtensionFeeApplied,UsrExtensionDueDate&$filter=UsrAgreement/Id eq {0}&$expand=UsrEMIType($select = Name), UsrPaymentGate($select = UsrName)", Id);
                string emiurl = apiurl + temp_emiurl;
                JObject emiResponse = GET_Object(emiurl);

                List<EMI_Records> emi_list = new List<EMI_Records>();


                foreach (var v in emiResponse["value"])
                {
                    EMI_Records emir = new EMI_Records()
                    {
                        agreementid = Id,
                        repaid = v["UsrIsRepaid"].ToString() == "False" ? "No" : "Yes",
                        amount = v["UsrAmount"].ToString(),
                        duedate= (v["UsrDueDate"].ToString()).Split(' ')[0],
                        startdate = (v["UsrStartDate"].ToString()).Split(' ')[0],
                        islatepaymentfeeapplied = v["UsrIsLatePaymentFeeApplied"].ToString() == "False" ? "No" : "Yes",
                        isextensionfeeapplied = v["UsrIsExtensionFeeApplied"].ToString() == "False" ? "No": "Yes",
                        emitype = v["UsrEMIType"]["Name"].ToString(),
                        oldamount = v["UsrOldAmount"].ToString(),
                        paymentstatus = v["UsrIsRepaid"].ToString() == "False" ? "Pay" : "Paid",

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
                //string pannumber = row["pannumber"].ToString();
                string email = row["email"].ToString();
                string mobile = row["mobile"].ToString();
                List<string> GetCookies = Authentication();
                string apiurl = ConfigurationManager.AppSettings["apiurl"];                
                
                JObject ParsedResponse = null;

                while (true)
                {
                    string temp_url = string.Format("0/odata/UsrApplications?$select=Id,UsrName,CreatedOn,UsrRequestedTermInDays,UsrAgreementId,UsrRequestedProductId,UsrRequestedAmount,UsrRequestedTermInMonths&$filter=UsrContact/Email eq '{0}' and UsrContact/MobilePhone eq '{1}'&$orderby=CreatedOn desc&$expand=UsrApplicationStatus($select=Name),UsrRequestedProduct($select=Name),UsrAgreement($select=UsrName)", email, mobile);
                    string url = apiurl + temp_url;
                    ParsedResponse = GET_Object(url);

                    if (ParsedResponse["value"].Count() > 0 && ParsedResponse["value"][0]["UsrName"].ToString() != "")
                    {
                        break;
                    }
                }

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
                        requestedterm = v["UsrRequestedTermInMonths"].ToString() == "0" ? v["UsrRequestedTermInDays"].ToString()+" Days": v["UsrRequestedTermInMonths"].ToString()+" Months",
                        agreement = v["UsrAgreement"]["UsrName"].ToString() != "" ? v["UsrAgreement"]["UsrName"].ToString() : "Not Created",
                        agreementId = v["UsrAgreementId"].ToString(),
                        productId = v["UsrRequestedProductId"].ToString()
                    };

                    list.Add(app);
                }
                if (list.Count == 0)
                {
                    ViewData["ApplicationData"] = null;
                }
                else
                {
                    ViewData["ApplicationData"] = list;
                }
                
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
            string apiurl = ConfigurationManager.AppSettings["apiurl"];            
            string temp_url = string.Format("0/odata/UsrApplications({0})?$select=Id,UsrName,CreatedOn,UsrRequestedTermInDays,UsrRequestedAmount,UsrRequestedTermInMonths,UsrCoApplicantName,UsrApprovedTermInMonths,UsrApprovedTermInDays,UsrApprovedAmount,UsrAccountNumber&$expand=UsrApplicationStatus($select=Name),UsrRequestedProduct($select=Name),UsrContact($select=Name),UsrIndustryType($select=Name),UsrReasonForLoan($select=Name),UsrBankName($select=Name)",Id);            
            string url = apiurl + temp_url;

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
                string temp_rejectionreasonurl = string.Format("0/odata/UsrApplicationRejectionReasonRecords?$select=UsrRejectionReasonId&$filter=UsrApplication/Id eq {0}&$expand=UsrRejectionReason($select=Name)", Id);
                string rejectionreasonurl = apiurl + temp_rejectionreasonurl;
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
            SqlConnection sqlCnctn = new SqlConnection(connectionString);
            sqlCnctn.Open();
            string strQry = "Select * from UserInfo where session = '" + Session["Name"] + "'";
            SqlDataAdapter sda = new SqlDataAdapter(strQry, sqlCnctn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                
                List<string> GetCookies = Authentication();
                string apiurl = ConfigurationManager.AppSettings["apiurl"];

                // API for Gender
                string commonAPI = "0/odata/Gender?$select=Id,Name";
                string url = apiurl + commonAPI;

                JObject ParsedResponse = GET_Object(url);
                List<Gender> genderlist = new List<Gender>();

                foreach (var v in ParsedResponse["value"])
                {
                    Gender gender = new Gender()
                    {
                        id = v["Id"].ToString(),
                        name = v["Name"].ToString()
                    };
                    genderlist.Add(gender);
                }
                ViewData["GenderList"] = genderlist;

                // API for EmployeeType
                commonAPI = "0/odata/UsrTSEmploymentType?$select=Id,Name";
                url = apiurl + commonAPI;

                ParsedResponse = GET_Object(url);
                List<EmploymentType> emptypelist = new List<EmploymentType>();

                foreach (var v in ParsedResponse["value"])
                {
                    EmploymentType emptype = new EmploymentType()
                    {
                        id = v["Id"].ToString(),
                        name = v["Name"].ToString()
                    };
                    emptypelist.Add(emptype);
                }
                ViewData["EmpTypeList"] = emptypelist;

                // API for Marital Status
                commonAPI = "0/odata/UsrMaritalStatus?$select=Id,Name";
                url = apiurl + commonAPI;

                ParsedResponse = GET_Object(url);
                List<MaritalStatus> maritalstautslist = new List<MaritalStatus>();

                foreach (var v in ParsedResponse["value"])
                {
                    MaritalStatus mstatus = new MaritalStatus()
                    {
                        id = v["Id"].ToString(),
                        name = v["Name"].ToString()
                    };
                    maritalstautslist.Add(mstatus);
                }
                ViewData["MaritalList"] = maritalstautslist;

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
                    currentstreet = row["uploadedvalue"].ToString() == "true" ? "ward no, 02, shivraji post tyodhari, Umari Alias Shivraji, gram umari" : "",
                    currentlandmark = row["uploadedvalue"].ToString() == "true" ? "shivraji post tyodhari, Umari Alias Shivraji, gram umari" : "",
                    currentbuilding = row["uploadedvalue"].ToString() == "true" ? "ward no, 02, shivraji post tyodhari" : "",
                    currentcity = row["uploadedvalue"].ToString() == "true" ? "833858c0-8232-4a61-a291-3ef814afc041" : "",
                    currentstate = row["uploadedvalue"].ToString() == "true" ? "5cd2b6fb-1aac-4406-8cd2-b8f6346db5fd" : "",
                    currentpin = row["uploadedvalue"].ToString() == "true" ? "485775" : "",
                    currentcountry = row["uploadedvalue"].ToString() == "true" ? "d427eb5d-ecd2-4049-9275-420038a42bea" : "",
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
                    birthdate = row["birthdate"].ToString(),
                    appgateId = row["applicationgateId"].ToString(),
                    currentaddrsameasaadhar = row["currentaddrsameasaadhar"].ToString()
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
            string fathername = p.fathername;
            string currentstreet = !String.IsNullOrEmpty(p.currentstreet) ? "ward no, 02, shivraji post tyodhari, Umari Alias Shivraji, gram umari" : "";
            string currentlandmark = !String.IsNullOrEmpty(p.currentlandmark) ? "shivraji post tyodhari, Umari Alias Shivraji, gram umari" : "";
            string currentbuilding = !String.IsNullOrEmpty(p.currentbuilding) ? "ward no, 02, shivraji post tyodhari" : "";
            string currentcity = !String.IsNullOrEmpty(p.currentcity) ? "833858c0-8232-4a61-a291-3ef814afc041" : "";
            string currentstate = !String.IsNullOrEmpty(p.currentstate) ? "5cd2b6fb-1aac-4406-8cd2-b8f6346db5fd" : "";
            string currentpin = !String.IsNullOrEmpty(p.currentpin) ? "485775" : "";
            string currentcountry = !String.IsNullOrEmpty(p.currentcountry) ? "d427eb5d-ecd2-4049-9275-420038a42bea" : "";
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
            string currentaddrsameasaadhar = p.currentaddrsameasaadhar;
            if (dt.Rows.Count > 0)
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand cmd;
                string sql = "Update UserInfo set firstname = '" + firstname + "',currentaddrsameasaadhar = '" + currentaddrsameasaadhar + "',birthdate = '" + birthdate + "',aadharbirthdate = '" + aadharbirthdate + "',aadharaddress = '" + aadharaddress + "',aadharlastname = '" + aadharlastname + "',aadharmiddlename = '" + aadharmiddlename + "',aadharfirstname = '" + aadharfirstname + "',panbirthdate = '" + panbirthdate + "',panfathername = '" + panfathername + "',panlastname = '" + panlastname + "',panmiddlename = '" + panmiddlename + "',panfirstname = '" + panfirstname + "',currentcountry = '" + currentcountry + "',currentpin = '" + currentpin + "',currentstate = '" + currentstate + "',currentcity = '" + currentcity + "',currentbuilding = '" + currentbuilding + "',currentlandmark = '" + currentlandmark + "',currentstreet = '" + currentstreet + "',lastname = '" + lastname + "',middlename = '" + middlename + "',gender = '" + gender + "',aadharnumber = '" + aadharnumber + "',pannumber = '" + pannumber + "',maritalstatus = '" + maritalstatus + "',employmenttype = '" + employmenttype + "',fathername = '" + fathername + "' where session = '" + Session["Name"].ToString() + "'";
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
            string responseResult = "";
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
            try
            {
                IRestResponse response = client.Execute(request);

                var details = JObject.Parse(response.Content);
                
                JArray jarray = JArray.Parse(details["ParsedResults"].ToString());
                responseResult = jarray[0]["ParsedText"].ToString();
            }
            catch (Exception e) 
            {
                return responseResult;
            }
            return responseResult;
        }

        public string OCR_PAN(string path)
        {
            string getData = "";
            List<string> OutputList_PAN = new List<string>();
            string responseResult = OCR_Mechanism(path);
            if (!string.IsNullOrEmpty(responseResult))
            {
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

                
                OutputList_PAN.Add(list.ToArray()[0].ToString());  // Name
                OutputList_PAN.Add(list.ToArray()[1].ToString());   // Father's Name
                OutputList_PAN.Add((Regex.Matches(responseResult, PANNumber_Regex)[0]).ToString()); // PAN Number
                OutputList_PAN.Add((Regex.Matches(responseResult, DateOfBirth_Regex)[0]).ToString());  // Date of Birth
                getData = String.Join(";", OutputList_PAN);
                return getData;
            }
            else
            {
                return getData;
            }
            
        }

        public string OCR_AadharFront(string path)
        {
            string getData = "";
            List<string> OutputList_Aadhar = new List<string>();
            
            string responseResult = OCR_Mechanism(path);

            if (!string.IsNullOrEmpty(responseResult))
            {
                string AaadharNumber_Regex = "[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}";

                string DateOfBirth_Regex = "[0-3]{1}[0-9]{1}/[0-9]{2}/[0-9]{4}";

                String[] strArray = responseResult.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);


                OutputList_Aadhar.Add(strArray[1]); // Aadhar Name 
                OutputList_Aadhar.Add(Regex.Matches(responseResult, DateOfBirth_Regex)[0].ToString()); // Aadhar DOB            
                OutputList_Aadhar.Add(Regex.Matches(responseResult, AaadharNumber_Regex)[0].ToString()); // Aadhar Number
                getData = String.Join(";", OutputList_Aadhar);
                return getData;
            }
            else
            {
                return getData;
            }

        }

        public string OCR_AadharBack(string path)
        {

            string getData = "";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://accurascan.com/api/v4/ocr");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Api-Key", "1634211844z3Rg8F4pE31pVuNEpYpZ01O0iqwjkxylx9i9m9DR");
            //request.AddHeader("Cookie", "laravel_session=eyJpdiI6IjJqUkllcHA3ZFhuaEdZR1krN3pUVFE9PSIsInZhbHVlIjoiSVNMbDRNTDZYT1JRWW5UbjlSWnlTUTF0bXhQOStMOTVjM1lESTNDZEFTdlpocCtGSFVxeXNTall5ckFpOUY2WSIsIm1hYyI6ImExODEzNzZmNmY0MmQxNGVhMDdjMzcwNmYzZDQ1ZmM0NTZmYjRiOTVlM2Q2YmQzMDZlYmY0Y2Q3YjJmZmMzMzcifQ%3D%3D");
            request.AddCookie("Cookie", "laravel_session=eyJpdiI6IjJqUkllcHA3ZFhuaEdZR1krN3pUVFE9PSIsInZhbHVlIjoiSVNMbDRNTDZYT1JRWW5UbjlSWnlTUTF0bXhQOStMOTVjM1lESTNDZEFTdlpocCtGSFVxeXNTall5ckFpOUY2WSIsIm1hYyI6ImExODEzNzZmNmY0MmQxNGVhMDdjMzcwNmYzZDQ1ZmM0NTZmYjRiOTVlM2Q2YmQzMDZlYmY0Y2Q3YjJmZmMzMzcifQ%3D%3D");
            request.AddFile("scan_image", path);
            request.AddParameter("country_code", "IND");
            request.AddParameter("card_code", "ADHB");
            try
            {
                IRestResponse response = client.Execute(request);

                var details = JObject.Parse(response.Content);
                getData = details["data"]["OCRdata"]["Address"].ToString();
                return getData;
            }
            catch (Exception e)
            {
                return getData;
            }
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
                if (!string.IsNullOrEmpty(pan_details))
                {
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

            }

            if (files2 != null && files2.ContentLength > 0)
            {
                string AadharFront_details = OCR_AadharFront(path_AadharFront);
                if (!string.IsNullOrEmpty(AadharFront_details))
                {
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
            }

            if (files3 != null && files3.ContentLength > 0)
            {
                string getaadharaddress = OCR_AadharBack(path_AadharBack);
                if (!string.IsNullOrEmpty(getaadharaddress))
                {
                    aadharaddress = getaadharaddress;
                }
                
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
                string numberofdependents = !String.IsNullOrEmpty(data["numberofdependents"]) ? data["numberofdependents"] : "0";
                string coapplicantname = data["coapplicantname"];
                string coapplicantmobilephone = data["coapplicantmobilephone"];
                string coapplicantrelationship = !String.IsNullOrEmpty(data["coapplicantrelationship"]) ? "\"" + data["coapplicantrelationship"] + "\"" : "null";
                string nomineename = data["nomineename"];
                string nomineemobilephone = data["nomineemobilephone"];
                string nomineerelationship = !String.IsNullOrEmpty(data["nomineerelationship"]) ? "\"" + data["nomineerelationship"] + "\"" : "null";
                string bankifsccode = data["bankifsccode"];
                string bankaccountnumber = data["bankaccountnumber"];
                string bankname = data["bankname"];

                DataRow row = dt.Rows[0];
                
                string firstname = row["firstname"].ToString();
                string middlename = row["middlename"].ToString();
                string lastname = row["lastname"].ToString();
                string gender = !String.IsNullOrEmpty(row["gender"].ToString()) ? "\"" + row["gender"].ToString() + "\"" : "null";
                string aadharnumber = row["aadharnumber"].ToString();
                string maritalstatus = !String.IsNullOrEmpty(row["maritalstatus"].ToString()) ? "\"" + row["maritalstatus"].ToString() + "\"" : "null";
                string employmenttype = !String.IsNullOrEmpty(row["employmenttype"].ToString()) ? "\"" + row["employmenttype"].ToString() + "\"" : "null";
                string fathername = row["fathername"].ToString();
                string currentstreet = row["currentstreet"].ToString();
                string currentlandmark = row["currentlandmark"].ToString();
                string currentbuilding = row["currentbuilding"].ToString();
                string currentcity = !String.IsNullOrEmpty(row["currentcity"].ToString()) ? "\"" + row["currentcity"].ToString() + "\"" : "null";
                string currentstate = !String.IsNullOrEmpty(row["currentstate"].ToString()) ? "\"" + row["currentstate"].ToString() + "\"" : "null";
                string currentpin = row["currentpin"].ToString();
                string currentcountry = !String.IsNullOrEmpty(row["currentcountry"].ToString()) ? "\"" + row["currentcountry"].ToString() + "\"" : "null";
                string panfirstname = row["panfirstname"].ToString();
                string panmiddlename = row["panmiddlename"].ToString();
                string panlastname = row["panlastname"].ToString();
                string panfathername = row["panfathername"].ToString();
                string panbirthdate = row["panbirthdate"].ToString();
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

                // Api hit for step-2

                sqlCnctn.Close();
                List<string> GetCookies = Authentication();

                string apiurl = ConfigurationManager.AppSettings["apiurl"];
                
                string temp_url = string.Format("0/odata/UsrApplicationGate({0})", applicationgateId);
                string url = apiurl + temp_url;
                var client2 = new RestClient(url);
                client2.Timeout = -1;
                var request2 = new RestRequest(Method.PATCH);
                request2.AddCookie(".ASPXAUTH", GetCookies[2]);
                request2.AddCookie("BPMCSRF", GetCookies[0]);
                request2.AddCookie("BPMLOADER", GetCookies[1]);
                request2.AddCookie("UserName", GetCookies[3]);
                request2.AddHeader("Content-Type", "application/json");
                request2.AddHeader("BPMCSRF", GetCookies[0]);
                request2.AddParameter("application/json", "{\r\n    \r\n    \"UsrAction\" : \"2\",\r\n    \"UsrCoApplicantMobilePhone\": \"" + coapplicantmobilephone + "\",\r\n    \"UsrFilePathForAadhaarBack\": \"" + CorrectfilepathAadharBack + "\",\r\n    \"UsrFilePathForAadhaarFront\": \"" + CorrectfilepathAadharFront + "\",\r\n    \"UsrFilePathForPAN\": \"" + CorrectfilepathPAN + "\",\r\n    \"UsrBirthDate\": \"" + birthdate + "\",\r\n    \"UsrAadhaarDOB\": \"" + aadharbirthdate + "\",\r\n    \"UsrAadhaarAddress\": \"" + aadharaddress + "\",\r\n    \"UsrAadhaarNumber\": \"" + aadharnumber + "\",\r\n    \"UsrAadhaarFirstName\": \"" + aadharfirstname + "\",\r\n    \"UsrAadhaarMiddleName\": \"" + aadharmiddlename + "\",\r\n    \"UsrAadhaarLastName\": \"" + aadharlastname + "\",\r\n    \"UsrEmploymentTypeId\": " + employmenttype + ",\r\n    \"UsrGivenName\": \"" + firstname + "\",\r\n    \"UsrMiddleName\": \"" + middlename + "\",\r\n    \"UsrSurname\": \"" + lastname + "\",\r\n    \"UsrGenderId\": " + gender + ",\r\n    \"UsrFatherName\":\"" + fathername + "\",\r\n    \"UsrMaritalStatusId\": " + maritalstatus + ",\r\n    \"UsrNumberOfDependents\":\"" + numberofdependents + "\",\r\n    \"UsrCoApplicantName\": \"" + coapplicantname + "\",\r\n    \"UsrCoApplicantRelationshipId\": " + coapplicantrelationship + ",\r\n    \"UsrPANFirstName\":\"" + panfirstname + "\",\r\n    \"UsrPANMiddleName\":\"" + panmiddlename + "\",\r\n    \"UsrPANLastName\":\"" + panlastname + "\",\r\n    \"UsrPANFatherName\": \"" + panfathername + "\",\r\n    \"UsrPANBirthDate\": \"" + panbirthdate + "\",\r\n    \"UsrCurrentStreet\":\"" + currentstreet + "\",\r\n    \"UsrCurrentBuilding\":\"" + currentbuilding + "\",\r\n    \"UsrCurrentLandmark\":\"" + currentlandmark + "\",\r\n    \"UsrCurrentPIN\":\"" + currentpin + "\",\r\n    \"UsrCurrentStateId\": " + currentstate + ",\r\n    \"UsrCurrentCityId\": " + currentcity + ",\r\n    \"UsrCurrentCountryId\": " + currentcountry + ",\r\n    \"UsrBankIFSCCode\" : \"" + bankifsccode + "\",\r\n    \"UsrBankAccountNumber\":\"" + bankaccountnumber + "\",\r\n    \"UsrBankNameId\": \"" + bankname + "\",\r\n    \"UsrNomineeName\": \"" + nomineename + "\",\r\n    \"UsrNomineeRelationshipId\": " + nomineerelationship + ",\r\n    \"UsrNomineeMobilePhone\": \"" + nomineemobilephone + "\"\r\n   \r\n}\r\n\r\n", ParameterType.RequestBody);
                IRestResponse response2 = client2.Execute(request2);
                sqlCnctn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand cmd;
                SqlCommand cmd2;
                string sql = "Update UserInfo set step1 = 'false' where session = '" + Session["Name"].ToString() + "'";
                string sql2 = "Delete from UserSTEP1Info where step1Id = '" + applicationgateId + "'";
                cmd = new SqlCommand(sql, sqlCnctn);
                cmd2 = new SqlCommand(sql2, sqlCnctn);
                adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);
                adapter.UpdateCommand.ExecuteNonQuery();
                adapter.DeleteCommand = new SqlCommand(sql2, sqlCnctn);
                adapter.DeleteCommand.ExecuteNonQuery();
                cmd.Dispose();
                cmd2.Dispose();
                //System.Threading.Thread.Sleep(15000);

                return RedirectToAction("Applications");
            }
            else
            {
                Response.Redirect("~/index.aspx");
                return null;
            }
            
        }
        public ActionResult MakePayment()
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
                string apiurl = ConfigurationManager.AppSettings["apiurl"];                
                string temp_url = string.Format("0/odata/UsrAgreements?$select=Id,UsrName&$filter=UsrContact/UsrPANNumber eq '{0}' and (UsrAgreementStatus/Name eq 'Active' or UsrAgreementStatus/Name eq 'Partial Repayment')&$expand=UsrLoanType($select=Name)", pannumber);
                string url = apiurl + temp_url;
                JObject ParsedResponse = GET_Object(url);

                List<Agreements> list = new List<Agreements>();


                foreach (var v in ParsedResponse["value"])
                {
                    Agreements agr = new Agreements()
                    {
                        id = v["Id"].ToString(),
                        number = v["UsrName"].ToString(),
                        loantype = v["UsrLoanType"]["Name"].ToString()
                    };

                    list.Add(agr);
                }
                if (list.Count == 1)
                {
                    Payment p = new Payment()
                    {
                        id = list[0].id,
                        agrloantype = list[0].loantype
                    };
                    Fetch(p);
                    return View("MakePayment");
                }
                ViewData["AgreementData"] = list;
                ViewData["EMIRecords"] = null;
                ViewData["LoanType"] = null;
                
                return View();
            }
            else
            {
                Response.Redirect("~/index.aspx");
                return null;
            }
             
        }
        [HttpPost]
        public ActionResult Fetch(Payment p)
        {
            string agrid = p.id;
            string agrloantype = p.agrloantype;

            string dbconn = ConfigurationManager.AppSettings["dbconn"];
            string connectionString = dbconn;
            SqlConnection sqlCnctn = new SqlConnection(connectionString);
            sqlCnctn.Open();

            string strQry = "Select * from UserInfo where session = '" + Session["Name"] + "'";
            SqlDataAdapter sda = new SqlDataAdapter(strQry, sqlCnctn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DataRow row = dt.Rows[0];
            string pannumber = row["pannumber"].ToString();

            string apiurl1 = ConfigurationManager.AppSettings["apiurl"];
            string temp_url1 = string.Format("0/odata/UsrAgreements?$select=Id,UsrName&$filter=UsrContact/UsrPANNumber eq '{0}' and (UsrAgreementStatus/Name eq 'Active' or UsrAgreementStatus/Name eq 'Partial Repayment')&$expand=UsrLoanType($select=Name)", pannumber);
            string url1 = apiurl1 + temp_url1;
            JObject ParsedResponse1 = GET_Object(url1);

            List<Agreements> list1 = new List<Agreements>();


            foreach (var v in ParsedResponse1["value"])
            {
                Agreements agr1 = new Agreements()
                {
                    id = v["Id"].ToString(),
                    number = v["UsrName"].ToString(),
                    loantype = v["UsrLoanType"]["Name"].ToString()
                };

                list1.Add(agr1);
            }
            ViewData["AgreementData"] = list1;
            if (agrloantype == "Long Term Loan")
            {
                string apiurl = ConfigurationManager.AppSettings["apiurl"];                
                string temp_emiurl = string.Format("0/odata/UsrEMIRecords?$select=UsrIsRepaid,UsrDueDate,UsrStartDate,UsrAmount,UsrIsLatePaymentFeeApplied,UsrOldAmount,UsrIsExtensionFeeApplied,UsrExtensionDueDate&$filter=UsrAgreement/Id eq {0} and UsrIsRepaid eq false &$orderby=UsrStartDate asc &$expand=UsrEMIType($select = Name),UsrAgreement($select = UsrName), UsrPaymentGate($select = UsrName)", agrid);
                string emiurl = apiurl + temp_emiurl;
                JObject emiResponse = GET_Object(emiurl);

                List<EMI_Records> emi_list = new List<EMI_Records>();
                string agrname = null;

                foreach (var v in emiResponse["value"])
                {
                    agrname = v["UsrAgreement"]["UsrName"].ToString();
                    EMI_Records emir = new EMI_Records()
                    {
                        repaid = v["UsrIsRepaid"].ToString() == "False" ? "No" : "Yes",
                        amount = v["UsrAmount"].ToString(),
                        duedate = (v["UsrDueDate"].ToString()).Split(' ')[0],
                        startdate = (v["UsrStartDate"].ToString()).Split(' ')[0],
                        islatepaymentfeeapplied = v["UsrIsLatePaymentFeeApplied"].ToString() == "False" ? "No" : "Yes",
                        isextensionfeeapplied = v["UsrIsExtensionFeeApplied"].ToString() == "False" ? "No" : "Yes",
                        extensionduedate = v["UsrExtensionDueDate"].ToString(),
                        emitype = v["UsrEMIType"]["Name"].ToString(),
                        oldamount = v["UsrOldAmount"].ToString()
                        

                    };

                    emi_list.Add(emir);
                }

                ViewData["EMIRecords"] = emi_list;
                ViewData["LoanType"] = agrloantype;
                ViewData["AgreementName"] = agrname;
                ViewData["AgreementId"] = agrid;
                
            }
            else if(agrloantype == "Short Term Loan")
            {
                string apiurl = ConfigurationManager.AppSettings["apiurl"];
                string temp_url = string.Format("0/odata/UsrAgreements({0})?$select=UsrName,UsrTotalDebtAmount,UsrBalancedDebtAmount,UsrOverpaymentDebtAmount&$expand=UsrAgreementStatus($select=Name)", agrid);
                string url = apiurl + temp_url;
                JObject ParsedResponse = GET_Object(url);

                ViewData["EMIRecords"] = null;
                ViewData["LoanType"] = agrloantype;
                ViewData["AgreementId"] = agrid;
                ViewData["AgreementName"] = ParsedResponse["UsrName"].ToString();
                float balance = float.Parse(ParsedResponse["UsrBalancedDebtAmount"].ToString());
                float debt = float.Parse(ParsedResponse["UsrTotalDebtAmount"].ToString());
                float overpay = float.Parse(ParsedResponse["UsrOverpaymentDebtAmount"].ToString());
                string status = ParsedResponse["UsrAgreementStatus"]["Name"].ToString();
                if (balance == 0 && overpay == 0 && status != "Closed")
                {
                    ViewData["AmountToPay"] = (debt).ToString();
                }
                else if (balance > 0)
                {
                    ViewData["AmountToPay"] =  (balance).ToString();
                }
                
                
            }
            
            return View("MakePayment");
        }

        public ActionResult Payments()
        {
            return View();
        }

        

        [HttpPost]
        public ActionResult On_Pay(Pay p)
        {
            string agrid = p.agrid;
            float amount = float.Parse(p.amount.ToString());
            string count = p.count;
            string amountindecimal = string.Format("{0:0.000}", amount);
            List<string> GetCookies = Authentication();


            string apiurl = ConfigurationManager.AppSettings["apiurl"];
            string url = apiurl + "0/odata/UsrPaymentGate";
            var client = new RestClient(url);
            
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("BPMCSRF", GetCookies[0]);
            request.AddCookie(".ASPXAUTH", GetCookies[2]);
            request.AddCookie("BPMCSRF", GetCookies[0]);
            request.AddCookie("BPMLOADER", GetCookies[1]);
            request.AddCookie("UserName", GetCookies[3]);
            request.AddParameter("application/json", "{\r\n\r\n    \"UsrAgreementId\": \"" + agrid + "\",\r\n    \"UsrNumberOfMonthsCustomerWantsToPay\": \"" + count + "\",\r\n    \"UsrAmountPaid\":" + amountindecimal + " \r\n \r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);


            System.Threading.Thread.Sleep(10000);

            Response.Redirect("~/Home/Payment_Records");
            return View();
        }

        public ActionResult DirectPay(string AgrId, string Amnt)
        {
            
            List<string> GetCookies = Authentication();


            string apiurl = ConfigurationManager.AppSettings["apiurl"];
            string url = apiurl + "0/odata/UsrPaymentGate";
            var client = new RestClient(url);

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("BPMCSRF", GetCookies[0]);
            request.AddCookie(".ASPXAUTH", GetCookies[2]);
            request.AddCookie("BPMCSRF", GetCookies[0]);
            request.AddCookie("BPMLOADER", GetCookies[1]);
            request.AddCookie("UserName", GetCookies[3]);
            request.AddParameter("application/json", "{\r\n\r\n    \"UsrAgreementId\": \"" + AgrId + "\",\r\n    \"UsrNumberOfMonthsCustomerWantsToPay\": \"" + "1" + "\",\r\n    \"UsrAmountPaid\":" + Amnt + " \r\n \r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);


            System.Threading.Thread.Sleep(10000);

            Response.Redirect("~/Home/Payment_Records");
            return View();
        }


        public ActionResult Payment_Records()
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

                string apiurl = ConfigurationManager.AppSettings["apiurl"];
                string temp_url = string.Format("0/odata/UsrPaymentGate?$select=Id,UsrAmountPaid,CreatedOn&$filter=UsrContact/UsrPANNumber eq '{0}'&$expand=UsrLoanType($select=Name),UsrAgreement($select=UsrName)", pannumber);
                string url = apiurl + temp_url;
                JObject ParsedResponse = GET_Object(url);

                List<Payment_Records> payrecords_list = new List<Payment_Records>();                
 
                foreach (dynamic v in ParsedResponse["value"])
                {
                    Payment_Records payrecord = new Payment_Records()
                    {
                        id = v["Id"].ToString(),
                        agreement = v["UsrAgreement"]["UsrName"].ToString(),
                        amount = v["UsrAmountPaid"].ToString(),
                        loantype = v["UsrLoanType"]["Name"].ToString(),
                        paymentdate = v["CreatedOn"].ToString()
                    };
                    payrecords_list.Add(payrecord);
                }
                if (payrecords_list.Count == 0)
                {
                    ViewData["PaymentRecords"] = null;
                }
                else
                {
                    ViewData["PaymentRecords"] = payrecords_list;
                }
               
                return View();
            }
            else
            {
                Response.Redirect("~/index.aspx");
                return null;
            }
           
        }

        public ActionResult FAQ()
        {

            //API for Miscalleneous FAQ's
            string apiurl = ConfigurationManager.AppSettings["apiurl"];
            string miscId = "eeae94af-d1f8-44a6-9502-6194d8a6502a";
            string temp_url = string.Format("/0/odata/KnowledgeBase?$select=Name,NotHtmlNote&$filter=Type/Id eq {0}", miscId);
            string url = apiurl + temp_url;
            JObject ParsedResponse = GET_Object(url);

            List<FAQ> faq_list = new List<FAQ>();

            foreach (dynamic v in ParsedResponse["value"])
            {
                FAQ faq = new FAQ
                {
                    question = v["Name"].ToString(),
                    answer = v["NotHtmlNote"].ToString(),
                };
                faq_list.Add(faq);
            }

            ViewData["miscFAQ"] = faq_list;
            return View();
        }
        public ActionResult Apply_for_loan()
        {
            Response.Redirect("~/personal.aspx");
            return null;
        }

        public ActionResult LoanEligibilityCheck(string Id)
        {

            // Fetching Products
            string apiurl = ConfigurationManager.AppSettings["apiurl"];
            string temp_url_products = "0/odata/UsrProducts?$select=Id,Name";
            string url_for_prdct = apiurl + temp_url_products;
            JObject Response_for_prdct = GET_Object(url_for_prdct);
            List<Products> prdcts_list = new List<Products>();

            foreach (var v in Response_for_prdct["value"])
            {
                Products prdct = new Products()
                {
                    id = v["Id"].ToString(),
                    name = v["Name"].ToString(),
                };
                prdcts_list.Add(prdct);
            }

            ViewData["ProductsData"] = prdcts_list;
            ViewData["ProductId"] = Id;
            return View();
        }

        public ActionResult PendingSteps()
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
                string mobile = row["mobile"].ToString();
                string email = row["email"].ToString();
                string strQry2 = "Select * from UserSTEP1Info where email = '" + email + "' and mobile = '"+ mobile +"'";
                SqlDataAdapter sda2 = new SqlDataAdapter(strQry2, sqlCnctn);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);

                List<PendingSteps> pendingstepslist = new List<PendingSteps>();

                foreach (DataRow row2 in dt2.Rows)
                {
                    PendingSteps pendingstep = new PendingSteps()
                    {
                        step1id = row2["step1Id"].ToString(),
                        processed = row2["processed"].ToString(),
                        loantype = row2["loantype"].ToString(),
                        loanamount = row2["loanamount"].ToString(),
                        loanterm = row2["loanterm"].ToString(),
                        loanname = row2["loanname"].ToString(),
                        date = row2["date"].ToString(),
                        proceed = "Continue",
                        cancel = "Cancel",
                        edit = "Edit"
                    };
                    pendingstepslist.Add(pendingstep);
                }
                ViewData["PendingSteps"] = pendingstepslist;
            }

                return View();
        }

        public ActionResult CancelPendingStep(string Id)
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
                SqlCommand cmd;
                string strQry2 = "Delete from UserSTEP1Info where step1Id = '" + Id + "'";
                SqlDataAdapter sda2 = new SqlDataAdapter();
                cmd = new SqlCommand(strQry2, sqlCnctn);
                sda2.DeleteCommand = new SqlCommand(strQry2, sqlCnctn);
                sda2.DeleteCommand.ExecuteNonQuery();
                cmd.Dispose();
                return RedirectToAction("PendingSteps");
            }
            else
            {
                Response.Redirect("~/index.aspx");
                return null;
            }
        }

        public ActionResult UpdateAppGateIdOfPendingStep(string Id)
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
                SqlCommand cmd;
                string sql = "Update UserInfo set applicationgateId = '" + Id + "' where session = '" + Session["Name"].ToString() + "'";
                SqlDataAdapter sda2 = new SqlDataAdapter();
                cmd = new SqlCommand(sql, sqlCnctn);
                sda2.UpdateCommand = new SqlCommand(sql, sqlCnctn);
                sda2.UpdateCommand.ExecuteNonQuery();
                cmd.Dispose();
                return RedirectToAction("About");
            }
            else
            {
                Response.Redirect("~/index.aspx");
                return null;
            }
        }

        public ActionResult EditPendingStep(string Id)
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
                return RedirectToAction("../personal.aspx");
            }
            else
            {
                Response.Redirect("~/index.aspx");
                return null;
            }
        }

        public ActionResult GetLoanEligibilityResult(FormCollection data) 
        {
            int loanAmount = Convert.ToInt32(data["loanAmount"]);
            int monthlyIncome = Convert.ToInt32(data["monthlyIncome"]);
            int Tenure = Convert.ToInt32(data["Tenure"]);
            string productId = data["prdct_id"].ToString();
            string typeofloan = data["loan"].ToString();
            string birthdate = data["birthdate"].ToString();
            ViewData["Result"] = null;
            ViewData["Options"] = null;

            string apiurl = ConfigurationManager.AppSettings["apiurl"];
            string temp_url_products = string.Format("0/odata/UsrProducts?$select=Name,UsrMinAge,UsrMaxAge,UsrMinLoanAmount,UsrMaxLoanAmount,UsrMinLoanTermInDays,UsrMaxLoanTermInDays,UsrMinLoanTermInMonths,UsrMaxLoanTermInMonths,UsrRequiredMonthlyIncome&$filter=Id eq {0}", productId);
            string url_for_prdct = apiurl + temp_url_products;
            JObject Response_for_prdct = GET_Object(url_for_prdct);

            // get the params
            ViewData["ProductName"] = Response_for_prdct["value"][0]["Name"];
            int MinAge = Convert.ToInt32(Response_for_prdct["value"][0]["UsrMinAge"]);
            int MaxAge = Convert.ToInt32(Response_for_prdct["value"][0]["UsrMaxAge"]);
            int MinLoanAmount = Convert.ToInt32(Response_for_prdct["value"][0]["UsrMinLoanAmount"]);
            int MaxLoanAmount = Convert.ToInt32(Response_for_prdct["value"][0]["UsrMaxLoanAmount"]);
            int MinLoanTermInMonths = Convert.ToInt32(Response_for_prdct["value"][0]["UsrMinLoanTermInMonths"]);
            int MaxLoanTermInMonths = Convert.ToInt32(Response_for_prdct["value"][0]["UsrMaxLoanTermInMonths"]);
            int MinLoanTermInDays = Convert.ToInt32(Response_for_prdct["value"][0]["UsrMinLoanTermInDays"]);
            int MaxLoanTermInDays = Convert.ToInt32(Response_for_prdct["value"][0]["UsrMaxLoanTermInDays"]);
            int RequiredMonthlyIncome = Convert.ToInt32(Response_for_prdct["value"][0]["UsrRequiredMonthlyIncome"]);

            //calculate Age
            DateTime dob = Convert.ToDateTime(birthdate);
            int age = CalculateAge(dob);
            //get product parameters from API Call
            if (typeofloan == "Short Term Loan")
            {
                if (age > MinAge && age < MaxAge && loanAmount > MinLoanAmount && loanAmount < MaxLoanAmount && monthlyIncome > RequiredMonthlyIncome && Tenure > MinLoanTermInDays && Tenure < MaxLoanTermInDays)
                {
                    ViewData["Result"] = "true";
                }
            }
            else
            {
                if (age > MinAge && age < MaxAge && loanAmount > MinLoanAmount && loanAmount < MaxLoanAmount && monthlyIncome > RequiredMonthlyIncome && Tenure > MinLoanTermInMonths && Tenure < MaxLoanTermInMonths)
                {
                    ViewData["Result"] = "true";
                }
            }
            //compare parameters
            if (ViewData["Result"] == null && typeofloan == "Short Term Loan")
            {
                string optionalprdurl = string.Format("/0/odata/UsrProducts?$select=Id,Name&$filter=UsrMinAge lt {0} and UsrMaxAge gt {0} and UsrMinLoanAmount lt {1} and UsrMaxLoanAmount gt {1} and UsrRequiredMonthlyIncome lt {2} and UsrMinLoanTermInDays lt {3} and UsrMaxLoanTermInDays gt {3}", age,loanAmount,monthlyIncome,Tenure);
                string optionsurl = apiurl + optionalprdurl;
                JObject response_of_Optional_product = GET_Object(optionsurl);

                List<Products> product_list = new List<Products>();
                foreach (var v in response_of_Optional_product["value"])
                {
                    Products prd = new Products()
                    {
                        id = v["Id"].ToString(),
                        name = v["Name"].ToString()
                    };
                    product_list.Add(prd);
                }
                ViewData["Options"] = product_list;
            }
            else if(ViewData["Result"] == null && typeofloan == "Long Term Loan")
            {
                string optionalprdurl = string.Format("/0/odata/UsrProducts?$select=Id,Name&$filter=UsrMinAge lt {0} and UsrMaxAge gt {0} and UsrMinLoanAmount lt {1} and UsrMaxLoanAmount gt {1} and UsrRequiredMonthlyIncome lt {2} and UsrMinLoanTermInMonths lt {3} and UsrMaxLoanTermInMonths gt {3}", age, loanAmount, monthlyIncome, Tenure);
                string optionsurl = apiurl + optionalprdurl;
                JObject response_of_Optional_product = GET_Object(optionsurl);

                List<Products> product_list = new List<Products>();
                foreach (var v in response_of_Optional_product["value"])
                {
                    Products prd = new Products()
                    {
                        id = v["Id"].ToString(),
                        name = v["Name"].ToString()
                    };
                    product_list.Add(prd);
                }
                ViewData["Options"] = product_list;
            }

            string temp_product = "0/odata/UsrProducts?$select=Id,Name";
            string url_prdct = apiurl + temp_product;
            JObject Response_prdct = GET_Object(url_prdct);
            List<Products> prdcts_list = new List<Products>();

            foreach (var v in Response_prdct["value"])
            {
                Products prdct = new Products()
                {
                    id = v["Id"].ToString(),
                    name = v["Name"].ToString(),
                };
                prdcts_list.Add(prdct);
            }

            ViewData["ProductsData"] = prdcts_list;
            return View("LoanEligibilityCheck");
        }

        public int CalculateAge(DateTime dob) 
        {
            int age = 0;
            age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear)
            { age = age - 1; }

            return age;
        }
    }
}