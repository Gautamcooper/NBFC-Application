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

namespace NBFC_App___dev.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
            //string connectionString = @"Data Source=DESKTOP-HLC3FB7\SQLEXPRESS;Initial Catalog=UserData;Integrated Security=false;User id=Admin;password=Admin@123";
            //string connectionString = @"Data Source=DESKTOP-CV6742D;Initial Catalog=UserData;Integrated Security=false;User id=Akshit;password=Akshit";
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

        //public string OCR_AadharBack(string path)
        //{
            
            

        //        var client = new RestClient("https://accurascan.com/api/v4/ocr");
        //        client.Timeout = -1;
        //        var request = new RestRequest(Method.POST);
        //        request.AddHeader("Api-Key", "1615142527yNEfcvD5u3BTj5IsM8Gk6W4xZ1i9kdMhuSHHMgFv");
        //    //request.AddHeader("Cookie", "laravel_session=eyJpdiI6IjJqUkllcHA3ZFhuaEdZR1krN3pUVFE9PSIsInZhbHVlIjoiSVNMbDRNTDZYT1JRWW5UbjlSWnlTUTF0bXhQOStMOTVjM1lESTNDZEFTdlpocCtGSFVxeXNTall5ckFpOUY2WSIsIm1hYyI6ImExODEzNzZmNmY0MmQxNGVhMDdjMzcwNmYzZDQ1ZmM0NTZmYjRiOTVlM2Q2YmQzMDZlYmY0Y2Q3YjJmZmMzMzcifQ%3D%3D");
        //        request.AddCookie("Cookie", "laravel_session=eyJpdiI6IjJqUkllcHA3ZFhuaEdZR1krN3pUVFE9PSIsInZhbHVlIjoiSVNMbDRNTDZYT1JRWW5UbjlSWnlTUTF0bXhQOStMOTVjM1lESTNDZEFTdlpocCtGSFVxeXNTall5ckFpOUY2WSIsIm1hYyI6ImExODEzNzZmNmY0MmQxNGVhMDdjMzcwNmYzZDQ1ZmM0NTZmYjRiOTVlM2Q2YmQzMDZlYmY0Y2Q3YjJmZmMzMzcifQ%3D%3D");
        //        request.AddFile("scan_image", path);
        //        request.AddParameter("country_code", "IND");
        //        request.AddParameter("card_code", "ADHB");
        //        IRestResponse response = client.Execute(request);

        //        var details = JObject.Parse(response.Content);

        //        return details["data"]["OCRdata"]["Address"].ToString();
            
        //}
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
           // var aadharaddress = "";
            var aadharbirthdate = "";
            var aadharnumber = "";


            var path_PAN = "";
            var path_AadharFront = "";
            //var path_AadharBack = "";
            int f1 = 0;
            int f2 = 0;
            //int f3 = 0;
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
                //if (files3 != null && files3.ContentLength > 0)
                //{
                //    var fileName = Path.GetFileName(files3.FileName);
                //    var extension = Path.GetExtension(files3.FileName);
                //    path_AadharBack = "D:\\Uploads\\AadharBack\\" + email + extension;

                //    files3.SaveAs(path_AadharBack);
                //    f3 = 1;
                //}

            }
            sqlCnctn.Close();
            
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
            else {
                panfirstname = strArray_PAN_nameSplit[0];
                panmiddlename = strArray_PAN_nameSplit[1];
                panlastname = strArray_PAN_nameSplit[2];
            };
            
            panfathername = strArray_PAN[1];
            var pan_birthdate = Convert.ToDateTime(strArray_PAN[3], System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);

            panbirthdate = pan_birthdate.ToString("yyyy-MM-dd");


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

            //aadharaddress = OCR_AadharBack(path_AadharBack);


            sqlCnctn.Open();
            strQry = "Select * from UserInfo where session = '" + Session["Name"] + "'";
            sda = new SqlDataAdapter(strQry, sqlCnctn);
            dt = new DataTable();
            sda.Fill(dt);           
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string uploadedval = row["uploadedvalue"].ToString();
                if(uploadedval == "false" && f1 == 1 && f2 == 1 )
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd;
                    string sql = "Update UserInfo set uploadedvalue = 'true',filepathAadharFront = '" + path_AadharFront + "',filepathPAN = '" + path_PAN + "',aadharnumber = '" + aadharnumber + "',aadharbirthdate = '" + aadharbirthdate + "',aadharmiddlename = '" + aadharmiddlename + "',aadharlastname = '" + aadharlastname + "',aadharfirstname = '" + aadharfirstname + "',pannumber = '" + pannumber + "', panbirthdate = '" + panbirthdate + "', panfathername = '" + panfathername + "', panlastname = '" + panlastname + "', panmiddlename = '" + panmiddlename + "', panfirstname = '" + panfirstname + "' where session = '" + Session["Name"].ToString() + "'";
                    cmd = new SqlCommand(sql, sqlCnctn);
                    adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);

                    adapter.UpdateCommand.ExecuteNonQuery();
                    cmd.Dispose();
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
                string CorrectfilepathPAN = filepathPAN.Replace(@"\", @"\\");
                string CorrectfilepathAadharFront = filepathAadharFront.Replace(@"\", @"\\");
                // Api hit for step-2 s

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

                string url = string.Format("http://localhost:92/0/odata/UsrApplicationGate({0})", applicationgateId);
                var client2 = new RestClient(url);
                client2.Timeout = -1;
                var request2 = new RestRequest(Method.PATCH);
                request2.AddHeader("BPMCSRF", bpmcsrf);
                request2.AddCookie(".ASPXAUTH", aspxauth);
                request2.AddCookie("BPMCSRF", bpmcsrf);
                request2.AddCookie("BPMLOADER", bpmloader);
                request2.AddCookie("UserName", username);
                request2.AddHeader("Content-Type", "application/json");
               // request2.AddHeader("Cookie", "BPMSESSIONID=51ldz41mtbuqxz1jjg3p4vwp; .ASPXAUTH=3E394F2748EFE7521FBE7F573EEC4CF7F4A8628E003960313141E2E503827EAB43C274FE658DF00F4734F66C5FC02B898EC7AA673C8E35C11BC37314EB02857CE3F09B65FECCB55F49DAC2F653BC7074E5FB2920831755CAFD58AEA3724B490D9FA19FE53419DAC6B4F9CC7ACCCC8D2F0C2446E9B50E3341EA01F28E9EDA821F758641FAA2AE4F1BFD5EB622C86837705B738802BA58A6326A1C02C14D94BBCB795085A1594FA0F8BD09997D5A6E28354F19E5A2D4F70EEB177C87656ADAB50CEE061F3C747DB70E9887C6899F63C98885FCAA990C9600E21A8A9C5217E227CCF95380B098CA43B690F126CE7DC266B6D036ECB6557418135B19F2AED8991589A7A15C49046D6C4C946D1C7718DA2144AC4F82246ED68E047D445D90C0AFA5DB62D8E6989B4FEC2FBA361992D29C665507E5A8DAF5E0A3C86B65A216AA32B7CF391D8B99AE5ED52A9632AF5E80924E5F0022396DE5AF7A27ECFDD2A3CF887613B6CDC919; BPMCSRF=RlMlsgX2n8C9JRjK1owIOu; BPMLOADER=zby4bc3aw2qebkrpakmcpunu; UserName=83|117|112|101|114|118|105|115|111|114");
                request2.AddParameter("application/json", "{\r\n    \r\n    \"UsrAction\" : \"2\",\r\n    \"UsrCoApplicantMobilePhone\": \"" + coapplicantmobilephone + "\",\r\n    \"UsrFilePathForAadharFront\": \"" + CorrectfilepathAadharFront + "\",\r\n    \"UsrFilePathForPAN\": \"" + CorrectfilepathPAN + "\",\r\n    \"UsrBirthDate\": \"" + birthdate + "\",\r\n    \"UsrAadhaarDOB\": \"" + aadharbirthdate + "\",\r\n    \"UsrAadhaarAddress\": \"" + aadharaddress + "\",\r\n    \"UsrAadhaarNumber\": \"" + aadharnumber + "\",\r\n    \"UsrAadhaarFirstName\": \"" + aadharfirstname + "\",\r\n    \"UsrAadhaarMiddleName\": \"" + aadharmiddlename + "\",\r\n    \"UsrAadhaarLastName\": \"" + aadharlastname + "\",\r\n    \"UsrEmploymentTypeId\": \"" + employmenttype + "\",\r\n    \"UsrGivenName\": \"" + firstname + "\",\r\n    \"UsrMiddleName\": \"" + middlename + "\",\r\n    \"UsrSurname\": \"" + lastname + "\",\r\n    \"UsrGenderId\":\"" + gender + "\",\r\n    \"UsrFatherName\":\"" + fathername + "\",\r\n    \"UsrSpouseName\":\"" + spousename + "\",\r\n    \"UsrMaritalStatusId\":\"" + maritalstatus + "\",\r\n    \"UsrNumberOfDependents\":\"" + numberofdependents + "\",\r\n    \"UsrCoApplicantName\": \"" + coapplicantname + "\",\r\n    \"UsrCoApplicantRelationshipId\": \"" + coapplicantrelationship + "\",\r\n    \"UsrPANFirstName\":\"" + panfirstname + "\",\r\n    \"UsrPANMiddleName\":\"" + panmiddlename + "\",\r\n    \"UsrPANLastName\":\"" + panlastname + "\",\r\n    \"UsrPANFatherName\": \"" + panfathername + "\",\r\n    \"UsrPANBirthDate\": \"" + panbirthdate + "\",\r\n    \"UsrCurrentStreet\":\"" + currentstreet + "\",\r\n    \"UsrCurrentBuilding\":\"" + currentbuilding + "\",\r\n    \"UsrCurrentLandmark\":\"" + currentlandmark + "\",\r\n    \"UsrCurrentPIN\":\"" + currentpin + "\",\r\n    \"UsrCurrentStateId\":\"" + currentstate + "\",\r\n    \"UsrCurrentCityId\":\"" + currentcity + "\",\r\n    \"UsrCurrentCountryId\":\"" + currentcountry + "\",\r\n    \"UsrBankIFSCCode\" : \"" + bankifsccode + "\",\r\n    \"UsrBankAccountNumber\":\"" + bankaccountnumber + "\",\r\n    \"UsrBankNameId\": \"" + bankname + "\"     \r\n}\r\n\r\n", ParameterType.RequestBody);
                IRestResponse response2 = client2.Execute(request2);
                

                return RedirectToAction("Index");
            }
            else
            {
                Response.Redirect("~/index.aspx");
                return null;
            }
            
        }


    }
}