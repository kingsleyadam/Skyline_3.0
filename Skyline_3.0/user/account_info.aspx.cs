using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserInfo;

namespace Skyline_3._0.user
{
    public partial class account_info : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateAccountInfo();
            }
        }

        protected void PopulateAccountInfo()
        {
            Guid userID = new Guid (Membership.GetUser().ProviderUserKey.ToString());
            string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
            SkylineUser su = new SkylineUser(userID, connectionString);

            txtFirstName.Text = su.FirstName;
            txtLastName.Text = su.LastName;
            txtCompanyName.Text = su.CompanyName;
            txtEmail.Text = su.Email;
            chkEmailNotify.Checked = su.Notifications;
            lblLastLoginDate.Text = su.LastLoginDate.ToShortDateString();
            lblUsername.Text = su.UserName;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Guid userID = new Guid(Membership.GetUser().ProviderUserKey.ToString());
            string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
            SkylineUser su = new SkylineUser(userID, connectionString);

            su.FirstName = txtFirstName.Text;
            su.LastName = txtLastName.Text;
            su.CompanyName = txtCompanyName.Text;
            su.Email = txtEmail.Text;
            su.Notifications = chkEmailNotify.Checked;
            su.UpdateDatabase();
        }
    }
}