using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Skyline_3._0
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lgnForm_LoginError(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Login lgnForm = (System.Web.UI.WebControls.Login)sender;
            MembershipUser user = Membership.GetUser(lgnForm.UserName);
            if (user == null)
                lgnForm.FailureText = "The username entered does not exist.";
            else if (user.IsLockedOut)
                lgnForm.FailureText = "Account is locked. Please user forgot password.";
            else
                lgnForm.FailureText = "Incorrect username of password. Please try again.";

            Panel pnlFailureText = (Panel)lgnForm.Controls[0].FindControl("pnlFailureText");
            if (pnlFailureText != null)
            {
                pnlFailureText.Visible = true;
            }
        }

        protected void lgnForm_PreRender(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Login lgnForm = (System.Web.UI.WebControls.Login)sender;
            Button btnLogin = (Button)lgnForm.Controls[0].FindControl("LoginButton");
            TextBox txtUsername = (TextBox)lgnForm.Controls[0].FindControl("Username");
            TextBox txtPassword = (TextBox)lgnForm.Controls[0].FindControl("Password");

            if (btnLogin != null && txtUsername != null && txtPassword != null)
            {
                pnlLogin.DefaultButton = btnLogin.UniqueID;

                if (lgnForm.UserName.ToString().Length > 0)
                {
                    txtPassword.Attributes.Add("autofocus", "");
                    txtUsername.Attributes.Remove("autofocus");
                }
                else
                {
                    txtUsername.Attributes.Add("autofocus", "");
                    txtPassword.Attributes.Remove("autofocus");
                }

            }
        }
    }
}