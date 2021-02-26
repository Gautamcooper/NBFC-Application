<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="NBFC_App___dev.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>First Page</title>
     <meta charset='utf-8'>
     <meta name='viewport' content='width=device-width, initial-scale=1'>     
     <link href='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' rel='stylesheet'>
     <link href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.0.3/css/font-awesome.css' rel='stylesheet'>
     <link href="style.css" rel="stylesheet" />
     <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin='anonymous'>
     <script type='text/javascript' src='https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js'></script>
     <script type='text/javascript' src='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.bundle.min.js'></script>
</head>
<body class="bodyclass" runat='server'>
    <div class="container-fluid">        
         <div class="row justify-content-center marg">
            <div class="col-11 col-sm-9 col-md-7 col-lg-6 text-center p-0 mt-3 mb-2">
                <div class="card px-0 pt-4 pb-0 mt-3 mb-3">
                    <h2><strong>Select an option</strong></h2>
                    <p id="first">First time users select Signup.</p>
                    <p id="second" style="display: none;">Select the choice</p>
                    <form id="msform" runat="server">                                                        
                        <fieldset>                           
                               <div class="row" style="justify-content: center;"><asp:Button ID="Signup" name="next" class="action-button but-cls" runat="server" Text="Sign up" OnClick="Signup_Click"/></div>
                               <div class="row" style="justify-content: center;"><asp:Button ID="Signin" name="next" class="action-button but-cls" Text="Log in" OnClick="Signin_Click" runat="server" /></div>                                        
                        </fieldset>                       
                    </form>
                </div>
             </div>
         </div>
     </div>
</body>
     <script src="script.js"></script>
</html>
