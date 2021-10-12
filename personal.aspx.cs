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
using Newtonsoft.Json.Linq;

namespace NBFC_App___dev
{
    public partial class personal : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Page lastpage = (Page)Context.Handler;
            if (!IsPostBack)
            {
                if (Session["Name"] == null)
                {
                    Response.Redirect("~/index.aspx");
                }
                else
                {
                    TextBox3.Text = "16000";
                    string pan_num = "";
                    string appGate = "";
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
                        pan_num = dt.Rows[0]["pannumber"].ToString();
                        PAN.Text = pan_num;
                        appGate = dt.Rows[0]["applicationgateId"].ToString();
                    }
                    else
                    {

                    }
                    if(Request.Cookies["ProductId"] != null)
                    {
                        Product.SelectedValue = Request.Cookies["ProductId"].Value;
                    }                   
                    sqlCnctn.Close();
                    

                    string apiurl = ConfigurationManager.AppSettings["apiurl"];


                    string url = apiurl + "0/odata/UsrLongTermProductDuration?$select=Name, Id&$orderby=Name asc";
                    JObject ParsedResponse = GET_Object(url);
                    longterm.Items.Add(new ListItem("Number of Months", "-1"));
                    foreach (var v in ParsedResponse["value"])
                    {
                        longterm.Items.Add(new ListItem(v["Name"].ToString().TrimStart('0') + " Months", v["Name"].ToString().TrimStart('0')));
                    }

                    url = apiurl + "0/odata/UsrShortTermProductDuration?$select=Name, Id&$orderby=Name asc";
                    ParsedResponse = GET_Object(url);
                    shortterm.Items.Add(new ListItem("Number of Days", "-1"));
                    foreach (var v in ParsedResponse["value"])
                    {
                        shortterm.Items.Add(new ListItem(v["Name"].ToString().TrimStart('0') + " Days", v["Name"].ToString().TrimStart('0')));
                    }

                    url = apiurl + "0/odata/UsrProducts?$select=Id,Name";
                    ParsedResponse = GET_Object(url);
                    Product.Items.Add(new ListItem("Select Loan Type", "-1"));
                    foreach (var v in ParsedResponse["value"])
                    {
                        Product.Items.Add(new ListItem(v["Name"].ToString(), v["Id"].ToString()));
                    }
                    
                    url = apiurl + "0/odata/UsrReasonForLoan?$select=Id,Name";
                    ParsedResponse = GET_Object(url);
                    Reason.Items.Add(new ListItem("Select Reason", "-1"));
                    foreach (var v in ParsedResponse["value"])
                    {
                        Reason.Items.Add(new ListItem(v["Name"].ToString(), v["Id"].ToString()));
                    }

                    if (!String.IsNullOrEmpty(pan_num) && !String.IsNullOrEmpty(appGate))
                    {
                        string sqlQuery = "Select loantype, loanterm, loanname, loanamount, monthlyIncome, reasonforloan from UserSTEP1Info where step1Id = '" + appGate + "'";
                        SqlDataAdapter sdadtr = new SqlDataAdapter(sqlQuery, sqlCnctn);
                        DataTable data_tbl = new DataTable();
                        sdadtr.Fill(data_tbl);
                        if (data_tbl.Rows.Count > 0)
                        {
                            Loan_type.SelectedItem.Text = data_tbl.Rows[0]["loantype"].ToString();

                            if (data_tbl.Rows[0].ItemArray[0].ToString() == "Long Term")
                            {
                                longterm.SelectedItem.Text = data_tbl.Rows[0]["loanterm"].ToString() + " Months";
                            }
                            else
                            {
                                shortterm.SelectedItem.Text = data_tbl.Rows[0]["loanterm"].ToString() + " Days";
                            }
                            Product.SelectedItem.Text = data_tbl.Rows[0]["loanname"].ToString();
                            TextBox3.Text = data_tbl.Rows[0]["loanamount"].ToString();
                            Monthly_income.Text = data_tbl.Rows[0]["monthlyIncome"].ToString();
                            Reason.SelectedItem.Text = data_tbl.Rows[0]["reasonforloan"].ToString();

                        }
                    }
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
            string fullname = FullName.Text.ToString();
            string pan_number = PAN.Text.ToString();
            string loanamount = TextBox3.Text.ToString();
            string monthly_income = Monthly_income.Text.ToString();
            string product_val = Product.SelectedValue.ToString();
            string productname = Product.SelectedItem.ToString();
            //string industry_type = Industry_type.SelectedValue.ToString();
            string reason = Reason.SelectedValue.ToString();
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
            
            string url = apiurl + "0/odata/UsrApplicationGate";
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
            //request2.AddHeader("Cookie", ".ASPXAUTH=" + aspxauth + "; BPMCSRF=" + bpmcsrf + "; BPMLOADER=" + bpmloader + "; UserName=" + username + "");
            request2.AddParameter("application/json", "{\r\n    \"UsrAction\": \"1\", \r\n    \"UsrLoanAmountRequested\": \"" + loanamount + "\",\r\n    \"UsrPANNumber\":\"" + pan_number + "\",\r\n    \"UsrIsLostCustomer\": true,\r\n    \"UsrShortTermLoanRequested\":\"" + shorttermloan + "\",\r\n    \"UsrLongTermLoanRequested\":\"" + longtermloan + "\",\r\n     \"UsrFullName\":\"" + fullname + "\",\r\n     \"UsrProductId\":\"" + product_val + "\",\r\n     \"UsrMonthlyIncome\":\"" + monthly_income + "\",\r\n     \"UsrEmail\":\"" + email + "\",\r\n    \"UsrMobileNumber\": \"" + mobile + "\",\r\n    \"UsrReasonForLoanId\":\"" + reason + "\"  \r\n}", ParameterType.RequestBody);
            IRestResponse response2 = client2.Execute(request2);
            var createdRecordId = JObject.Parse(response2.Content);
            string dbconn = ConfigurationManager.AppSettings["dbconn"];
            string date = createdRecordId["CreatedOn"].ToString();
            string connectionString = dbconn;
            SqlConnection sqlCnctn = new SqlConnection(connectionString);
            sqlCnctn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd;
            string sql = "Update UserInfo set step1 = 'true', pannumber = '"+ pan_number + "',applicationgateId = '" + createdRecordId["Id"] + "' where session = '" + Session["Name"].ToString() + "'";
            cmd = new SqlCommand(sql, sqlCnctn);
            adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);
            adapter.UpdateCommand.ExecuteNonQuery();
            cmd.Dispose();
            string loanterm = "";
            if (shorttermloan != "0")
            {
                loanterm = shorttermloan;
            }
            else { loanterm = longtermloan; }
            SqlConnection sqlCnctn2 = new SqlConnection(connectionString);
            sqlCnctn2.Open();
            SqlDataAdapter adapter2 = new SqlDataAdapter();
            SqlCommand cmd2;
            string sqlcmd2 = "Insert Into UserSTEP1Info (step1Id,processed,loantype,loanterm,date,loanname,email,mobile,loanamount,monthlyIncome,reasonforloan) values('" + createdRecordId["Id"].ToString() + "','false','" + Loan_type.SelectedItem.ToString() + "','" + longtermloan + "','" + date + "','" + productname + "','" + email + "','" + mobile + "','" + loanamount + "','" + monthly_income + "','" + Reason.SelectedItem.ToString() + "')"; cmd2 = new SqlCommand(sqlcmd2, sqlCnctn2);
            adapter2.UpdateCommand = new SqlCommand(sqlcmd2, sqlCnctn2);
            adapter2.UpdateCommand.ExecuteNonQuery();
            cmd2.Dispose();

