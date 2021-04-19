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
                temp_data.Text = way;
                if (way == "login")
                {
                    fullname.Visible = false;
                }
                else if (way == "signup")
                {
                    fullname.Visible = true;
                }
                next_clicked.Text = "false";
            }            
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            next_clicked.Text = "true";
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string dbconn = ConfigurationManager.AppSettings["dbconn"];
            string connectionString = dbconn;
            SqlConnection sqlCnctn = new SqlConnection(connectionString);
            sqlCnctn.Open();
            if (OTP.Text.ToString() == "1234" && temp_data.Text == "login")
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                string mobile = mnumber.Text.ToString();
                string emailid = email.Text.ToString();
                //string fulln = fullname.Text.ToString();
                string strQry = "Select * from Userinfo where mobile='" + mobile + "' and email='" + emailid + "'";
                SqlDataAdapter sda = new SqlDataAdapter(strQry, sqlCnctn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["Name"] = Guid.NewGuid().ToString();
                    //id_var = "Button3";
                    //idvar.Text = id_var;
                    Session["repeat"] = "true";
                    SqlCommand cmd;
                    string sql = "Update UserInfo set session='" + Session["Name"] + "' where mobile='" + mobile + "'and email = '" + emailid + "'";
                    cmd = new SqlCommand(sql, sqlCnctn);
                    adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    cmd.Dispose();
                    Response.Redirect("~/Home/Products");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('We do not have any record with these details. kindly signup')", true);
                }
            }
            else if (OTP.Text.ToString() == "1234" && temp_data.Text == "signup")
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                string mobile = mnumber.Text.ToString();
                string emailid = email.Text.ToString();
                string fulln = fullname.Text.ToString();
                string strQry = "Select * from Userinfo where mobile='" + mobile + "' and email='" + emailid + "'";
                SqlDataAdapter sda = new SqlDataAdapter(strQry, sqlCnctn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('We already have a user with these details. kindly sign in')", true);
                }
                else
                {
                    //id_var = "Button3";
                    //idvar.Text = id_var;
                    Session["Name"] = Guid.NewGuid().ToString();
                    Session["repeat"] = "false";
                    string query = "INSERT INTO UserInfo (email,mobile,session,fullname) values ('" + emailid + "','" + mobile + "','" + Session["Name"] + "','" + fulln + "')";
                    SqlCommand command = new SqlCommand(query, sqlCnctn);
                    adapter.InsertCommand = new SqlCommand(query, sqlCnctn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    command.Dispose();

                    // Create a record in Lead
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
                    request2.AddParameter("application/json", "{\r\n    \"Contact\": \"" + fulln + "\",\r\n    \"Email\": \"" + emailid + "\",\r\n    \"MobilePhone\":\"" + mobile + "\"\r\n\r\n}", ParameterType.RequestBody);
                    client2.Execute(request2);


                    Response.Redirect("~/Home/Products");
                }
            }
            sqlCnctn.Close();
        }
        protected void login_button_click(object sender, EventArgs e)
        {
            System.Web.HttpCookie UserCookie = new System.Web.HttpCookie("User");
            UserCookie.Value = "login";
            UserCookie.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Add(UserCookie);
            Response.Redirect("~/index.aspx");
        }
        protected void signup_button_click(object sender, EventArgs e)
        {
            System.Web.HttpCookie UserCookie = new System.Web.HttpCookie("User");
            UserCookie.Value = "signup";
            UserCookie.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Add(UserCookie);
            Response.Redirect("~/index.aspx");
        }
    }
}