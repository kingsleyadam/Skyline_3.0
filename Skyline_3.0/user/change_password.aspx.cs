using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Skyline_3._0.user
{
    public partial class change_password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void changePwd_ChangePasswordError(object sender, EventArgs e)
        {
            Panel pnlFailureText = (Panel)changePwd.Controls[0].FindControl("pnlFailureText");
            if (pnlFailureText != null)
            {
                pnlFailureText.Visible = true;
            }
        }
    }
}