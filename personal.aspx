<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="personal.aspx.cs" Inherits="NBFC_App___dev.personal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Details</title>
     <meta charset='utf-8'>
     <meta name='viewport' content='width=device-width, initial-scale=1'>     
     <link href='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' rel='stylesheet'>
     <link href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.0.3/css/font-awesome.css' rel='stylesheet'>
     <link href="style.css" rel="stylesheet" />
     <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin='anonymous'>
     <script type='text/javascript' src='https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js'></script>
     <script type='text/javascript' src='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.bundle.min.js'></script>  
     <link href="https://cdnjs.cloudflare.com/ajax/libs/mdb-ui-kit/3.3.0/mdb.min.css"rel="stylesheet"/>
     <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/mdb-ui-kit/3.3.0/mdb.min.js"></script>
</head>
<body runat='server' oncontextmenu='return false' class='snippet-body bodyclass'>
    <div class="container-fluid" id="grad1">
    <div class="row justify-content-center mt-0">
        <div class="col-11 col-sm-9 col-md-7 col-lg-6 text-center p-0 mt-3 mb-2">
            <div class="card px-0 pt-4 pb-0 mt-3 mb-3">
                <h2><strong>Enter your Details</strong></h2>
                <p>Fill all form field to go to next step</p>
                <div class="row">
                    <div class="col-md-12 mx-0">
                        <form id="msform" runat="server">                                                        
                            <fieldset>
                                <div class="form-card">                                    
                                    <div><h3 class="title fs-title" id="header">Loan Amount</h3></div>
                                    <div><asp:TextBox ID="TextBox3" class="loanamount" runat="server"></asp:TextBox></div>                                    
                                    <div id="cont">
                                        <div id="left"><label>2000</label></div>                                        
                                        <div id="right"><label>30000</label></div>
                                    </div>    
                                    <div class="range">
                                        <input id="myRange" type="range" class="form-range slider-range" min="2000" max="30000" runat="server" />
                                    </div>                                    
                                    <asp:TextBox ID="TextBox1" placeholder="Mobile No." runat="server" ReadOnly="true"></asp:TextBox>
                                    <asp:TextBox ID="TextBox2" placeholder="Email ID" runat="server" ReadOnly="true"></asp:TextBox> 
                                    <asp:TextBox ID="FullName" placeholder="Full Name" runat="server" ReadOnly="true"></asp:TextBox>
                                    <asp:DropDownList ID="Loan_type" class="dropdowncls" runat="server">
                                        <asp:ListItem Text="Loan Type" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Short Term Loan" Value="short"></asp:ListItem>
                                        <asp:ListItem Text="Long Term Loan" Value="long"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="shortterm" class="dropdowncls" runat="server">
                                        <asp:ListItem Text="Number of Days" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>                                        
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="longterm" class="dropdowncls" runat="server">
                                        <asp:ListItem Text="Number of Months" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>                                       
                                    </asp:DropDownList>
                                    <asp:TextBox ID="PAN" placeholder="PAN number" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="Monthly_income" placeholder="Monthly Income" runat="server"></asp:TextBox>
                                    <asp:DropDownList ID="Product" class="dropdowncls" runat="server">
                                        <asp:ListItem Enabled="true" Text="Product" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Payroll card programs" Value="7ed09da4-16cd-4682-9256-06ec7de429ad"></asp:ListItem>
                                        <asp:ListItem Text="Fixed Rate Mortgage, 30 Year Term" Value="3e38e0a5-00ac-4870-922f-08c8662ad943"></asp:ListItem>
                                        <asp:ListItem Text="Simple Auto Coverage" Value="2e1e9729-54b6-4f43-819c-10ffbbf977de"></asp:ListItem>
                                        <asp:ListItem Text="Classic Auto Loan" Value="a23812d9-875a-4729-b42b-1cdf167bb446"></asp:ListItem>
                                        <asp:ListItem Text="Business Visa Card" Value="0d19fb43-0ce9-4e50-be86-33196859f80a"></asp:ListItem>
                                        <asp:ListItem Text="Used Auto Loan" Value="bbc012e3-8971-4603-9a36-43d7503f6ac8"></asp:ListItem>
                                        <asp:ListItem Text="Adjustable Rate Mortgage, 30 Year Term" Value="a5ea0caa-56fc-461b-a3e4-518dc19a8231"></asp:ListItem>
                                        <asp:ListItem Text="Product bundle" Value="a2d8601f-f06e-4930-b842-537943407971"></asp:ListItem>
                                        <asp:ListItem Text="Personal insurance" Value="6454c024-6f36-4e8c-91d7-542a70d13d00"></asp:ListItem>
                                        <asp:ListItem Text="Fixed Rate Mortgage, 15 Year Term" Value="dd9e0f24-3e23-4734-b02f-5b0ee906c425"></asp:ListItem>
                                        <asp:ListItem Text="Personal Loan" Value="b69f5776-5740-4b14-9c31-78210bc45483"></asp:ListItem>
                                        <asp:ListItem Text="Point Awards Credit Card" Value="28eeff97-3650-49d9-a63c-89099c64e708"></asp:ListItem>
                                        <asp:ListItem Text="Business credit card (Master Card)" Value="beaf491c-9c6a-4a2e-80ca-8b2869e568a6"></asp:ListItem>
                                        <asp:ListItem Text="Savings accounts" Value="135b2dd3-8c8a-4c02-8efc-8b5be733a05b"></asp:ListItem>
                                        <asp:ListItem Text="Merchant Services" Value="b0dc21d9-f9bd-4499-8cec-b06335c83717"></asp:ListItem>
                                        <asp:ListItem Text="Travel MasterCard" Value="d90f65b3-5595-4961-a35c-bd1fed2fb7e8"></asp:ListItem>
                                        <asp:ListItem Text="Business credit card (Visa)" Value="a113029d-6296-47f5-a94e-bd5a176f0c0d"></asp:ListItem>
                                        <asp:ListItem Text="Travel Credit Cards" Value="a47e2f93-eaab-4adc-b468-dd9eb4335efa"></asp:ListItem>
                                        <asp:ListItem Text="Full Auto Coverage" Value="9d2614f8-047a-4d98-a389-e0def88906fa"></asp:ListItem>                             
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="Industry_type" class="dropdowncls" runat="server">
                                        <asp:ListItem Enabled="true" Text="Occupation" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Agriculture/Farming" Value = "218d70ef-2236-48ec-9ced-f2a1d62d7818"></asp:ListItem>
                                        <asp:ListItem Text="Automobile" Value = "600b1751-3ddb-4440-8b55-f4858d33f3be"></asp:ListItem>
                                        <asp:ListItem Text="Aviation" Value = "21f7e8c2-0be8-40db-8cff-d49ab6a4e9c0"></asp:ListItem>
                                        <asp:ListItem Text="Banking/Finance/Insurance" Value = "93d63754-868c-48cc-8590-ee5de27484eb"></asp:ListItem>
                                        <asp:ListItem Text="Building/Construction" Value = "6f482fbc-1460-468a-80a6-0746fba0fefd"></asp:ListItem>
                                        <asp:ListItem Text="Cab Driver" Value = "4eae2881-5be7-452b-bfd4-f4a2bea7d6a5"></asp:ListItem>
                                        <asp:ListItem Text="Call Centre" Value = "775cf32a-a1a3-4856-8aee-36f91ecb2e4e"></asp:ListItem>
                                        <asp:ListItem Text="Cinema / Entertainment" Value = "a8ce016f-5a8e-4f3f-85bf-03236fe5717e"></asp:ListItem>
                                        <asp:ListItem Text="Consulting" Value = "fa949e7b-c018-4865-b0bc-1572345a5524"></asp:ListItem>
                                        <asp:ListItem Text="Consumer Goods" Value = "535daf71-958f-4ac2-b07a-6326b704e821"></asp:ListItem>
                                        <asp:ListItem Text="Courier/Logistics" Value = "4cafa6d7-b6e6-4921-bdd8-febcb9be465d"></asp:ListItem>
                                        <asp:ListItem Text="Dairy Business" Value = "c38749e7-b958-49af-9db1-e6c54eaeca43"></asp:ListItem>
                                        <asp:ListItem Text="Ecommerce Delivery" Value = "5f907db1-a57c-413a-8f95-d273aa8effcd"></asp:ListItem>
                                        <asp:ListItem Text="Education Institutes" Value = "d6e20f7b-6bd9-4bab-a6cd-36d7275240ba"></asp:ListItem>
                                        <asp:ListItem Text="Education/Training" Value = "2f2c5708-65ec-4083-a5a3-5b2468e5ebb8"></asp:ListItem>
                                        <asp:ListItem Text="Engineering" Value = "b1e67628-cc66-47e2-b1d9-ef5b9982a70f"></asp:ListItem>
                                        <asp:ListItem Text="Fashion / Body Care" Value = "1ecc2f05-c0aa-4679-a13b-ff723e3c8331"></asp:ListItem>
                                        <asp:ListItem Text="Food Delivery" Value = "c134c8f0-6ee5-4844-9056-542907b47f5c"></asp:ListItem>
                                        <asp:ListItem Text="Government Sector" Value = "7dd377ab-dce3-46f3-873c-b8d00a39af8f"></asp:ListItem>
                                        <asp:ListItem Text="Grocery Shop" Value = "6893e419-6aca-4ca5-adbd-4d67f301eff9"></asp:ListItem>
                                        <asp:ListItem Text="Healthcare / Hospitals" Value = "3d091e30-ae5d-4324-899d-e026ad80b4dd"></asp:ListItem>
                                        <asp:ListItem Text="Hotels" Value = "b89d0888-65bb-49e3-a4cb-dea871696417"></asp:ListItem>
                                        <asp:ListItem Text="Human Resources" Value = "43c2bf7d-9407-484b-9559-1ac18a447168"></asp:ListItem>
                                        <asp:ListItem Text="Information Technology / Software" Value = "c6b5536a-482f-418b-8e88-ce6988eb3c2a"></asp:ListItem>
                                        <asp:ListItem Text="Legal Services Industry" Value = "18aaa7b1-cc81-4498-a325-47fc0428b08b"></asp:ListItem>
                                        <asp:ListItem Text="Manufacturing" Value = "eba0a18b-7cd8-4627-bf08-4e8d61213731"></asp:ListItem>
                                        <asp:ListItem Text="Media/Advertising" Value = "14262337-8dc0-4464-9cb4-602b604d7308"></asp:ListItem>
                                        <asp:ListItem Text="Medical Shop" Value = "4ec7353f-06b7-4e3d-948c-e99cec251227"></asp:ListItem>
                                        <asp:ListItem Text="Mining" Value = "e5b6ecb1-a3ae-43dd-b220-a08351806880"></asp:ListItem>
                                        <asp:ListItem Text="NGO" Value = "a0c0f9a8-c8a9-4588-865f-db0c2e9a2ae0"></asp:ListItem>
                                        <asp:ListItem Text="Oil & Gas" Value = "104c6731-32bc-41d0-b510-b296b28803f4"></asp:ListItem>
                                        <asp:ListItem Text="Other Industries" Value = "6b1dfeb4-68d4-4c57-9a00-547f2c317761"></asp:ListItem>
                                        <asp:ListItem Text="Other Shops" Value = "c78035ee-8564-4b77-964b-a7cf8be5430c"></asp:ListItem>
                                        <asp:ListItem Text="Pharmaceuticals" Value = "1daba57d-dd97-4304-8034-1957f0f93938"></asp:ListItem>
                                        <asp:ListItem Text="Real Estate / Construction" Value = "490fd8b9-dc50-4b62-993a-37a55536fb69"></asp:ListItem>
                                        <asp:ListItem Text="Restaurant" Value = "86383628-2ae1-4f0b-ab19-1cb03973f211"></asp:ListItem>
                                        <asp:ListItem Text="Retail/ FMCG" Value = "e9581e63-41f2-47e3-90f9-7eedf8945aa6"></asp:ListItem>
                                        <asp:ListItem Text="Sales Marketing" Value = "7702c994-c09b-4e16-bc34-47916ceb478c"></asp:ListItem>
                                        <asp:ListItem Text="Services" Value = "f4aea129-daa7-4e2f-a503-8a348ccfb160"></asp:ListItem>
                                        <asp:ListItem Text="Shipping/Exports" Value = "249f7ba8-53d7-4ef1-aa24-ef51ddeb9d51"></asp:ListItem>
                                        <asp:ListItem Text="Teaching" Value = "fd8ce187-52c5-415b-b6af-e17231b82ddb"></asp:ListItem>
                                        <asp:ListItem Text="Telecom/DTH" Value = "8838a138-8fd5-4719-81ae-7f7aaaad1e66"></asp:ListItem>
                                        <asp:ListItem Text="Textile" Value = "86427dea-85c5-4f3c-bf07-118493ebed74"></asp:ListItem>
                                        <asp:ListItem Text="Trading / Retail/ Distributor" Value = "7ef788e1-c934-427e-9272-eb6152a24b2a"></asp:ListItem>
                                        <asp:ListItem Text="Transport" Value = "06e63172-888d-471d-95df-292d728fd068"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="Reason" class="dropdowncls" runat="server">
                                        <asp:ListItem Enabled="true" Text="Reason" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Wedding" Value = "c49e7557-c49a-48a3-b409-7b1d6ff3d648"></asp:ListItem>
                                        <asp:ListItem Text="Travel / Vacation" Value = "4cfd0d1a-4866-490a-926b-ffede0339f1d"></asp:ListItem>
                                        <asp:ListItem Text="Stock Markets/ Mutual Funds/ Lottery" Value = "13a760ce-ae81-4895-8e37-2a852a30f1c0"></asp:ListItem>
                                        <asp:ListItem Text="Shopping" Value = "ec5e9bb5-9531-4787-aef1-c1b92f89d629"></asp:ListItem>
                                        <asp:ListItem Text="School / College Fees" Value = "84aff127-99d2-4a7f-9d1f-848e93ae234e"></asp:ListItem>
                                        <asp:ListItem Text="Salary Issue: Covid19" Value = "3072a726-eaa4-41c7-84af-87687457591b"></asp:ListItem>
                                        <asp:ListItem Text="Repaying other loan" Value = "a6e11516-7150-4dc8-af37-9e2bab44a01e"></asp:ListItem>
                                        <asp:ListItem Text="Purchasing mobile phone" Value = "b8c0ff2b-42d2-4dd6-ba40-cbb73819ce99"></asp:ListItem>
                                        <asp:ListItem Text="Personal" Value = "3c3aca39-a131-441a-b11e-2b5c476ebdb5"></asp:ListItem>
                                        <asp:ListItem Text="Paying Rent / Bill" Value = "4bdcb3f9-fe9c-4e61-9503-5deb07d5a551"></asp:ListItem>
                                        <asp:ListItem Text="Others" Value = "139832a4-06da-4415-aa2d-8f854afc02e9"></asp:ListItem>
                                        <asp:ListItem Text="Medical Emergency" Value = "ed0ad22a-09e8-4194-9c31-de816d3bdc39"></asp:ListItem>
                                        <asp:ListItem Text="Home Improvement / Renovation" Value = "0d4b35f9-1294-455d-acfe-914a6bc0b704"></asp:ListItem>
                                        <asp:ListItem Text="Grocery Shopping" Value = "0c7215f3-dd08-4aac-92ae-09baa0bfaeea"></asp:ListItem>
                                        <asp:ListItem Text="Grocery / Household Expense" Value = "1731a449-a88e-4d61-8ef4-3b5b400eab2b"></asp:ListItem>
                                        <asp:ListItem Text="Gift Purpose" Value = "dec16f77-7d0d-46b8-b1f9-8035b0d8bab1"></asp:ListItem>
                                        <asp:ListItem Text="For Business" Value = "2a06ada1-3373-4410-b0e2-d3ae9e6de731"></asp:ListItem>
                                        <asp:ListItem Text="Family Event - Birthday/Marriage" Value = "9ff66005-a63d-4f0d-b1ff-1f4f1bd94983"></asp:ListItem>
                                        <asp:ListItem Text="EMI/Loan Repayment" Value = "abf18093-fb25-49aa-b564-caa89089f797"></asp:ListItem>
                                        <asp:ListItem Text="Buying jewellery" Value = "cc9e931e-35ad-45c0-906d-f1167d377bfa"></asp:ListItem>
                                        <asp:ListItem Text="Appliance Purchase" Value = "6461a085-63aa-4168-a1e1-d095c455275f"></asp:ListItem>
                                        <asp:ListItem Text="2 Wheeler Purchase" Value = "8e7942d6-4909-48fe-8eb8-6e9ccfa7f48a"></asp:ListItem>
                                    </asp:DropDownList>                                    
                                </div>                                                                                                                        
                                <asp:Button ID="Button1" class="next action-button" runat="server" data-toggle="modal" data-target="#myModal" Text="Submit" OnClick="Button1_Click" />
                            </fieldset>                           
                        </form>
                        <div class="modal fade" id="myModal" role="dialog">
                            <div class="modal-dialog">               
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="modal-title">Loan Registration...</h4>
                                    </div>
                                    <div class="modal-body">
                                    <div>
                                        <div style="position: relative;">
                                            <h4 style="text-align: center;">Hey! Please wait while we are Processing your request</h4>
                                        </div>
                                        <br />
                                        <div class="container-fluid">
                                            <div class="d-flex justify-content-center">
                                                <div class="spinner-border" role="status">
                                                    <span class="sr-only">Loading...</span>
                                                </div>
                                            </div>
                                        </div>
                                   </div>
                                   </div>                                
                                </div>

                          </div>
                    </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</body>
    <script src="script.js"></script>
</html>