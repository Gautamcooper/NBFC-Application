<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="NBFC_App___dev.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
        <title>Login / Signup</title>
        <link rel="shortcut icon" href="assets/images/fav.png" type="image/x-icon">
        <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i,800,800i&display=swap" rel="stylesheet">
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" />
        <link rel="shortcut icon" href="assets/images/fav.jpg">
        <link rel="stylesheet" href="assets/css/bootstrap.min.css">
        <link rel="stylesheet" href="assets/css/all.min.css">
        <link rel="stylesheet" href="assets/css/animate.css">
        <link rel="stylesheet" type="text/css" href="assets/css/style.css" />        
    </head>

    <body class="form-login-body">
        <div id="page-container">
        <div id="content-wrap" class="container-fluid">
            <form runat="server">
                <div class="top-menu">
                    <div class="container header-section">
                        <div class="row">
                            <div class="col-lg-6 col-sm-6 logo">
                                <img class="rounded mx-auto d-block" src="assets/images/virtuos_flat.png" alt="logo-header" />
                            </div>           
                            <div class="col-lg-6 col-sm-6 sup text-center">
                                    <button runat="server" id="login_button" onserverclick="login_button_click" class="btn btn-outline-primary btn-sm"><i class="fas fa-user-lock"></i>Login</button>
                                    <button runat="server" id="signup_button" onserverclick="signup_button_click" class="btn btn-outline-primary btn-sm"><i class="fas fa-user-plus"></i>Signup</button>
                             </div>
                        </div>
                    </div>
                </div>
                <div class="login-body">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-12">
                                    <br /><br />
                                    <div class="login-text">
                                        <div id="main_page">
                                            <img id="vlogo" src="assets/images/VirtuosLogo.png" alt="Virtuos Logo" />
                                            <asp:TextBox ID="fullname" placeholder="Full Name" runat="server" CssClass="form-control"></asp:TextBox>
                                            <div class="invalid-feedback">Please provide a valid city.</div>
                                            <asp:TextBox ID="mnumber" type="number"  placeholder="Mobile No." runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:TextBox ID="email" placeholder="Email ID" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:TextBox ID="next_clicked" runat="server" hidden="true"></asp:TextBox>
                                            <asp:TextBox ID="temp_data" runat="server" hidden="true"></asp:TextBox>                                     
                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary mx-auto" OnClick="Button1_Click" />
                                         </div>
                                        <div id="otp_verify">
                                            <img id="vlogo1" src="assets/images/VirtuosLogo.png" alt="Virtuos Logo" />
                                            <h4 id="otp_label">Please Enter the OTP</h4>
                                            <asp:TextBox ID="OTP" placeholder="OTP" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="Button2" runat="server" Text="Verify" CssClass="btn btn-primary mx-auto" OnClick="Button2_Click" />
                                        </div>                                    
                                    </div>
                                </div>
                            </div>                            
                        </div>
                    </div>
                </form>
            </div>
            <footer class="footer">
            <div class="container bottom_border">
                <div class="row">
                    <div class=" col-sm-4 col-md col-12 col">
                        <h5 class="headin5_amrc col_white_amrc pt2">About us</h5>
                         <p>
                            <img class="logo-footer" src="assets/images/virtuos_logo.png" alt="logo-footer" data-at2x="assets/img/logo.png" style="width: 120px;height: 109px;" />
                        </p>
                        <p>
                            We are a young company always looking for new and creative ideas to help you with our products in your everyday work.
                        </p>
                    </div>


                    <div class=" col-sm-8 col-md col-12 col">
                        <h5 class="headin5_amrc col_white_amrc pt2">Find us</h5>
                        <p class="mb10">We are born in native cloud redefining how Customer Experience (CX), Employee Experience (EX), and Everything Experience (XX) transformed across brand, digital, and commerce. We are a company with 12+ years of experience in Digital Strategy, Design, Transformation, and IT Consulting.</p>
                        <p><i class="fa fa-location-arrow"></i> Emaar Digital Greens, Tower A, Sector-62, Gurugram </p>
                        <p><i class="fa fa-phone"></i>  +91 124-498-5500  </p>
                        <p><i class="fa fa fa-envelope"></i> info@virtuos.com  </p>
                    </div>
                </div>
            </div>
    <br /><br />
    <div class="container">
        <p class="text-center">VIRTUOS. CUSTOMER @ HEART | 2020 Virtuos Digital Ltd. All rights reserved.</p>
    
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

    <script src="assets/js/jquery-3.2.1.min.js"></script>
    <script src="assets/js/popper.min.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>    
    <script src="assets/js/script.js"></script>   
</html>