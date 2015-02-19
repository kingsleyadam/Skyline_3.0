using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Web.SessionState;

namespace Skyline_3._0
{
    public partial class Web : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string site = Path.GetFileName(Request.Url.AbsolutePath).ToLower();
            string dir = VirtualPathUtility.GetDirectory(Request.Path).ToString().Replace("/", "");

            if (!dir.Contains("admin")) 
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
    }
}