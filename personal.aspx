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
<body runat='server' oncontextmenu='return false' class='snippet-body bodycls'>
    <div id="page-container">
    <div class="container-fluid" id="content-wrap">
        <div class="progressBarContainer">
            <div id="step1">
                <span>Loan Details</span>
            </div>
            <div>&#8250;</div>
            <div id="step2">
                <span>Personal Details</span>
            </div>
        </div>
        <div class="row">
            <div class="col-md-7">                                    
                <div class="row justify-content-center mt-0">
                    <div class="col-11 col-sm-9 col-md-7 col-lg-6 text-center p-0 mt-3 mb-2">
                        <div class="card px-0 pt-4 pb-0 mt-3 mb-3">
                                
                            <%--<p>Fill all form field to go to next step</p>--%>
                            <div class="row">
                                <div class="col-md-12 mx-0">
                                    <form id="msform" runat="server">                                                        
                                        <fieldset>
                                            <div class="form-card">                                    
                                                <div><h3 class="title fs-title" id="header">Loan Amount</h3></div>
                                                <div><asp:TextBox ID="TextBox3" class="loanamount" runat="server"></asp:TextBox></div>                                    
                                                <div id="cont">
                                                    <div id="left"><label>2000</label></div>                                        
                                                    <div id="right"><label>100000</label></div>
                                                </div>    
                                                <div class="range">
                                                    <input id="myRange" type="range" class="form-range slider-range" min="2000" max="100000" runat="server" />
                                                </div>
                                                <div class="row" style="display:none;">
                                                    <label class="label_inp" for="TextBox1" id="label_TextBox1">Mobile</label>
                                                    <asp:TextBox ID="TextBox1" placeholder="Mobile No." runat="server" ReadOnly="true" CssClass="inp"></asp:TextBox>
                                                </div>
                                                <div class="row" style="display:none;">
                                                    <label class="label_inp" for="TextBox2" id="label_TextBox2">Email</label>
                                                    <asp:TextBox ID="TextBox2" placeholder="Email ID" runat="server" ReadOnly="true" CssClass="inp"></asp:TextBox>
                                                </div>
                                                <div class="row" style="display:none;">
                                                    <label class="label_inp" for="FullName" id="label_FullName">Full Name</label>
                                                    <asp:TextBox ID="FullName" placeholder="Full Name" runat="server" ReadOnly="true" CssClass="inp"></asp:TextBox>
                                                </div> 
                                                <div class="row">
                                                    <label class="label_inp" for="Loan_type" id="label_Loan_type">Loan Duration</label>
                                                    <asp:DropDownList ID="Loan_type" class="dropdowncls inp-dropdown" runat="server">
                                                        <asp:ListItem Text="Loan Duration" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Short Term Loan" Value="short"></asp:ListItem>
                                                        <asp:ListItem Text="Long Term Loan" Value="long"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="row" id="shorttermrow">
                                                    <label class="label_inp" for="shortterm" id="label_shortterm">Number of Days</label>
                                                    <asp:DropDownList ID="shortterm" class="dropdowncls inp-dropdown" runat="server">
                                                        <%--<asp:ListItem Text="Number of Days" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="5 Days" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="6 Days" Value="6"></asp:ListItem>
                                                        <asp:ListItem Text="7 Days" Value="7"></asp:ListItem>
                                                        <asp:ListItem Text="8 Days" Value="8"></asp:ListItem>
                                                        <asp:ListItem Text="9 Days" Value="9"></asp:ListItem>
                                                        <asp:ListItem Text="10 Days" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="11 Days" Value="11"></asp:ListItem>
                                                        <asp:ListItem Text="12 Days" Value="12"></asp:ListItem>
                                                        <asp:ListItem Text="13 Days" Value="13"></asp:ListItem>
                                                        <asp:ListItem Text="14 Days" Value="14"></asp:ListItem>
                                                        <asp:ListItem Text="15 Days" Value="15"></asp:ListItem>
                                                        <asp:ListItem Text="16 Days" Value="16"></asp:ListItem>
                                                        <asp:ListItem Text="17 Days" Value="17"></asp:ListItem>
                                                        <asp:ListItem Text="18 Days" Value="18"></asp:ListItem>
                                                        <asp:ListItem Text="19 Days" Value="19"></asp:ListItem>
                                                        <asp:ListItem Text="20 Days" Value="20"></asp:ListItem>
                                                        <asp:ListItem Text="21 Days" Value="21"></asp:ListItem>
                                                        <asp:ListItem Text="22 Days" Value="22"></asp:ListItem>
                                                        <asp:ListItem Text="23 Days" Value="23"></asp:ListItem>
                                                        <asp:ListItem Text="24 Days" Value="24"></asp:ListItem>
                                                        <asp:ListItem Text="25 Days" Value="25"></asp:ListItem>     --%>                                   
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="row" id="longtermrow">
                                                    <label class="label_inp" for="longterm" id="label_longterm">Number of Months</label>
                                                    <asp:DropDownList ID="longterm" class="dropdowncls inp-dropdown" runat="server">
                                                        <%--<asp:ListItem Text="Number of Months" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="2 Months" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="3 Months" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="4 Months" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="5 Months" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="6 Months" Value="6"></asp:ListItem>
                                                        <asp:ListItem Text="7 Months" Value="7"></asp:ListItem>
                                                        <asp:ListItem Text="8 Months" Value="8"></asp:ListItem>
                                                        <asp:ListItem Text="9 Months" Value="9"></asp:ListItem>
                                                        <asp:ListItem Text="10 Months" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="11 Months" Value="11"></asp:ListItem>
                                                        <asp:ListItem Text="12 Months" Value="12"></asp:ListItem>
                                                        <asp:ListItem Text="13 Months" Value="13"></asp:ListItem>
                                                        <asp:ListItem Text="14 Months" Value="14"></asp:ListItem>
                                                        <asp:ListItem Text="15 Months" Value="15"></asp:ListItem>
                                                        <asp:ListItem Text="16 Months" Value="16"></asp:ListItem>
                                                        <asp:ListItem Text="17 Months" Value="17"></asp:ListItem>
                                                        <asp:ListItem Text="18 Months" Value="18"></asp:ListItem>
                                                        <asp:ListItem Text="19 Months" Value="19"></asp:ListItem>
                                                        <asp:ListItem Text="20 Months" Value="20"></asp:ListItem>
                                                        <asp:ListItem Text="21 Months" Value="21"></asp:ListItem>
                                                        <asp:ListItem Text="22 Months" Value="22"></asp:ListItem>
                                                        <asp:ListItem Text="23 Months" Value="23"></asp:ListItem>
                                                        <asp:ListItem Text="24 Months" Value="24"></asp:ListItem>                --%>                       
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="row">
                                                    <label class="label_inp" for="PAN" id="label_PAN">PAN Number</label>
                                                    <asp:TextBox ID="PAN" placeholder="PAN number" runat="server" CssClass="inp"></asp:TextBox>
                                                </div>
                                                <div class="row">
                                                    <label class="label_inp" for="Monthly_income" id="label_Monthly_income">Monthly Income</label>
                                                    <asp:TextBox ID="Monthly_income" placeholder="Monthly Income" runat="server" CssClass="inp"></asp:TextBox>
                                                </div>
                                                <div class="row">
                                                    <label class="label_inp" for="Product" id="label_Product">Loan Type</label>
                                                    <asp:DropDownList ID="Product" class="dropdowncls inp-dropdown" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <%--<div class="row">
                                                    <label class="label_inp" for="Industry_type" id="label_Industry_type">Occupation</label>
                                                    <asp:DropDownList ID="Industry_type" class="dropdowncls inp-dropdown" runat="server">
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
                                                </div>--%>
                                                <div class="row">
                                                    <label class="label_inp" for="Reason" id="label_Reason">Reason</label>
                                                    <asp:DropDownList ID="Reason" class="dropdowncls inp-dropdown" runat="server">
                                                        <%--<asp:ListItem Enabled="true" Text="Reason" Value="-1"></asp:ListItem>
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
                                                        <asp:ListItem Text="2 Wheeler Purchase" Value = "8e7942d6-4909-48fe-8eb8-6e9ccfa7f48a"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="row">
                                                    <asp:TextBox hidden ID="EditStep" runat="server" CssClass="inp"></asp:TextBox>
                                                    <asp:TextBox hidden ID="AppGateId" runat="server" CssClass="inp"></asp:TextBox>
                                                </div>
                                                                        
                                            </div>                                                                                                                        
                                            <asp:Button ID="Button1" class="next action-button" runat="server" data-toggle="modal" data-target="#exampleModalLong" Text="Submit" OnClick="Button1_Click" />
                                        </fieldset>                           
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-5">
                 <div class="login-img">
                      <img src="assets/images/login.png" alt="" width="85%" height="70%">
                 </div>
            </div>
        </div>  
        <div class="modal" id="exampleModalLong" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
            <div class="modal-dialog  modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <p class="modal-title" id="exampleModalLongTitle"  style="font-size: 23px; color: #4a89dc; margin: 0 auto;">Notification</p>
                    </div>
                    <div class="modal-body text-center" style="font-size: 18px;">
                        Please wait while we are processing your request
                    </div>
                    <div class="modal-footer d-flex justify-content-center" style="height: 80px;">
                        <div class="loadingbar">
                            <div class="loadprogress">
                                <span class="loading">Loading</span>
                            </div>
                        </div>
                    </div>
                  </div>
                </div>
            </div>
    </div>
        <%--<footer id="footer">
            <div class="footer-top">
                <div class="container">
                    <div class="row">
                        <div class="col-md-4 col-lg-4 footer-about wow fadeInUp animated" style="visibility: visible; animation-name: fadeInUp;">
                            <p>
                                <img class="logo-footer" src="/assets/images/virtuos_logo.png" alt="logo-footer" data-at2x="assets/img/logo.png" style="width: 120px;height: 109px;">
                            </p>
                            <p>
                                We are a young company always looking for new and creative ideas to help you with our products in your everyday work.
                            </p>
                            <br />
                            <p><a style="color: #337ab7;" href="#">Our Team</a></p>
                        </div>
                        <div class="col-md-4 col-lg-4 offset-lg-1 footer-contact wow fadeInDown animated" style="visibility: visible; animation-name: fadeInDown;">
                            <h3 style="color: #aaa;">Contact</h3>
                            <p><i class="fas fa-map-marker-alt"></i> Emaar Digital Greens, Tower A, Sector-6, Gurugram</p>
                            <p><i class="fas fa-phone"></i> Phone: (+91) 9988998899</p>
                            <p><i class="fas fa-envelope"></i> Email: <a style="color: #337ab7;" href="mailto:hello@virtuos.com">hello@virtuos.com</a></p>
                            <p><i class="fab fa-skype"></i> Skype: virtuos_help</p>
                        </div>
                        <div class="col-md-4 col-lg-3 footer-social wow fadeInUp animated" style="visibility: visible; animation-name: fadeInUp;position: relative;left: 8em;">
                            <h3 style="color: #aaa;">Follow us</h3>
                            <p>
                                <a style="color: #337ab7;" href="#"><i class="fab fa-facebook fa-2x"></i></a>
                                <a style="color: #337ab7;" href="#"><i class="fab fa-twitter fa-2x"></i></a>
                                <a style="color: #337ab7;" href="#"><i class="fab fa-google-plus-g fa-2x"></i></a>
                                <a style="color: #337ab7;" href="#"><i class="fab fa-pinterest fa-2x"></i></a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </footer>--%>
        <footer class="footer">
            <div class="container bottom_border">
                <div class="row">
                    <div class=" col-sm-4 col-md col-12 col">
                        <h4 class="headin5_amrc col_white_amrc pt2">About us</h4>
                        <p>
                            <img class="logo-footer" src="virtuos_logo.png" alt="logo-footer" data-at2x="assets/img/logo.png" style="width: 120px;height: 109px;" />
                        </p>
                        <p>
                            We are a young company always looking for new and creative ideas to help you with our products in your everyday work.
                        </p>
                        <p><a href="#">Our Team</a></p>
                    </div>


                    <div class=" col-sm-8 col-md col-12 col">
                        <h4 class="headin5_amrc col_white_amrc pt2">Find us</h4>
                        <p class="mb10">We are born in native cloud redefining how Customer Experience (CX), Employee Experience (EX), and Everything Experience (XX) transformed across brand, digital, and commerce. We are a company with 12+ years of experience in Digital Strategy, Design, Transformation, and IT Consulting.</p>
                        <p><i class="fa fa-location-arrow"></i> Emaar Digital Greens, Tower A, Sector-62, Gurugram </p>
                        <p><i class="fa fa-phone"></i>  +91 124-498-5500  </p>
                        <p><i class="fa fa fa-envelope"></i> info@virtuos.com  </p>
                    </div>
                </div>
            </div>
            <br /><br />
            <div class="container">
                <p class="text-center">VIRTUOS. CUSTOMER HEART | 2020 <a href="https://www.virtuos.com/">Virtuos Digital Ltd.</a> All rights reserved.</p>

                <ul class="social_footer_ul">
                    <li><a href="#" class="btn btn-primary" style="background-color: #3b5998;"><i class="fab fa-facebook-f"></i></a></li>
                    <li><a href="#" class="btn btn-primary" style="background-color: #55acee;"><i class="fab fa-twitter"></i></a></li>
                    <li><a href="#" class="btn btn-primary" style="background-color: #0082ca;"><i class="fab fa-linkedin"></i></a></li>
                    <li><a href="#" class="btn btn-primary" style="background-color: #ac2bac;"><i class="fab fa-instagram"></i></a></li>
                    <li><a href="#" class="btn btn-primary" style="background-color: #ed302f;"><i class="fab fa-youtube"></i></a></li>
                </ul>
            </div>

        </footer>
</div>
</body>
    <script src="script.js"></script>
</html>