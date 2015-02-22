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
            GridView gr = (GridView)sender;
            if (gr.Rows.Count > 0)
                gr.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void grdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button selectButton = (Button)e.Row.FindControl("btnOrderInfo");
                if (selectButton != null)
                {
                    e.Row.Attributes["OnClick"] = ClientScript.GetPostBackEventReference(selectButton, "");
                }
            }
        }

        protected void grdOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OrderInfo")
            {
                int orderID = Convert.ToInt32(e.CommandArgument);

                if (orderID > 0)
                {
                    Response.Redirect("~/user/orders_info.aspx?o=" + orderID.ToString());
                }

            }
        }
    }
}