using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Web.SessionState;
using System.Web.Security;
using ProductInfo;

namespace Skyline_3._0
{
    public partial class Web : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string site = Path.GetFileName(Request.Url.AbsolutePath).ToLower();
            string dir = VirtualPathUtility.GetDirectory(Request.Path).ToString().Replace("/", "");

            if (dir.Contains("admin"))
            {
                if (Request.IsAuthenticated)
                {
                    HyperLink hl = (HyperLink)lgnView.FindControl("lnkAdminDropDown");
                    if (hl != null)
                        hl.ForeColor = System.Drawing.Color.White;
                }
            }
            else
            {
                switch (site)
                {
                    case "default.aspx":
                        lnkHome.CssClass = "navbar-brand navbar-brand-active";
                        pnlBanner.Visible = true;
                        break;
                    case "products.aspx":
                        liProducts.Attributes["Class"] = "active";
                        break;
                    case "order.aspx":
                        liOrder.Attributes["Class"] = "active";
                        break;
                    case "about_us.aspx":
                        liAboutUs.Attributes["Class"] = "active";
                        break;
                    case "contact_us.aspx":
                        liContactUs.Attributes["Class"] = "active";
                        break;
                    case "account_info.aspx":
                        liAccountInfo.Attributes["Class"] = "active";
                        break;
                    case "change_password.aspx":
                        liAccountInfo.Attributes["Class"] = "active";
                        break;
                }
            }

            if (HttpContext.Current.User.IsInRole("User"))
                lnkAccountSettings.Visible = true;
            else
                lnkAccountSettings.Visible = false;

            if (HttpContext.Current.User.IsInRole("Order"))
                lnkOrderForm.Visible = true;
            else
                lnkOrderForm.Visible = false;

            if (Context.Request.IsAuthenticated)
            {
                lnkProducts.PostBackUrl = "~/stores/products.aspx";
            }
            else
            {
                lnkProducts.Attributes.Add("href", "#login");
                lnkProducts.Attributes.Add("data-toggle", "modal");
                lnkProducts.Attributes.Add("data-id", "product");
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            string appPath = HttpContext.Current.Request.ApplicationPath;

            if (!(appPath == "/"))
                appPath = appPath + "/";

            
            litBootStrapCSSRef.Text = "<link id='cssbootstrap' href='" + appPath + "css/bootstrap.min.css' rel='stylesheet' />";
            litSiteCSSRef.Text = "<link id='csssite' href='" + appPath + "css/style.css' rel='stylesheet' />";

            litJQueryRef.Text = "<script src='" + appPath + "scripts/jquery-2.1.3.min.js'></script>";
            litBootstrapJSRef.Text = "<script src='" + appPath + "scripts/bootstrap.min.js'></script>";
            litJavascriptRef.Text = "<script src='" + appPath + "scripts/javascript.js'></script>";
        }

        protected void Page_PreRender(Object sender, EventArgs e)
        {
            if (Cart.Instance.Items.Count > 0)
                lnkOrderForm.Text = "Your Order <span class='badge'>" + Cart.Instance.Items.Count.ToString() + "</span>";
            else
                lnkOrderForm.Text = "Your Order";
        }

        protected void lgnForm_LoginError(object sender, EventArgs e)
        {
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

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void lgnForm_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Button btnLogin = (Button)lgnForm.Controls[0].FindControl("LoginButton");
                TextBox txtUsername = (TextBox)lgnForm.Controls[0].FindControl("Username");
                TextBox txtPassword = (TextBox)lgnForm.Controls[0].FindControl("Password");

                if (btnLogin != null && txtUsername != null && txtPassword != null)
                {
                    login.DefaultButton = btnLogin.UniqueID;

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

        protected void lgnForm_LoggedIn(object sender, EventArgs e)
        {
            string loginSource = Request.Form["loginSource"].ToString();

            if (loginSource == "product")
            {
                Response.Redirect("~/stores/products.aspx");
            }


        }

        private Control FindChildControl(Control parent, string idToFind)
        {
            foreach (Control child in parent.Controls)
            {
                if (child.ID == idToFind)
                {
                    return child;
                }
                else
                {
                    Control control = FindChildControl(child, idToFind);
                    if (control != null)
                    {
                        return control;
                    }
                }
            }
            return null;
        }
    }
}