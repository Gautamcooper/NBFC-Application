﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace NBFC_App___dev
{
    public partial class personal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Name"] == null)
                {
                    Response.Redirect("~/index.aspx");
                }
                else
                {
                    TextBox3.Text = "16000";
                    //string connectionString = @"Data Source=DESKTOP-HLC3FB7\SQLEXPRESS;Initial Catalog=UserData;Integrated Security=false;User id=Admin;password=Admin@123";
                    //string connectionString = @"Data Source=DESKTOP-CV6742D;Initial Catalog=UserData;Integrated Security=false;User id=Akshit;password=Akshit";
                    string dbconn = ConfigurationManager.AppSettings["dbconn"];
                    string connectionString = dbconn;
                    SqlConnection sqlCnctn = new SqlConnection(connectionString);
                    sqlCnctn.Open();

                    //Session["Name"] = Guid.NewGuid().ToString();
                    //SqlDataAdapter adapter = new SqlDataAdapter();
                    string strQry = "Select * from Userinfo where session='" + Session["Name"] + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(strQry, sqlCnctn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        TextBox1.Text = dt.Rows[0]["mobile"].ToString();
                        TextBox2.Text = dt.Rows[0]["email"].ToString();
                        FullName.Text = dt.Rows[0]["fullname"].ToString();
                        PAN.Text = dt.Rows[0]["pannumber"].ToString();
                    }
                    else
                    {

                    }
                    sqlCnctn.Close();
                }
            }
            
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string shorttermloan = "0";
            string longtermloan = "0";
            if (Loan_type.SelectedValue == "short")
            {
                shorttermloan = shortterm.SelectedValue.ToString();
            }
            else if (Loan_type.SelectedValue == "long")
            {
                longtermloan = longterm.SelectedValue.ToString();
            }
            string mobile = TextBox1.Text.ToString();
            string email = TextBox2.Text.ToString();
            string pan_number = PAN.Text.ToString();
            string loanamount = TextBox3.Text.ToString();
            string monthly_income = Monthly_income.Text.ToString();
            string product_val = Product.SelectedValue.ToString();
            string industry_type = Industry_type.SelectedValue.ToString();
            string reason = Reason.SelectedValue.ToString();
            string bpmcsrf = "";
            string bpmloader = "";
            string aspxauth = "";
            string username = "";
            string loanterm = "";


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
            var client2 = new RestClient("http://localhost:92/0/odata/UsrApplicationGate");
            client2.Timeout = -1;
            client2.MaxRedirects = 10;
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("BPMCSRF", bpmcsrf);
            request2.AddHeader("Content-Type", "application/json");
            request2.AddCookie(".ASPXAUTH", aspxauth);
            request2.AddCookie("BPMCSRF", bpmcsrf);
            request2.AddCookie("BPMLOADER", bpmloader);
            request2.AddCookie("UserName", username);
            request2.AddHeader("Cookie", ".ASPXAUTH=" + aspxauth + "; BPMCSRF=" + bpmcsrf + "; BPMLOADER=" + bpmloader + "; UserName=" + username + "");
            request2.AddParameter("application/json", "{\r\n    \"UsrAction\": \"1\", \r\n    \"UsrLoanAmountRequested\": \"" + loanamount + "\",\r\n    \"UsrPANNumber\":\"" + pan_number + "\",\r\n    \"UsrShortTermLoanRequested\":\"" + shorttermloan + "\",\r\n    \"UsrLongTermLoanRequested\":\"" + longtermloan + "\",\r\n     \"UsrProductId\":\"" + product_val + "\",\r\n     \"UsrMonthlyIncome\":\"" + monthly_income + "\",\r\n     \"UsrEmail\":\"" + email + "\",\r\n    \"UsrMobileNumber\": \"" + mobile + "\",\r\n    \"UsrReasonForLoanId\":\"" + reason + "\",\r\n    \"UsrIndustryTypeId\":\"" + industry_type + "\"  \r\n}", ParameterType.RequestBody);
            IRestResponse response2 = client2.Execute(request2);
            var createdRecordId = JObject.Parse(response2.Content);
            string dbconn = ConfigurationManager.AppSettings["dbconn"];
            string connectionString = dbconn;
            SqlConnection sqlCnctn = new SqlConnection(connectionString);
            sqlCnctn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd;
            //string sql = "Update UserInfo set step1 = 'true', pannumber = '"+ pan_number + "',applicationgateId = '" + createdRecordId["Id"] + "' where session = '" + Session["Name"].ToString() + "'";
            string sql = "Update UserInfo set step1 = 'true', pannumber = '" + pan_number + "' where session = '" + Session["Name"].ToString() + "'";
            cmd = new SqlCommand(sql, sqlCnctn);
            adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);
            adapter.UpdateCommand.ExecuteNonQuery();
            cmd.Dispose();

            string way = Request.Cookies["User"].Value;
            if (way == "login")
            {
                System.Threading.Thread.Sleep(50000);
            }            
            

            Response.Redirect("~/Home/About");


        }
    }
}
