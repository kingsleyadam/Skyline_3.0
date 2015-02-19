using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Skyline_3._0
{
    public partial class Login : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            string appPath = HttpContext.Current.Request.ApplicationPath;

            if (!(appPath == "/"))
                appPath = appPath + "/";


            litBootStrapCSSRef.Text = "<link id='cssbootstrap' href='" + appPath + "css/bootstrap.min.css' rel='stylesheet' />";
            litSiteCSSRef.Text = "<link id='csssite' href='" + appPath + "css/style.css' rel='stylesheet' />";
            litLoginCSSRef.Text = "<link id='csslogin' href='" + appPath + "css/login.css' rel='stylesheet' />";

            litJQueryRef.Text = "<script src='" + appPath + "scripts/jquery-2.1.3.min.js'></script>";
            litBootstrapJSRef.Text = "<script src='" + appPath + "scripts/bootstrap.min.js'></script>";
            litJavascriptRef.Text = "<script src='" + appPath + "scripts/javascript.js'></script>";
        }
    }
}