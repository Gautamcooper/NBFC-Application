using System;
using System.Collections.Generic;
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

        }

        protected void Signup_Click(object sender, EventArgs e)
        {
            HttpCookie UserCookie = new HttpCookie("User");
            UserCookie.Value = "signup";
            UserCookie.Expires = DateTime.Now.AddMinutes(5);
            Response.Cookies.Add(UserCookie);
            Response.Redirect("~/default.aspx");
        }
        protected void Signin_Click(object sender, EventArgs e)
        {
            HttpCookie UserCookie = new HttpCookie("User");
            UserCookie.Value = "login";
            UserCookie.Expires = DateTime.Now.AddMinutes(5);
            Response.Cookies.Add(UserCookie);
            Response.Redirect("~/default.aspx");
        }        
    }
}