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
                string temp_url_for_agrmnt = string.Format("0/odata/UsrAgreements?$select=Id,UsrName&$filter=UsrContact/UsrPANNumber eq '{0}'", pannumber);
                string url_for_agrmnt = apiurl + temp_url_for_agrmnt;
                JObject Response_for_agrmnt = GET_Object(url_for_agrmnt);
                List<Agreements> list = new List<Agreements>();

                foreach (var v in Response_for_agrmnt["value"])
                {
                    Agreements agr = new Agreements()
                    {
                        id = v["Id"].ToString(),                  
                        number = v["UsrName"].ToString(),                        
                    };
                    list.Add(agr);
                }
                ViewData["AgreementData"] = list;

                // Fetching applications
                string temp_url_applications = string.Format("0/odata/UsrApplications?$select=Id,UsrName&$filter=UsrContact/UsrPANNumber eq '{0}'", pannumber);
                string url_for_apln= apiurl + temp_url_applications;
                JObject Response_for_apln = GET_Object(url_for_apln);
                List<Applications> apln_list = new List<Applications>();

                foreach (var v in Response_for_apln["value"])
                {
                    Applications apln = new Applications()
                    {
                        id = v["Id"].ToString(),
                        number = v["UsrName"].ToString(),
                    };
                    apln_list.Add(apln);
                }
                ViewData["ApplicationData"] = apln_list;

                // Fetching Products
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
            List<string> GetCookies = Authentication();
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

                if (!string.IsNullOrEmpty(pannumber))
                { //API for Repeat Customer
                    
                    var client = new RestClient("http://localhost:98/0/odata/UsrCustomerQueries");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddCookie("BPMCSRF", GetCookies[0]);
                    request.AddCookie(".ASPXAUTH", GetCookies[2]);
                    request.AddCookie("BPMLOADER", GetCookies[1]);
                    request.AddCookie("UserName", GetCookies[3]);
                    request.AddHeader("BPMCSRF", GetCookies[0]);
                    request.AddHeader("Content-Type", "application/json");
                    //request.AddHeader("Cookie", "BPMSESSIONID=y0c13r4uocmzvbq4dgwuretr; .ASPXAUTH=555114D3A7888B5FB7988150BF980B0A91763BD8AE6D73E3D023B2E0491C1A5336F54FB5BFE60809F44521186926085FF184AF8D448D030B9418A0A9D68179D8C0F3E416391223A52549728D8E646FCEC13E2CAE1AD66E71FD2EBFEE551651039D7A620E4CBEAE88EB523E0506F349A783EBAF959C3B73F16F967EBF094C8876732E4EF5C7BF3F05AB7C509434251AABD0BD23B0C15D9557E9C712EFC7AC092A80B7B49D38A181FA5DC411059F2BE34A57BF5C03D2D8E0F77710C2C861387C54A29ADB8E67A19D8DE698FF93C66F366FFDDFDC3F3493E53DB03B79257B7A32C2601C1BAD33689F421FB9AA5F64C870ACD8C4E7C648FADF0AA97A28A7E50240EE8326962557843DE3E0434CB7F038DAE040B45824FA5F86B56F8C5F5D1952A103F73687B13EAC01355B996499CCA99191F11D1091AAE9B8D0F43E4736208FC6E80EFCC6A77D6B009C70E0D22DBDDEAAFB9722DA8BE3A7AE280B1EB42CD0D1BD1281AD0C89; BPMCSRF=YD1erO56tQdxXidGsYNkgu; BPMLOADER=rcunk3fmvpbddlkoo05xmsnh; UserName=83|117|112|101|114|118|105|115|111|114");
                    request.AddParameter("application/json", "{\r\n\r\n    \"UsrNotes\" :  \"" + query + "\",\r\n    \"UsrContactNotExists\" : false,\r\n    \"UsrPANNumber\" : \"" + pannumber + "\"\r\n\r\n}", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                }
                else if (string.IsNullOrEmpty(pannumber))
                {

                    var client = new RestClient("http://localhost:98/0/odata/UsrCustomerQueries");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddCookie("BPMCSRF", GetCookies[0]);
                    request.AddCookie(".ASPXAUTH", GetCookies[2]);
                    request.AddCookie("BPMLOADER", GetCookies[1]);
                    request.AddCookie("UserName", GetCookies[3]);
                    request.AddHeader("BPMCSRF", GetCookies[0]);
                    request.AddHeader("Content-Type", "application/json");
                    //request.AddHeader("Cookie", "BPMSESSIONID=y0c13r4uocmzvbq4dgwuretr; .ASPXAUTH=555114D3A7888B5FB7988150BF980B0A91763BD8AE6D73E3D023B2E0491C1A5336F54FB5BFE60809F44521186926085FF184AF8D448D030B9418A0A9D68179D8C0F3E416391223A52549728D8E646FCEC13E2CAE1AD66E71FD2EBFEE551651039D7A620E4CBEAE88EB523E0506F349A783EBAF959C3B73F16F967EBF094C8876732E4EF5C7BF3F05AB7C509434251AABD0BD23B0C15D9557E9C712EFC7AC092A80B7B49D38A181FA5DC411059F2BE34A57BF5C03D2D8E0F77710C2C861387C54A29ADB8E67A19D8DE698FF93C66F366FFDDFDC3F3493E53DB03B79257B7A32C2601C1BAD33689F421FB9AA5F64C870ACD8C4E7C648FADF0AA97A28A7E50240EE8326962557843DE3E0434CB7F038DAE040B45824FA5F86B56F8C5F5D1952A103F73687B13EAC01355B996499CCA99191F11D1091AAE9B8D0F43E4736208FC6E80EFCC6A77D6B009C70E0D22DBDDEAAFB9722DA8BE3A7AE280B1EB42CD0D1BD1281AD0C89; BPMCSRF=YD1erO56tQdxXidGsYNkgu; BPMLOADER=rcunk3fmvpbddlkoo05xmsnh; UserName=83|117|112|101|114|118|105|115|111|114");
                    request.AddParameter("application/json", "{\r\n\r\n    \"UsrNotes\" :  \"" + query + "\",\r\n    \"UsrContactNotExists\" : true,\r\n    \"UsrEmail\" : \"" + email + "\",\r\n    \"UsrCustomerName\" : \"" + fullname + "\"\r\n\r\n}", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                }

            }
            return RedirectToAction("Queries");
        }



      

        [HttpPost]
        public ActionResult Submit_application(FormCollection data)
        {
            string query = data["application"].ToString();
            string apln_id = data["apln_id"].ToString();
            List<string> GetCookies = Authentication();

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

                if (!string.IsNullOrEmpty(pannumber))
                { //API for Repeat Customer
                    
                    var client = new RestClient("http://localhost:98/0/odata/UsrCustomerQueries");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddCookie("BPMCSRF", GetCookies[0]);
                    request.AddCookie(".ASPXAUTH", GetCookies[2]);
                    request.AddCookie("BPMLOADER", GetCookies[1]);
                    request.AddCookie("UserName", GetCookies[3]);
                    request.AddHeader("BPMCSRF", GetCookies[0]);
                    request.AddHeader("Content-Type", "application/json");
                    //request.AddHeader("Cookie", "BPMSESSIONID=y0c13r4uocmzvbq4dgwuretr; .ASPXAUTH=555114D3A7888B5FB7988150BF980B0A91763BD8AE6D73E3D023B2E0491C1A5336F54FB5BFE60809F44521186926085FF184AF8D448D030B9418A0A9D68179D8C0F3E416391223A52549728D8E646FCEC13E2CAE1AD66E71FD2EBFEE551651039D7A620E4CBEAE88EB523E0506F349A783EBAF959C3B73F16F967EBF094C8876732E4EF5C7BF3F05AB7C509434251AABD0BD23B0C15D9557E9C712EFC7AC092A80B7B49D38A181FA5DC411059F2BE34A57BF5C03D2D8E0F77710C2C861387C54A29ADB8E67A19D8DE698FF93C66F366FFDDFDC3F3493E53DB03B79257B7A32C2601C1BAD33689F421FB9AA5F64C870ACD8C4E7C648FADF0AA97A28A7E50240EE8326962557843DE3E0434CB7F038DAE040B45824FA5F86B56F8C5F5D1952A103F73687B13EAC01355B996499CCA99191F11D1091AAE9B8D0F43E4736208FC6E80EFCC6A77D6B009C70E0D22DBDDEAAFB9722DA8BE3A7AE280B1EB42CD0D1BD1281AD0C89; BPMCSRF=YD1erO56tQdxXidGsYNkgu; BPMLOADER=rcunk3fmvpbddlkoo05xmsnh; UserName=83|117|112|101|114|118|105|115|111|114");
                    request.AddParameter("application/json", "{\r\n\r\n    \"UsrNotes\" :  \"" + query + "\",\r\n    \"UsrContactNotExists\" : false,\r\n    \"UsrPANNumber\" : \"" + pannumber + "\",\r\n    \"UsrApplicationId\" : \"" + apln_id + "\"\r\n\r\n}", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    //hello Dear

                }
                else if (string.IsNullOrEmpty(pannumber))
                {
                    
                    var client = new RestClient("http://localhost:98/0/odata/UsrCustomerQueries");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddCookie("BPMCSRF", GetCookies[0]);
                    request.AddCookie(".ASPXAUTH", GetCookies[2]);
                    request.AddCookie("BPMLOADER", GetCookies[1]);
                    request.AddCookie("UserName", GetCookies[3]);
                    request.AddHeader("BPMCSRF", GetCookies[0]);
                    request.AddHeader("Content-Type", "application/json");
                    //request.AddHeader("Cookie", "BPMSESSIONID=y0c13r4uocmzvbq4dgwuretr; .ASPXAUTH=555114D3A7888B5FB7988150BF980B0A91763BD8AE6D73E3D023B2E0491C1A5336F54FB5BFE60809F44521186926085FF184AF8D448D030B9418A0A9D68179D8C0F3E416391223A52549728D8E646FCEC13E2CAE1AD66E71FD2EBFEE551651039D7A620E4CBEAE88EB523E0506F349A783EBAF959C3B73F16F967EBF094C8876732E4EF5C7BF3F05AB7C509434251AABD0BD23B0C15D9557E9C712EFC7AC092A80B7B49D38A181FA5DC411059F2BE34A57BF5C03D2D8E0F77710C2C861387C54A29ADB8E67A19D8DE698FF93C66F366FFDDFDC3F3493E53DB03B79257B7A32C2601C1BAD33689F421FB9AA5F64C870ACD8C4E7C648FADF0AA97A28A7E50240EE8326962557843DE3E0434CB7F038DAE040B45824FA5F86B56F8C5F5D1952A103F73687B13EAC01355B996499CCA99191F11D1091AAE9B8D0F43E4736208FC6E80EFCC6A77D6B009C70E0D22DBDDEAAFB9722DA8BE3A7AE280B1EB42CD0D1BD1281AD0C89; BPMCSRF=YD1erO56tQdxXidGsYNkgu; BPMLOADER=rcunk3fmvpbddlkoo05xmsnh; UserName=83|117|112|101|114|118|105|115|111|114");
                    request.AddParameter("application/json", "{\r\n\r\n    \"UsrNotes\" :  \"" + query + "\",\r\n    \"UsrContactNotExists\" : true,\r\n    \"UsrEmail\" : \"" + email + "\",\r\n    \"UsrCustomerName\" : \"" + fullname + "\",\r\n    \"UsrApplicationId\" : \"" + apln_id + "\"\r\n\r\n}", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                }


            }

            return RedirectToAction("Queries");
        }

        [HttpPost]
        public ActionResult Submit_agreement(FormCollection data)
        {
            string query = data["agreement"].ToString();
            string agreement_id = data["agr_id"].ToString();
            List<string> GetCookies = Authentication();
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

                if (!string.IsNullOrEmpty(pannumber))
                { //API for Repeat Customer
                    
                    var client = new RestClient("http://localhost:98/0/odata/UsrCustomerQueries");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddCookie("BPMCSRF", GetCookies[0]);
                    request.AddCookie(".ASPXAUTH", GetCookies[2]);
                    request.AddCookie("BPMLOADER", GetCookies[1]);
                    request.AddCookie("UserName", GetCookies[3]);
                    request.AddHeader("BPMCSRF", GetCookies[0]);
                    request.AddHeader("Content-Type", "application/json");
                    //request.AddHeader("Cookie", "BPMSESSIONID=y0c13r4uocmzvbq4dgwuretr; .ASPXAUTH=555114D3A7888B5FB7988150BF980B0A91763BD8AE6D73E3D023B2E0491C1A5336F54FB5BFE60809F44521186926085FF184AF8D448D030B9418A0A9D68179D8C0F3E416391223A52549728D8E646FCEC13E2CAE1AD66E71FD2EBFEE551651039D7A620E4CBEAE88EB523E0506F349A783EBAF959C3B73F16F967EBF094C8876732E4EF5C7BF3F05AB7C509434251AABD0BD23B0C15D9557E9C712EFC7AC092A80B7B49D38A181FA5DC411059F2BE34A57BF5C03D2D8E0F77710C2C861387C54A29ADB8E67A19D8DE698FF93C66F366FFDDFDC3F3493E53DB03B79257B7A32C2601C1BAD33689F421FB9AA5F64C870ACD8C4E7C648FADF0AA97A28A7E50240EE8326962557843DE3E0434CB7F038DAE040B45824FA5F86B56F8C5F5D1952A103F73687B13EAC01355B996499CCA99191F11D1091AAE9B8D0F43E4736208FC6E80EFCC6A77D6B009C70E0D22DBDDEAAFB9722DA8BE3A7AE280B1EB42CD0D1BD1281AD0C89; BPMCSRF=YD1erO56tQdxXidGsYNkgu; BPMLOADER=rcunk3fmvpbddlkoo05xmsnh; UserName=83|117|112|101|114|118|105|115|111|114");
                    request.AddParameter("application/json", "{\r\n\r\n    \"UsrNotes\" :  \"" + query + "\",\r\n    \"UsrContactNotExists\" : false,\r\n    \"UsrPANNumber\" : \"" + pannumber + "\",\r\n    \"UsrAgreementId\" : \"" + agreement_id + "\"\r\n\r\n}", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    
                }
                else if (string.IsNullOrEmpty(pannumber))
                {

                    var client = new RestClient("http://localhost:98/0/odata/UsrCustomerQueries");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddCookie("BPMCSRF", GetCookies[0]);
                    request.AddCookie(".ASPXAUTH", GetCookies[2]);
                    request.AddCookie("BPMLOADER", GetCookies[1]);
                    request.AddCookie("UserName", GetCookies[3]);
                    request.AddHeader("BPMCSRF", GetCookies[0]);
                    request.AddHeader("Content-Type", "application/json");
                    //request.AddHeader("Cookie", "BPMSESSIONID=y0c13r4uocmzvbq4dgwuretr; .ASPXAUTH=555114D3A7888B5FB7988150BF980B0A91763BD8AE6D73E3D023B2E0491C1A5336F54FB5BFE60809F44521186926085FF184AF8D448D030B9418A0A9D68179D8C0F3E416391223A52549728D8E646FCEC13E2CAE1AD66E71FD2EBFEE551651039D7A620E4CBEAE88EB523E0506F349A783EBAF959C3B73F16F967EBF094C8876732E4EF5C7BF3F05AB7C509434251AABD0BD23B0C15D9557E9C712EFC7AC092A80B7B49D38A181FA5DC411059F2BE34A57BF5C03D2D8E0F77710C2C861387C54A29ADB8E67A19D8DE698FF93C66F366FFDDFDC3F3493E53DB03B79257B7A32C2601C1BAD33689F421FB9AA5F64C870ACD8C4E7C648FADF0AA97A28A7E50240EE8326962557843DE3E0434CB7F038DAE040B45824FA5F86B56F8C5F5D1952A103F73687B13EAC01355B996499CCA99191F11D1091AAE9B8D0F43E4736208FC6E80EFCC6A77D6B009C70E0D22DBDDEAAFB9722DA8BE3A7AE280B1EB42CD0D1BD1281AD0C89; BPMCSRF=YD1erO56tQdxXidGsYNkgu; BPMLOADER=rcunk3fmvpbddlkoo05xmsnh; UserName=83|117|112|101|114|118|105|115|111|114");
                    request.AddParameter("application/json", "{\r\n\r\n    \"UsrNotes\" :  \"" + query + "\",\r\n    \"UsrContactNotExists\" : true,\r\n    \"UsrEmail\" : \"" + email + "\",\r\n    \"UsrCustomerName\" : \"" + fullname + "\",\r\n    \"UsrAgreementId\" : \"" + agreement_id + "\"\r\n\r\n}", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                }

            }

            return RedirectToAction("Queries");
        }

        [HttpPost]
        public ActionResult Submit_product(FormCollection data)
        {
            string query = data["product"].ToString();  
            string prdct_id = data["prdct_id"].ToString();
            string ser_type = data["type_id"].ToString();
            List<string> GetCookies = Authentication();
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

                if (!string.IsNullOrEmpty(pannumber))
                { //API for Repeat Customer
                    
                    var client = new RestClient("http://localhost:98/0/odata/UsrCustomerQueries");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddCookie("BPMCSRF", GetCookies[0]);
                    request.AddCookie(".ASPXAUTH", GetCookies[2]);
                    request.AddCookie("BPMLOADER", GetCookies[1]);
                    request.AddCookie("UserName", GetCookies[3]);
                    request.AddHeader("BPMCSRF", GetCookies[0]);
                    request.AddHeader("Content-Type", "application/json");
                    //request.AddHeader("Cookie", "BPMSESSIONID=y0c13r4uocmzvbq4dgwuretr; .ASPXAUTH=555114D3A7888B5FB7988150BF980B0A91763BD8AE6D73E3D023B2E0491C1A5336F54FB5BFE60809F44521186926085FF184AF8D448D030B9418A0A9D68179D8C0F3E416391223A52549728D8E646FCEC13E2CAE1AD66E71FD2EBFEE551651039D7A620E4CBEAE88EB523E0506F349A783EBAF959C3B73F16F967EBF094C8876732E4EF5C7BF3F05AB7C509434251AABD0BD23B0C15D9557E9C712EFC7AC092A80B7B49D38A181FA5DC411059F2BE34A57BF5C03D2D8E0F77710C2C861387C54A29ADB8E67A19D8DE698FF93C66F366FFDDFDC3F3493E53DB03B79257B7A32C2601C1BAD33689F421FB9AA5F64C870ACD8C4E7C648FADF0AA97A28A7E50240EE8326962557843DE3E0434CB7F038DAE040B45824FA5F86B56F8C5F5D1952A103F73687B13EAC01355B996499CCA99191F11D1091AAE9B8D0F43E4736208FC6E80EFCC6A77D6B009C70E0D22DBDDEAAFB9722DA8BE3A7AE280B1EB42CD0D1BD1281AD0C89; BPMCSRF=YD1erO56tQdxXidGsYNkgu; BPMLOADER=rcunk3fmvpbddlkoo05xmsnh; UserName=83|117|112|101|114|118|105|115|111|114");
                    request.AddParameter("application/json", "{\r\n\r\n    \"UsrNotes\" :  \"" + query + "\",\r\n    \"UsrContactNotExists\" : false,\r\n    \"UsrPANNumber\" : \"" + pannumber + "\",\r\n    \"UsrProductId\" : \"" + prdct_id + "\"\r\n\r\n}", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                }
                else if (string.IsNullOrEmpty(pannumber))
                {

                    var client = new RestClient("http://localhost:98/0/odata/UsrCustomerQueries");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddCookie("BPMCSRF", GetCookies[0]);
                    request.AddCookie(".ASPXAUTH", GetCookies[2]);
                    request.AddCookie("BPMLOADER", GetCookies[1]);
                    request.AddCookie("UserName", GetCookies[3]);
                    request.AddHeader("BPMCSRF", GetCookies[0]);
                    request.AddHeader("Content-Type", "application/json");
                    //request.AddHeader("Cookie", "BPMSESSIONID=y0c13r4uocmzvbq4dgwuretr; .ASPXAUTH=555114D3A7888B5FB7988150BF980B0A91763BD8AE6D73E3D023B2E0491C1A5336F54FB5BFE60809F44521186926085FF184AF8D448D030B9418A0A9D68179D8C0F3E416391223A52549728D8E646FCEC13E2CAE1AD66E71FD2EBFEE551651039D7A620E4CBEAE88EB523E0506F349A783EBAF959C3B73F16F967EBF094C8876732E4EF5C7BF3F05AB7C509434251AABD0BD23B0C15D9557E9C712EFC7AC092A80B7B49D38A181FA5DC411059F2BE34A57BF5C03D2D8E0F77710C2C861387C54A29ADB8E67A19D8DE698FF93C66F366FFDDFDC3F3493E53DB03B79257B7A32C2601C1BAD33689F421FB9AA5F64C870ACD8C4E7C648FADF0AA97A28A7E50240EE8326962557843DE3E0434CB7F038DAE040B45824FA5F86B56F8C5F5D1952A103F73687B13EAC01355B996499CCA99191F11D1091AAE9B8D0F43E4736208FC6E80EFCC6A77D6B009C70E0D22DBDDEAAFB9722DA8BE3A7AE280B1EB42CD0D1BD1281AD0C89; BPMCSRF=YD1erO56tQdxXidGsYNkgu; BPMLOADER=rcunk3fmvpbddlkoo05xmsnh; UserName=83|117|112|101|114|118|105|115|111|114");
                    request.AddParameter("application/json", "{\r\n\r\n    \"UsrNotes\" :  \"" + query + "\",\r\n    \"UsrContactNotExists\" : true,\r\n    \"UsrEmail\" : \"" + email + "\",\r\n    \"UsrCustomerName\" : \"" + fullname + "\",\r\n    \"UsrProductId\" : \"" + prdct_id + "\"\r\n\r\n}", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                }

            }
            return RedirectToAction("Queries");
        }

        public ActionResult Queries()
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
                if (!string.IsNullOrEmpty(pannumber))
                {
                    string temp_url = string.Format("0/odata/UsrCustomerQueries?$select=Id,UsrName,UsrNotes&$filter=UsrPANNumber eq '{0}'&$expand=UsrQueryStatus($select=Name),UsrProduct($select=Name),UsrAgreement($select=UsrName),UsrApplication($select=UsrName)", pannumber);
                    string url = apiurl + temp_url;
                    JObject ParsedResponse = GET_Object(url);


                    List<Queries> list = new List<Queries>();


                    foreach (var v in ParsedResponse["value"])
                    {
                        Queries qry = new Queries()
                        {
                            id = v["Id"].ToString(),
                            ticketnumber = v["UsrName"].ToString(),
                            query = v["UsrNotes"].ToString(),
                            status = v["UsrQueryStatus"]["Name"].ToString()
                        };

                        if (!string.IsNullOrEmpty(v["UsrProduct"]["Name"].ToString()))
                        {
                            qry.regarding = v["UsrProduct"]["Name"].ToString();
                        }
                        else if (!string.IsNullOrEmpty(v["UsrAgreement"]["UsrName"].ToString()))
                        {
                            qry.regarding = v["UsrAgreement"]["UsrName"].ToString();
                        }
                        else if (!string.IsNullOrEmpty(v["UsrApplication"]["UsrName"].ToString()))
                        {
                            qry.regarding = v["UsrApplication"]["UsrName"].ToString();
                        }
                        else
                        {
                            qry.regarding = "General";
                        }


                        list.Add(qry);
                    }

                    ViewData["QueryData"] = list;
                }

                return View();
            }
            else
            {
                Response.Redirect("~/index.aspx");
                return null;
            }
           
        }

        public ActionResult QueryInfo(string Id)
        {
            return View();
        }
    }
}