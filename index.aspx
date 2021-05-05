<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="NBFC_App___dev.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
        <title>Login / Signup</title>
        <link rel="shortcut icon" href="assets/images/fav.png" type="image/x-icon">
        <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i,800,800i&display=swap" rel="stylesheet">
        <link rel="shortcut icon" href="assets/images/fav.jpg">
        <link rel="stylesheet" href="assets/css/bootstrap.min.css">
        <link rel="stylesheet" href="assets/css/all.min.css">
        <link rel="stylesheet" href="assets/css/animate.css">
        <link rel="stylesheet" type="text/css" href="assets/css/style.css" />        
    </head>

    <body class="form-login-body">
        <div id="page-container">
        <div id="content_wrap" class="container-fluid">
            <form runat="server">
                <div class="top-menu">
                    <div class="container">
                        <div class="row">
                            <div class="col-sm-4 logo">
                                <img src="assets/images/virtuos_flat.png" alt="">
                            </div>                            
                            <div class="col-sm-8 sup">
                                <ul>
                                    <li><button runat="server" id="login_button" onserverclick="login_button_click" class="btn btn-sm btn-outline-primary"><i class="fas fa-user-lock"></i>Login</button></li>
                                    <li><button runat="server" id="signup_button" onserverclick="signup_button_click" class="btn btn-sm btn-outline-primary"><i class="fas fa-user-plus"></i>Signup</button></li>
                             </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="login-body container-fluid">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="login-text">
                                        <div id="main_page">
                                        <h4 id="login_label">Log In</h4>
                                        <h4 id="signup_label">Sign Up</h4>                                              
                                            <asp:TextBox ID="fullname" placeholder="Full Name" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:TextBox ID="mnumber"  placeholder="Mobile No." runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:TextBox ID="email" placeholder="Email ID" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:TextBox ID="next_clicked" runat="server" hidden="true"></asp:TextBox>
                                            <asp:TextBox ID="temp_data" runat="server" hidden="true"></asp:TextBox>                                     
                                        <asp:Button ID="Button1" runat="server" Text="Next" CssClass="btn btn-primary" OnClick="Button1_Click" />
                                            </div>
                                        <div id="otp_verify">
                                            <h4 id="otp_label">Please Enter the OTP</h4>
                                            <asp:TextBox ID="OTP" placeholder="OTP" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="Button2" runat="server" Text="Verify" CssClass="btn btn-primary" OnClick="Button2_Click" />
                                        </div>                                    
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="login-img">
                                        <img src="assets/images/login.png" alt="">
                                    </div>
                                </div>
                            </div>                            
                        </div>
                </div>
            </form>
        </div>
        <footer id="footer">
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
                        <div class="col-md-4 col-lg-3 footer-social wow fadeInUp animated" style="visibility: visible; animation-name: fadeInUp;">
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
        </footer>
            </div>
    </body>

    <script src="assets/js/jquery-3.2.1.min.js"></script>
    <script src="assets/js/popper.min.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>    
    <script src="assets/js/script.js"></script>   
</html>