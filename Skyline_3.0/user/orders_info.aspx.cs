using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserInfo;

namespace Skyline_3._0.user
{
    public partial class orders_info : System.Web.UI.Page
    {
        private decimal totalPrice = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int orderID = Convert.ToInt32(Request.QueryString["o"]);
                string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
                UserOrder uo = new UserOrder(orderID, connectionString);

                if (orderID > 0 && uo.SkylineID > 0)
                {
                    lblOrderNum.Text = uo.OrderNum;
                    lblCompanyName.Text = uo.CompanyName;
                    lblStreetAddress.Text = uo.Address1;
                    if (uo.Address2.Length > 0)
                    {
                        lblStreetAddress.Text = lblStreetAddress.Text + "<br />" + uo.Address2;
                    }
                    lblCountryInfo.Text = uo.City + ", " + uo.State + " " + uo.ZipCode;
                    lblPhone.Text = "<abbr title='Phone'>P:</abbr> " + uo.Phone;

                    lblFullName.Text = uo.FirstName + " " + uo.LastName;
                    lblEmail.Text = uo.Email;

                    totalPrice = uo.TotalPrice;

                    //Order Items
                    DataSet itemDS = uo.GetItemDataSet();
                    grdOrderItems.DataSource = itemDS;
                    grdOrderItems.DataBind();
                }
                else
                {
                    pnlNoOrders.Visible = true;
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

        protected void grdOrderItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "<strong>Total: " + totalPrice.ToString("c") + "</strong>";
            }
        }


    }
}