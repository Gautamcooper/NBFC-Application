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
                    Dictionary<string, string> product_dictionary = new Dictionary<string, string>();
                    Dictionary<string, string> reason_dictionary = new Dictionary<string, string>();

                    string dbconn = ConfigurationManager.AppSettings["dbconn"];
                    string connectionString = dbconn;
                    SqlConnection sqlCnctn = new SqlConnection(connectionString);
                    sqlCnctn.Open();

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

                    url = apiurl + "0/odata/UsrProducts?$select=Id,Name&$orderby=CreatedOn desc";
                    ParsedResponse = GET_Object(url);
                    Product.Items.Add(new ListItem("Select Loan Type", "-1"));
                    foreach (var v in ParsedResponse["value"])
                    {
                        product_dictionary.Add(v["Name"].ToString(), v["Id"].ToString());
                        Product.Items.Add(new ListItem(v["Name"].ToString(), v["Id"].ToString()));
                    }
                    
                    url = apiurl + "0/odata/UsrReasonForLoan?$select=Id,Name&$orderby=CreatedOn desc";
                    ParsedResponse = GET_Object(url);
                    Reason.Items.Add(new ListItem("Select Reason", "-1"));
                    foreach (var v in ParsedResponse["value"])
                    {
                        reason_dictionary.Add(v["Name"].ToString(), v["Id"].ToString());
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
                            var typeOfLoan = data_tbl.Rows[0]["loantype"].ToString();
                            Loan_type.SelectedItem.Value = (typeOfLoan == "Long Term Loan") ? "long" : "short";
                            Loan_type.SelectedIndex = (typeOfLoan != "Long Term Loan") ? 1 : 2;
                            Loan_type.SelectedItem.Text = data_tbl.Rows[0]["loantype"].ToString();

                            if (typeOfLoan == "Long Term Loan")
                            {
                                longterm.SelectedItem.Value = data_tbl.Rows[0]["loanterm"].ToString();
                                longterm.SelectedIndex = int.Parse(data_tbl.Rows[0]["loanterm"].ToString()) - 1;
                                longterm.SelectedItem.Text = data_tbl.Rows[0]["loanterm"].ToString() + " Months";
                            }
                            else
                            {
                                shortterm.SelectedItem.Value = data_tbl.Rows[0]["loanterm"].ToString();
                                shortterm.SelectedIndex = int.Parse(data_tbl.Rows[0]["loanterm"].ToString()) - 4;
                                shortterm.SelectedItem.Text = data_tbl.Rows[0]["loanterm"].ToString() + " Days";
                            }
                            var product_name = data_tbl.Rows[0]["loanname"].ToString();
                            foreach (KeyValuePair<string, string> item in product_dictionary)
                            {
                                if (item.Key == product_name)
                                {
                                    Product.SelectedIndex = product_dictionary.Keys.ToList().IndexOf(item.Key) + 1;
                                    Product.SelectedItem.Value = item.Value;
                                    break;
                                }
                            }
                            Product.SelectedItem.Text = product_name;
                            TextBox3.Text = data_tbl.Rows[0]["loanamount"].ToString();
                            Monthly_income.Text = data_tbl.Rows[0]["monthlyIncome"].ToString();
                            var reason_name = data_tbl.Rows[0]["reasonforloan"].ToString();
                            foreach (KeyValuePair<string, string> item in reason_dictionary)
                            {
                                if (item.Key == reason_name)
                                {
                                    Reason.SelectedIndex = reason_dictionary.Keys.ToList().IndexOf(item.Key) + 1;
                                    Reason.SelectedItem.Value = item.Value;
                                    break;
                                }
                            }
                            Reason.SelectedItem.Text = data_tbl.Rows[0]["reasonforloan"].ToString();
                            EditStep.Text = "Edit Step";
                            AppGateId.Text = appGate;
                        }
                    }
                }
            }                       
        }

        public void Button1_Click(object sender, EventArgs e)
        {
            string shorttermloan = "0";
            string longtermloan = "0";
            string loan_type = "";
            if (Loan_type.SelectedValue == "short")
            {
                shorttermloan = shortterm.SelectedValue.ToString();
                loan_type = "Short Term Loan";
            }
            else if (Loan_type.SelectedValue == "long")
            {
                longtermloan = longterm.SelectedValue.ToString();
                loan_type = "Long Term Loan";
            }
            string loanterm = (shorttermloan != "0") ? shorttermloan : longtermloan;
            string mobile = TextBox1.Text.ToString();
            string email = TextBox2.Text.ToString();
            string fullname = FullName.Text.ToString();
            string pan_number = PAN.Text.ToString();
            string loanamount = TextBox3.Text.ToString();
            string monthly_income = Monthly_income.Text.ToString();
            string product_val = Product.SelectedValue.ToString();
            string productname = Product.SelectedItem.ToString();
            string editstep = EditStep.Text.ToString();
            string applicationgateid = AppGateId.Text.ToString();
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
            string connectionString = ConfigurationManager.AppSettings["dbconn"];

            if (String.IsNullOrEmpty(editstep) && String.IsNullOrEmpty(applicationgateid))
            {
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
                request2.AddParameter("application/json", "{\r\n    \"UsrAction\": \"1\", \r\n    \"UsrLoanAmountRequested\": \"" + loanamount + "\",\r\n    \"UsrPANNumber\":\"" + pan_number + "\",\r\n    \"UsrIsLostCustomer\": true,\r\n    \"UsrShortTermLoanRequested\":\"" + shorttermloan + "\",\r\n    \"UsrLongTermLoanRequested\":\"" + longtermloan + "\",\r\n     \"UsrFullName\":\"" + fullname + "\",\r\n     \"UsrProductId\":\"" + product_val + "\",\r\n     \"UsrMonthlyIncome\":\"" + monthly_income + "\",\r\n     \"UsrEmail\":\"" + email + "\",\r\n    \"UsrMobileNumber\": \"" + mobile + "\",\r\n    \"UsrReasonForLoanId\":\"" + reason + "\"  \r\n}", ParameterType.RequestBody);
                IRestResponse response2 = client2.Execute(request2);
                var createdRecordId = JObject.Parse(response2.Content);
                applicationgateid = createdRecordId["Id"].ToString();
                string date = createdRecordId["CreatedOn"].ToString();
                SqlConnection sqlCnctn = new SqlConnection(connectionString);
                sqlCnctn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand cmd;
                string sql = "Update UserInfo set step1 = 'true', pannumber = '" + pan_number + "',applicationgateId = '" + createdRecordId["Id"] + "' where session = '" + Session["Name"].ToString() + "'";
                cmd = new SqlCommand(sql, sqlCnctn);
                adapter.UpdateCommand = new SqlCommand(sql, sqlCnctn);
                adapter.UpdateCommand.ExecuteNonQuery();
                cmd.Dispose();
                SqlConnection sqlCnctn2 = new SqlConnection(connectionString);
                sqlCnctn2.Open();
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                SqlCommand cmd2;
                string sqlcmd2 = "Insert Into UserSTEP1Info (step1Id, processed, loantype, loanterm, date, loanname, email, mobile, loanamount, monthlyIncome, reasonforloan) values('" + createdRecordId["Id"].ToString() + "','false','" + loan_type + "','" + loanterm + "','" + date + "','" + productname + "','" + email + "','" + mobile + "','" + loanamount + "','" + monthly_income + "','" + Reason.SelectedItem.ToString() + "')";
                cmd2 = new SqlCommand(sqlcmd2, sqlCnctn2);
                adapter2.UpdateCommand = new SqlCommand(sqlcmd2, sqlCnctn2);
                adapter2.UpdateCommand.ExecuteNonQuery();
                cmd2.Dispose();
            }
            else
            {
                string url = apiurl + "0/odata/UsrApplicationGate(" + applicationgateid + ")";
                var client3 = new RestClient(url);
                client3.Timeout = -1;
                var request3 = new RestRequest(Method.PATCH);
                request3.AddHeader("BPMCSRF", bpmcsrf);
                request3.AddHeader("Content-Type", "application/json");
                request3.AddCookie(".ASPXAUTH", aspxauth);
                request3.AddCookie("BPMCSRF", bpmcsrf);
                request3.AddCookie("BPMLOADER", bpmloader);
                request3.AddCookie("UserName", username);
                request3.AddParameter("application/json", "{\r\n    \"UsrLoanAmountRequested\": \"" + loanamount + "\",\r\n    \"UsrShortTermLoanRequested\":\"" + shorttermloan + "\",\r\n    \"UsrLongTermLoanRequested\":\"" + longtermloan + "\",\r\n    \"UsrProductId\":\"" + product_val + "\",\r\n    \"UsrMonthlyIncome\":\"" + monthly_income + "\",\r\n    \"UsrReasonForLoanId\":\"" + reason + "\"  \r\n}", ParameterType.RequestBody);
                var qryResponse = client3.Execute(request3);
                SqlConnection sqlCnctn3 = new SqlConnection(connectionString);
                sqlCnctn3.Open();
                SqlDataAdapter adapter3 = new SqlDataAdapter();
                SqlCommand cmd3;
                string sqlcmd3 = "Update UserSTEP1Info set step1Id = '" + applicationgateid + "', processed = 'false', loantype = '" + loan_type + "', loanterm = '" + loanterm + "', date = '" + DateTime.Now + "', loanname = '" + productname + "', loanamount = '" + loanamount + "', monthlyIncome = '" + monthly_income + "', reasonforloan = '" + Reason.SelectedItem.ToString() + "' where step1Id = '" + applicationgateid + "'";
                cmd3 = new SqlCommand(sqlcmd3, sqlCnctn3);
                adapter3.UpdateCommand = new SqlCommand(sqlcmd3, sqlCnctn3);
                adapter3.UpdateCommand.ExecuteNonQuery();
                cmd3.Dispose();
            }

            string way = Request.Cookies["User"].Value;
            if (way == "login")
            {
                System.Threading.Thread.Sleep(5000);


                string temp_FetchProcessingResult = String.Format("0/odata/UsrApplicationGate({0})?$select=UsrProcessingResultId,UsrActiveApplicationId,UsrActiveAgreementId&$expand=UsrProcessingResult($select=Name),UsrActiveAgreement($select=UsrName),UsrActiveApplication($select=UsrName),UsrContact($select=Name)", applicationgateid);
                string FetchProcessingResult = apiurl + temp_FetchProcessingResult;
                var client4 = new RestClient(FetchProcessingResult);
                client4.Timeout = -1;
                var request4 = new RestRequest(Method.GET);
                request4.AddHeader("Content-Type", "application/json");
                request4.AddHeader("BPMCSRF", bpmcsrf);
                request4.AddCookie(".ASPXAUTH", aspxauth);
                request4.AddCookie("BPMCSRF", bpmcsrf);
                request4.AddCookie("BPMLOADER", bpmloader);
                request4.AddCookie("UserName", username);
                IRestResponse query_response = client4.Execute(request4);

                var ParsedResponse = JObject.Parse(query_response.Content);


                if (ParsedResponse["UsrProcessingResultId"].ToString() == "00000000-0000-0000-0000-000000000000")
                {

                    Response.Redirect("~/Home/About");
                }
                else
                {
                    if (ParsedResponse["UsrActiveApplicationId"].ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        Response.Redirect("~/Home/Applications");
                    }
                    else
                    {
                        Response.Redirect("~/Home/Agreements");
                    }
                }
            }
            else
            {
                Response.Redirect("~/Home/About");
            }
        }
    }
}