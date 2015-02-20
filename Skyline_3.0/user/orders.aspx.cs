using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserInfo;

namespace Skyline_3._0.user
{
    public partial class orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Guid userID = new Guid(Membership.GetUser().ProviderUserKey.ToString());
                string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
                SkylineUser su = new SkylineUser(userID, connectionString);
                DataSet orderDS = su.GetWebSiteOrders();

                grdOrder.DataSource = orderDS;
                grdOrder.DataBind();

                if (orderDS.Tables["WebOrders"].Rows.Count > 0)
                {
                    pnlNoOrders.Visible = false;
                }
                else
                {
                    pnlSiteOrders.Visible = false;
                }
            }
        }

        protected void grdOrder_PreRender(object sender, EventArgs e)
        {
            if (grdOrder.Rows.Count > 0)
                grdOrder.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}