            string way = Request.Cookies["User"].Value;
            if (way == "login")
            {
                System.Threading.Thread.Sleep(5000);
                
               
                string temp_FetchProcessingResult = String.Format("0/odata/UsrApplicationGate({0})?$select=UsrProcessingResultId,UsrActiveApplicationId,UsrActiveAgreementId&$expand=UsrProcessingResult($select=Name),UsrActiveAgreement($select=UsrName),UsrActiveApplication($select=UsrName),UsrContact($select=Name)", createdRecordId["Id"]);
                string FetchProcessingResult = apiurl + temp_FetchProcessingResult;
                var client3 = new RestClient(FetchProcessingResult);
                client3.Timeout = -1;
                var request3 = new RestRequest(Method.GET);
                request3.AddHeader("Content-Type", "application/json");
                request3.AddHeader("BPMCSRF", bpmcsrf);
                request3.AddCookie(".ASPXAUTH", aspxauth);
                request3.AddCookie("BPMCSRF", bpmcsrf);
                request3.AddCookie("BPMLOADER", bpmloader);
                request3.AddCookie("UserName", username);
                //request3.AddHeader("Cookie", ".ASPXAUTH=" + aspxauth + "; BPMCSRF=" + bpmcsrf + "; BPMLOADER=" + bpmloader + "; UserName=" + username + "");
                IRestResponse response3 = client3.Execute(request3);

                var ParsedResponse = JObject.Parse(response3.Content);

                if (ParsedResponse["UsrProcessingResultId"].ToString() == "00000000-0000-0000-0000-000000000000")
                {
                    
                    Response.Redirect("~/Home/About");
                }
                else
                {
                    if (ParsedResponse["UsrActiveApplicationId"].ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        //Console.WriteLine(ParsedResponse["UsrProcessingResult"]["Name"]);
                        //Console.WriteLine(ParsedResponse["UsrActiveApplication"]["UsrName"]);
                        Response.Redirect("~/Home/Applications");
                    }
                    else
                    {
                        //Console.WriteLine(ParsedResponse["UsrProcessingResult"]["Name"]);
                        //Console.WriteLine(ParsedResponse["UsrActiveAgreement"]["UsrName"]);
                        Response.Redirect("~/Home/Agreements");
                    }
                }
            }
            else {
                Response.Redirect("~/Home/About");
            }
        }
    }
}
