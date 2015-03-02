using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProductInfo;
using UserInfo;
using System.Web.Security;
using System.Configuration;

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
                    ChangeStep(1);

                    grdOrderItems.DataSource = Cart.Instance.Items;
                    grdOrderItems.DataBind();

                    lblTotal.Text = Cart.Instance.GetSubTotal().ToString("c");
                }
                else
                {
                    ChangeStep(0);
                }
            }
        }

        protected void grdOrderItems_PreRender(object sender, EventArgs e)
        {
            GridView gr = (GridView)sender;
            if (gr.Rows.Count > 0)
                gr.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void grdOrderItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ProductSelected")
            {
                int productID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("~/stores/products.aspx?i=" + productID.ToString());
            }
        }

        private void ChangeStep(int stepNum)
        {
            switch (stepNum)
            {
                case 0:
                    {
                        pnlNoItems.Visible = true;

                        pnlBodyStep1.Visible = false;
                        pnlBodyStep2.Visible = false;
                        pnlBodyStep3.Visible = false;

                        pnlFooter.Visible = false;
                        pnlFooterStep1.Visible = false;
                        pnlFooterStep2.Visible = false;
                        pnlFooterStep3.Visible = false;
                        break;
                    }
                case 1:
                    {
                        pnlNoItems.Visible = false;

                        pnlBodyStep1.Visible = true;
                        pnlBodyStep2.Visible = false;
                        pnlBodyStep3.Visible = false;

                        pnlFooter.Visible = true;
                        pnlFooterStep1.Visible = true;
                        pnlFooterStep2.Visible = false;
                        pnlFooterStep3.Visible = false;
                        break;
                    }
                case 2:
                    {
                        pnlNoItems.Visible = false;

                        pnlBodyStep1.Visible = false;
                        pnlBodyStep2.Visible = true;
                        pnlBodyStep3.Visible = false;

                        pnlFooter.Visible = true;
                        pnlFooterStep1.Visible = false;
                        pnlFooterStep2.Visible = true;
                        pnlFooterStep3.Visible = false;
                        break;
                    }
                case 3:
                    {
                        pnlNoItems.Visible = false;

                        pnlBodyStep1.Visible = false;
                        pnlBodyStep2.Visible = false;
                        pnlBodyStep3.Visible = true;

                        pnlFooter.Visible = true;
                        pnlFooterStep1.Visible = false;
                        pnlFooterStep2.Visible = false;
                        pnlFooterStep3.Visible = true;
                        break;
                    }
            }
        }

        protected void ChangeStep_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = (LinkButton)sender;

            int page2GoTo = Convert.ToInt32(lnkBtn.CommandArgument);

            ChangeStep(page2GoTo);

            if (page2GoTo == 2)
            {
                Guid userID = new Guid(Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey.ToString());
                string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
                SkylineUser su = new SkylineUser(userID, connectionString);
                SkylineUserAddress sua = new SkylineUserAddress(su);

                if (sua.CompanyName.Length > 0)
                    txtCompanyName.Text = sua.CompanyName;
                if (sua.FirstName.Length > 0)
                    txtFirstName.Text = sua.FirstName;
                if (sua.LastName.Length > 0)
                    txtLastName.Text = sua.LastName;
                if (sua.PhoneNumber.Length > 0)
                    txtPhoneNumber.Text = sua.PhoneNumber;
                if (sua.Email.Length > 0)
                    txtEmail.Text = sua.Email;
                if (sua.Address1.Length > 0)
                    txtAddress1.Text = sua.Address1;
                if (sua.Address2.Length > 0)
                    txtAddress2.Text = sua.Address2;
                if (sua.City.Length > 0)
                    txtCity.Text = sua.City;
                if (sua.State.Length > 0)
                    txtState.Text = sua.State;
                if (sua.ZipCode.Length > 0)
                    txtZipCode.Text = sua.ZipCode;

            } 
            else if (page2GoTo == 3)
            {
                lblCompanyNameStep3.Text = txtCompanyName.Text;
                lblStreetAddress.Text = txtAddress1.Text;
                if (txtAddress2.Text.Length > 0)
                {
                    lblStreetAddress.Text = lblStreetAddress.Text + "<br />" + txtAddress2.Text;
                }
                lblCountryInfo.Text = txtCity.Text + ", " + txtState.Text + " " + txtZipCode.Text + "<br />" + txtCountry.Text;
                lblPhone.Text = "<abbr title='Phone'>P:</abbr> " + txtPhoneNumber.Text;

                lblFullName.Text = txtFirstName.Text + " " + txtLastName.Text;
                lblEmail.Text = txtEmail.Text;

                grdProductsStep3.DataSource = Cart.Instance.Items;
                grdProductsStep3.DataBind();
            } 
        }

        protected void UpdateQuantity_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = (LinkButton)sender;
            int rowIndex = Convert.ToInt32(lnkBtn.CommandArgument), quantity = 0;
            int productID = (int)grdOrderItems.DataKeys[rowIndex].Value;

            if (lnkBtn.CommandName == "UpdateItem")
            {
                GridViewRow gr = grdOrderItems.Rows[rowIndex];
                TextBox txtQuantity = (TextBox)gr.Cells[2].FindControl("txtQuantity");

                if (txtQuantity != null && productID > 0)
                {
                    CartItem ci = Cart.Instance.Items.Find(x => x.ProductID == productID);
                    Product pr = new Product(ci.ProductID,ci.ProductNum,ci.Name,ci.UnitPrice);


                    if (int.TryParse(txtQuantity.Text,out quantity) && ci != null)
                        Cart.Instance.SetItemQuantity(pr,quantity);
                }

                if (Cart.Instance.Items.Count > 0)
                {
                    grdOrderItems.DataSource = Cart.Instance.Items;
                    grdOrderItems.DataBind();
                }
                else
                {
                    ChangeStep(0);
                }    
            }
            else if (lnkBtn.CommandName == "DeleteItem")
            {
                if (productID > 0)
                {
                    CartItem ci = Cart.Instance.Items.Find(x => x.ProductID == productID);
                    Product pr = new Product(ci.ProductID,ci.ProductNum,ci.Name,ci.UnitPrice);

                    if (ci != null)
                        Cart.Instance.SetItemQuantity(pr, quantity);
                }

                if (Cart.Instance.Items.Count > 0)
                {
                    grdOrderItems.DataSource = Cart.Instance.Items;
                    grdOrderItems.DataBind();
                }
                else
                {
                    ChangeStep(0);
                }                
            }
        }
    }
}