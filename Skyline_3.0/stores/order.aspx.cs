using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProductInfo;

namespace Skyline_3._0.stores
{
    public partial class order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Cart.Instance.Items.Count > 0)
                {
                    pnlOrderInfo.Visible = true;
                    grdOrderItems.DataSource = Cart.Instance.Items;
                    grdOrderItems.DataBind();
                }
                else
                {
                    pnlNoItems.Visible = true;
                }
            }
        }

        protected void grdOrderItems_PreRender(object sender, EventArgs e)
        {
            GridView gr = (GridView)sender;
            if (gr.Rows.Count > 0)
                gr.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}