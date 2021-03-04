using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace NBFC_App___dev
{
    public partial class _default : System.Web.UI.Page
    {
        string id_var = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string way = Request.Cookies["User"].Value;
                if(way == "login")
                {
                    temp_data.Text = "login";
                    fullname.Visible = false;
                }
                else
                {
                    temp_data.Text = "signup";
                    fullname.Visible = true;
                }
                //TextBox3.Text = "16000";
            }
            
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //string connectionString = @"Data Source=DESKTOP-HLC3FB7\SQLEXPRESS;Initial Catalog=UserData;Integrated Security=false;User id=Admin;password=Admin@123";
            //SqlConnection sqlCnctn = new SqlConnection(connectionString);
            //sqlCnctn.Open();
            //string mobile = TextBox1.Text.ToString();
            //string email = TextBox2.Text.ToString();
            //string loanamount = Loan_Amount.Text.ToString();
            //string loanterm = Loan_Term.Text.ToString();
            //string monthly_income = Monthly_income.Text.ToString();
            //string product_val = Product.SelectedValue.ToString();
            //string industry_type = Industry_type.SelectedValue.ToString();
            //string reason = Reason.SelectedValue.ToString();
            //string bpmcsrf = "";
            //string bpmloader = "";
            //string aspxauth = "";
            //string username = "";


            //var client = new RestClient("http://localhost:92/ServiceModel/AuthService.svc/Login");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("Accept", "application/json");
            //request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("application/json", "{\r\n    \"UserName\": \"Supervisor\",\r\n    \"UserPassword\": \"Supervisor\"\r\n}", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            //foreach (var c in response.Cookies)
            //{
            //    if (c.Name.ToString() == "BPMCSRF")
            //    {
            //        bpmcsrf = c.Value.ToString();
            //    }
            //    else if (c.Name.ToString() == "BPMLOADER")
            //    {
            //        bpmloader = c.Value.ToString();
            //    }
            //    else if (c.Name.ToString() == ".ASPXAUTH")
            //    {
            //        aspxauth = c.Value.ToString();
            //    }
            //    else if (c.Name.ToString() == "UserName")
            //    {
            //        username = c.Value.ToString();
            //    }
            //}

            //var client2 = new RestClient("http://localhost:92/0/odata/UsrApplicationGate");
            //client2.Timeout = -1;
            //client2.MaxRedirects = 10;
            //var request2 = new RestRequest(Method.POST);
            //request2.AddHeader("BPMCSRF", bpmcsrf);
            //request2.AddHeader("Content-Type", "application/json");
            //request2.AddCookie(".ASPXAUTH", aspxauth);
            //request2.AddCookie("BPMCSRF", bpmcsrf);
            //request2.AddCookie("BPMLOADER", bpmloader);
            //request2.AddCookie("UserName", username);
            //request2.AddHeader("Cookie", ".ASPXAUTH=" + aspxauth + "; BPMCSRF=" + bpmcsrf + "; BPMLOADER=" + bpmloader + "; UserName=" + username + "");
            //request2.AddParameter("application/json", "{\r\n    \"UsrTSAction\": \"1\", \r\n    \"UsrTSLoanAmountRequested\": \"" + loanamount + "\",\r\n    \"UsrTSLoanTermRequested\":\"" + loanterm + "\",\r\n     \"UsrProductId\":\"" + product_val + "\",\r\n     \"UsrTSMonthlyIncome\":\"" + monthly_income + "\",\r\n     \"UsrTSEmail\":\"" + email + "\",\r\n    \"UsrTSMobileNumber\": \"" + mobile + "\",\r\n    \"UsrReasonForLoanId\":\"" + reason + "\",\r\n    \"UsrTSIndustryTypeId\":\"" + industry_type + "\"  \r\n}", ParameterType.RequestBody);
            //IRestResponse response2 = client2.Execute(request2);
            //Console.WriteLine(response2.Content);
            //Session["Name"] = Guid.NewGuid().ToString();
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //string strQry = "Select * from Userinfo where mobile='" + mobile + "' or email='" + email + "'";
            //SqlDataAdapter sda = new SqlDataAdapter(strQry, sqlCnctn);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    SqlCommand cmd;
            //    string sql = "Update UserInfo set session='" + Session["Name"] + "' where mobile='" + mobile + "'";
            //    cmd = new SqlCommand(sql, sqlCnctn);
            //    adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);
            //    adapter.UpdateCommand.ExecuteNonQuery();
            //    cmd.Dispose();
            //    Response.Redirect("~/Home/Index");
            //}
            //else
            //{
            //    string query = "INSERT INTO UserInfo (email,mobile,session) values ('" + email + "','" + mobile + "','" + Session["Name"] + "')";
            //    SqlCommand command = new SqlCommand(query, sqlCnctn);
            //    adapter.InsertCommand = new SqlCommand(query, sqlCnctn);
            //    adapter.InsertCommand.ExecuteNonQuery();
            //    command.Dispose();
            //    Response.Redirect("~/Home/About");
            //}
            //sqlCnctn.Close();
        }
        protected void Button2_Click(object sender, EventArgs e)
        {           
            //TextBox1.Attributes.Add("readonly", "readonly");
            //TextBox2.Attributes.Add("readonly", "readonly");
            id_var = "Button2";
            idvar.Text = id_var;
            //string loan = TextBox3.Text.ToString();
            //string mobile = mnumber.Text.ToString();
            //string emailid = email.Text.ToString();
            //TextBox1.Text = mobile.ToString();
            //TextBox2.Text = emailid.ToString();
            //mnumber.Text = mobile;
            //email.Text = emailid;
            //Loan_Amount.Text = loan;

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-HLC3FB7\SQLEXPRESS;Initial Catalog=UserData;Integrated Security=false;User id=Admin;password=Admin@123";
            //string connectionString = @"Data Source=DESKTOP-CV6742D;Initial Catalog=UserData;Integrated Security=false;User id=Akshit;password=Akshit";
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
                    id_var = "Button3";
                    idvar.Text = id_var;
                    Session["repeat"] = "true";
                    SqlCommand cmd;
                    string sql = "Update UserInfo set session='" + Session["Name"] + "' where mobile='" + mobile + "'and email = '" + emailid + "'";
                    cmd = new SqlCommand(sql, sqlCnctn);
                    adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    cmd.Dispose();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('We do not have any record with these details. kindly signup')", true);
                }
            }
            else if(OTP.Text.ToString() == "1234" && temp_data.Text == "signup")
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
                    id_var = "Button3";
                    idvar.Text = id_var;
                    Session["Name"] = Guid.NewGuid().ToString();
                    Session["repeat"] = "false";
                    string query = "INSERT INTO UserInfo (email,mobile,session,fullname) values ('" + emailid + "','" + mobile + "','" + Session["Name"] + "','" + fulln + "')";
                    SqlCommand command = new SqlCommand(query, sqlCnctn);
                    adapter.InsertCommand = new SqlCommand(query, sqlCnctn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    command.Dispose();
                }               
            }
            sqlCnctn.Close();            
        }
        protected void Apply_for_Loan_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/personal.aspx");
        }

        protected void Go_to_dashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Home/About");
        }
    }
}