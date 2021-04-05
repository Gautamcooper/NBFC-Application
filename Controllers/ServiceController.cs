using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NBFC_App___dev.Models;
using RestSharp;

namespace NBFC_App___dev.Controllers
{
    public class ServiceController : Controller
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
        // GET: Service
        public ActionResult ServiceCentre()
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
                string email = row["email"].ToString();
                string mobile = row["mobile"].ToString();
                string fullname = row["fullname"].ToString();
                string pannumber = row["pannumber"].ToString();                
                ViewData["email"] = email;
                ViewData["mobile"] = mobile;
                ViewData["fullname"] = fullname;
                // Fetching agreements
                string apiurl = ConfigurationManager.AppSettings["apiurl"];
                string temp_url = string.Format("0/odata/UsrAgreements?$select=Id,UsrName&$filter=UsrContact/UsrPANNumber eq '{0}'&$expand=UsrAgreementStatus($select=Name),UsrProducts($select=Name)", pannumber);
                string url = apiurl + temp_url;
                JObject ParsedResponse = GET_Object(url);
                List<Agreements> list = new List<Agreements>();

                foreach (var v in ParsedResponse["value"])
                {
                    Agreements agr = new Agreements()
                    {
                        id = v["Id"].ToString(),                  
                        number = v["UsrName"].ToString(),                        
                    };
                    list.Add(agr);
                }
                ViewData["AgreementData"] = list;
                return View();
            }
            else
            {
                Response.Redirect("~/index.aspx");
                ViewData["email"] = null;
                ViewData["mobile"] = null;
                ViewData["fullname"] = null;
                return null;
            }                      
        }

        [HttpPost]
        public ActionResult Submit_general(FormCollection data)
        {
            string query = data["general"].ToString();
            return RedirectToAction("ServiceCentre");
        }

        [HttpPost]
        public ActionResult Submit_application(FormCollection data)
        {
            string query = data["application"].ToString();
            return RedirectToAction("ServiceCentre");
        }

        [HttpPost]
        public ActionResult Submit_agreement(FormCollection data)
        {
            string query = data["agreement"].ToString();
            return RedirectToAction("ServiceCentre");
        }

        [HttpPost]
        public ActionResult Submit_product(FormCollection data)
        {
            string query = data["product"].ToString();
            return RedirectToAction("ServiceCentre");
        }
    }
}