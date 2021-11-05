using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBFC_App___dev
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string way = "login";
                if (Request.Cookies["User"] != null)
                {
                    way = Request.Cookies["User"].Value;
                }
                else
                {
                    System.Web.HttpCookie UserCookie = new System.Web.HttpCookie("User");
                    UserCookie.Value = "login";
                    UserCookie.Expires = DateTime.Now.AddMinutes(60);
                    Response.Cookies.Add(UserCookie);
                }
                temp_data.Text = way;
                if (way == "login")
                {
                    fullname.Visible = false;
                    Button1.Text = "Login";
                }
                else if (way == "signup")
                {
                    fullname.Visible = true;
                    Button1.Text = "Signup";
                }
                next_clicked.Text = "false";
            }
            else
            {
                System.Web.HttpCookie UserCookie = new System.Web.HttpCookie("User");
                UserCookie.Value = "login";
                UserCookie.Expires = DateTime.Now.AddMinutes(60);
                Response.Cookies.Add(UserCookie);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            next_clicked.Text = "true";
            SqlDataAdapter adapter = new SqlDataAdapter();
            Random otp_num_random = new Random();
            string otp_num = otp_num_random.Next(100000, 999999).ToString();
            string dbconn = ConfigurationManager.AppSettings["dbconn"];
            string connectionString = dbconn;
            SqlConnection sqlCnctn = new SqlConnection(connectionString);
            sqlCnctn.Open();
            Session["Name"] = Guid.NewGuid().ToString();
            string mobile = mnumber.Text.ToString();
            string emailid = email.Text.ToString();
            
            //check if already exists or not
            string strQry = "Select * from Userinfo where mobile='" + mobile + "' and email='" + emailid + "'";
            SqlDataAdapter sda = new SqlDataAdapter(strQry, sqlCnctn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string way = Request.Cookies["User"].Value;
            if (way == "signup")
            {
                string fulln = fullname.Text.ToString();
                if (dt.Rows.Count > 0)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert Message", "alert('We already have a user with this Info. Please Login'); window.location='" + Request.ApplicationPath + "index.aspx';", true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", , true);                    
                }
                else
                {
                    string query_l = "INSERT INTO UserInfo (email,mobile,session,fullname,otp) values ('" + emailid + "','" + mobile + "','" + Session["Name"] + "','" + fulln + "','" + otp_num + "')";
                    SqlCommand command = new SqlCommand(query_l, sqlCnctn);
                    adapter.InsertCommand = new SqlCommand(query_l, sqlCnctn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    command.Dispose();
                    // Authentication
                    string bpmcsrf = "";
                    string bpmloader = "";
                    string aspxauth = "";
                    string username = "";


                    string apiurl = ConfigurationManager.AppSettings["apiurl"];
                    string temp = apiurl + "ServiceModel/AuthService.svc/Login";
                    var client = new RestClient(temp);
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

                    string url = apiurl + "0/odata/Lead";
                    var client2 = new RestClient(url);
                    client2.Timeout = -1;
                    client2.MaxRedirects = 10;
                    var request2 = new RestRequest(Method.POST);
                    request2.AddHeader("BPMCSRF", bpmcsrf);
                    request2.AddHeader("Content-Type", "application/json");
                    request2.AddCookie(".ASPXAUTH", aspxauth);
                    request2.AddCookie("BPMCSRF", bpmcsrf);
                    request2.AddCookie("BPMLOADER", bpmloader);
                    request2.AddCookie("UserName", username);
                    request2.AddParameter("application/json", "{\r\n    \"Contact\": \"" + fulln + "\",\r\n    \"UsrOTP\": \"" + otp_num + "\",\r\n    \"Email\": \"" + emailid + "\",\r\n    \"MobilePhone\":\"" + mobile + "\"\r\n\r\n}", ParameterType.RequestBody);
                    client2.Execute(request2);
                }               
            }
            else
            {
                if (dt.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert Message", "alert('We do not find any User with this Info. Please Signup'); window.location='" + Request.ApplicationPath + "index.aspx';", true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", , true);                    
                }
                else
                {
                    string query = "Update UserInfo set session='" + Session["Name"] + "',otp='" + otp_num + "',step1= 'false', step2= 'false' where mobile='" + mobile + "'and email = '" + emailid + "'";
                    adapter.InsertCommand = new SqlCommand(query, sqlCnctn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    adapter.Dispose();
                }
                
            }                       
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string dbconn = ConfigurationManager.AppSettings["dbconn"];
            string connectionString = dbconn;
            SqlConnection sqlCnctn = new SqlConnection(connectionString);
            sqlCnctn.Open();
            string strQry = "Select * from UserInfo where session='" + Session["Name"] + "'";
            SqlDataAdapter sda = new SqlDataAdapter(strQry, sqlCnctn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string otp = row["otp"].ToString();
                if (otp == OTP.Text.ToString())
                {
                    Response.Redirect("~/Home/Homepage");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter the correct OTP')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('We do not have any record with these details. kindly signup')", true);
            }
            sqlCnctn.Close();
        }                   
        protected void login_button_click(object sender, EventArgs e)
        {
            System.Web.HttpCookie UserCookie = new System.Web.HttpCookie("User");
            UserCookie.Value = "login";
            UserCookie.Expires = DateTime.Now.AddMinutes(60);
            Response.Cookies.Add(UserCookie);
            Response.Redirect("~/index.aspx");
        }
        protected void signup_button_click(object sender, EventArgs e)
        {
            System.Web.HttpCookie UserCookie = new System.Web.HttpCookie("User");
            UserCookie.Value = "signup";
            UserCookie.Expires = DateTime.Now.AddMinutes(60);
            Response.Cookies.Add(UserCookie);
            Response.Redirect("~/index.aspx");
        }
    }
}