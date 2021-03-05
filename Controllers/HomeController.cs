using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NBFC_App___dev.Models;

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
            string connectionString = @"Data Source=DESKTOP-HLC3FB7\SQLEXPRESS;Initial Catalog=UserData;Integrated Security=false;User id=Admin;password=Admin@123";
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
                    uploadedvalue = row["uploadedvalue"].ToString()
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
            string connectionString = @"Data Source=DESKTOP-HLC3FB7\SQLEXPRESS;Initial Catalog=UserData;Integrated Security=false;User id=Admin;password=Admin@123";
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
            if (dt.Rows.Count > 0)
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand cmd;
                string sql = "Update UserInfo set firstname = '" + firstname + "',panbirthdate = '" + panbirthdate + "',panfathername = '" + panfathername + "',panlastname = '" + panlastname + "',panmiddlename = '" + panmiddlename + "',panfirstname = '" + panfirstname + "',currentcountry = '" + currentcountry + "',currentpin = '" + currentpin + "',currentstate = '" + currentstate + "',currentcity = '" + currentcity + "',currentbuilding = '" + currentbuilding + "',currentlandmark = '" + currentlandmark + "',currentstreet = '" + currentstreet + "',lastname = '" + lastname + "',middlename = '" + middlename + "',gender = '" + gender + "',aadharnumber = '" + aadharnumber + "',pannumber = '" + pannumber + "',maritalstatus = '" + maritalstatus + "',employmenttype = '" + employmenttype + "',fathername = '" + fathername + "',spousename = '" + spousename + "' where session = '" + Session["Name"].ToString() + "'";
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

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase files,HttpPostedFileBase files2)
        {
            int f1 = 0;
            int f2 = 0;           
            if (files != null && files.ContentLength > 0)
            {                
                var fileName = Path.GetFileName(files.FileName);
                var path = "D:/Uploads/" + fileName;
                files.SaveAs(path);
                f1 = 1;
            }
            if (files2 != null && files2.ContentLength > 0)
            {                
                var fileName = Path.GetFileName(files2.FileName);               
                var path = "D:/Uploads/" + fileName;
                files2.SaveAs(path);
                f2 = 1;
            }
            string connectionString = @"Data Source=DESKTOP-HLC3FB7\SQLEXPRESS;Initial Catalog=UserData;Integrated Security=false;User id=Admin;password=Admin@123";
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
                string uploadedval = row["uploadedvalue"].ToString();
                if(uploadedval == "false" && f1 == 1 && f2 == 1)
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd;
                    string sql = "Update UserInfo set uploadedvalue = 'true' where session = '" + Session["Name"].ToString() + "'";
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
    }
}