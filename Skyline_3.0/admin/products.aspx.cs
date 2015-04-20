using Images;
using ProductInfo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Skyline_3._0.admin
{
    public partial class products : System.Web.UI.Page
    {
        string _connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
                //Set Global Variables
                ViewState["sortBy"] = "Default";
                ViewState["searchString"] = "";
                ViewState["searchField"] = "AllFields";

                Category cat = new Category(connectionString);
                //Load Category Dataset
                int newProducts;
                DataSet catDS = cat.GetCategoryDataSet(false, out newProducts);

                repCategories.DataSource = catDS;
                repCategories.DataBind();

                if (newProducts == 0)
                {
                    lnkNewProducts.Visible = false;
                    ViewState["categoryID"] = 1;
                    lnkCategories.Text = "Category: All Products <span class='caret'></span>";
                }
                else
                {
                    ViewState["categoryID"] = -1;
                }

                PopulateProductsGrid();
            }
            else
            {
                if (fileUploadImage.PostedFile != null)
                {
                    try
                    {
                        string fileExt = System.IO.Path.GetExtension(fileUploadImage.FileName);
                        if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".JPEG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".GIF" || fileExt.ToUpper() == ".BMP")
                        {
                            int productID = (int)grdProducts.DataKeys[grdProducts.SelectedIndex].Value;
                            Product pr = new Product(productID, _connectionString);
                            string imgName = pr.AddImage(false, fileExt.ToLower());
                            string origPath = Server.MapPath("~/images/product_images/orig/");

                            try
                            {
                                fileUploadImage.SaveAs(origPath + imgName + fileExt.ToLower());

                                ImageProcessing ip = new ImageProcessing(imgName + fileExt.ToLower());
                                string statusMessage;
                                ip.ProcessImage(out statusMessage);

                                if (statusMessage.Length > 0)
                                {
                                    lblImageUploadMessage.Text = "<strong>Error!</strong> " + statusMessage;
                                    pnlImageUploadStatus.Visible = true;
                                }
                                else
                                {
                                    pnlImageUploadStatus.Visible = false;
                                }
                            } catch (Exception ex)
                            {
                                pnlImageUploadStatus.Visible = true;
                                lblImageUploadMessage.Text = "<strong>Error!</strong> " + ex.Message + ex.StackTrace;
                            }

                            //Populate Images Repeater
                            repImages.DataSource = pr.GetAdminImagesDataSet();
                            repImages.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            txtSearch.Focus();
        }

        protected void lnkClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ViewState["pageNum"] = 1;
            ViewState["pageGrp"] = 1;
            ViewState["categoryID"] = 1;
            ViewState["searchString"] = "";
            ViewState["searchField"] = "AllFields";
            ViewState["sortBy"] = "Default";

            btnSearch.Text = "Search: All Fields";
            lnkCategories.Text = "Category: All Products <span class='caret'>";

            PopulateProductsGrid();
        }

        protected void CategorySelected(object sender, EventArgs e)
        {
            LinkButton lnkBtn = (LinkButton)sender;
            string categoryName;
            int catID = Convert.ToInt32(lnkBtn.CommandArgument);

            if (lnkBtn.Text.Length > 20)
                categoryName = lnkBtn.Text.Substring(0, 20) + "...";
            else
                categoryName = lnkBtn.Text;

            lnkCategories.Text = "Category: " + categoryName + " <span class='caret'>";

            ViewState["categoryID"] = catID;
            ViewState["pageNum"] = 1;
            ViewState["pageGrp"] = 1;
            PopulateProductsGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
                ViewState["searchString"] = txtSearch.Text;
            else
                ViewState["searchString"] = "";

            ViewState["pageNum"] = 1;
            ViewState["pageGrp"] = 1;
            PopulateProductsGrid();

        }

        protected void SearchInSelected(object sender, EventArgs e)
        {
            LinkButton lnkBtn = (LinkButton)sender;
            string searchField = lnkBtn.CommandArgument;
            string searchText = lnkBtn.Text;

            btnSearch.Text = "Search: " + searchText;
            ViewState["searchField"] = searchField;

            PopulateProductsGrid();
        }

        private void PopulateProductsGrid()
        {
            int catID = (int)ViewState["categoryID"];
            string searchString = ViewState["searchString"].ToString();
            string searchField = ViewState["searchField"].ToString();
            string sortBy = ViewState["sortBy"].ToString();

            AllProducts ap = new AllProducts(_connectionString);

            ap.CategoryID = catID;
            ap.SearchString = searchString;
            ap.SearchField = searchField;
            ap.SortExpression = sortBy;

            DataSet ds = ap.GetAllProductsDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdProducts.DataSource = ds;
                grdProducts.DataBind();
                ViewState["totalPages"] = ap.TotalPages;
            }
            else
            {
                grdProducts.DataSource = null;
                grdProducts.DataBind();
            }
        }

        protected void grdProducts_PreRender(object sender, EventArgs e)
        {
            GridView gr = (GridView)sender;
            if (gr.Rows.Count > 0)
                gr.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void grdProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gr = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button selectButton = (Button)e.Row.FindControl("SelectButton");
                int colCount = gr.Columns.Count;
                for (int i = 1; i < colCount; i++)
                {
                    e.Row.Cells[i].Attributes["OnClick"] = ClientScript.GetPostBackEventReference(selectButton, "");
                }

                //Shorten Description
                Label lblDescription = (Label)e.Row.Cells[4].FindControl("lblDescription");
                if (lblDescription != null)
                {
                    if (lblDescription.Text.Length > 70)
                        lblDescription.Text = lblDescription.Text.Substring(0, 69).TrimEnd() + "...";

                }
            }
        }

        protected void grdProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gr = (GridView)sender;
            gr.PageIndex = e.NewPageIndex;
            PopulateProductsGrid();
        }

        protected void grdProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            int productID = (int)grdProducts.DataKeys[grdProducts.SelectedIndex].Value;

            if (productID > 0)
            {
                Product pr = new Product(productID, _connectionString);

                lblProductName.Text = pr.Name;
                txtProductName.Text = pr.Name;
                txtProductNum.Text = pr.ProductNum;
                txtDescription.Text = pr.Description;
                if (pr.Price > 0)
                    txtPrice.Text = pr.Price.ToString("0.00");
                else
                    txtPrice.Text = "";

                if (pr.Quantity > 0)
                    txtQuantity.Text = pr.Quantity.ToString();
                else
                    txtQuantity.Text = "";

                chkBestSeller.Checked = pr.BestSeller;
                chkSoldOut.Checked = pr.SoldOut;

                //Populate Images Repeater
                repImages.DataSource = pr.GetAdminImagesDataSet();
                repImages.DataBind();

                //Populate Categories Repeater
                repProductCategories.DataSource = pr.GetCategoriesDataSet();
                repProductCategories.DataBind();

                pnlProductInfo.Visible = true;
                pnlProductImages.Visible = true;
                pnlProductCategories.Visible = true;
                pnlProducts.Visible = false;
                pnlProductsFilter.Visible = false;
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            lbtnUpdate.CommandName = "Update";
            lbtnUpdate.Text = "Update";

            pnlProductInfo.Visible = false;
            pnlProductImages.Visible = false;
            pnlProductCategories.Visible = false;
            pnlUpdateSuccess.Visible = false;
            pnlProducts.Visible = true;
            pnlProductsFilter.Visible = true;
            grdProducts.SelectedIndex = -1;
        }

        protected void repProductCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField hf = (HiddenField)e.Item.FindControl("hdnAssigned");
            LinkButton lbtnCategory = (LinkButton)e.Item.FindControl("lbtnCategory");

            bool isAssigned = Convert.ToBoolean(hf.Value.ToString());

            if (isAssigned)
            {
                lbtnCategory.CssClass = lbtnCategory.CssClass + " list-group-item-danger";
                lbtnCategory.Text = lbtnCategory.Text + "<span class='glyphicon glyphicon-minus-sign float-right'></span>";
                lbtnCategory.CommandName = "Remove";
            }
            else
            {
                lbtnCategory.Text = lbtnCategory.Text + "<span class='glyphicon glyphicon-plus-sign float-right'></span>";
                lbtnCategory.CommandName = "Add";
            }
        }

        protected void repProductCategories_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lbtnCategory = (LinkButton)e.Item.FindControl("lbtnCategory");
            HiddenField hf = (HiddenField)e.Item.FindControl("hdnProductID");

            int productID = Convert.ToInt32(hf.Value);
            int categoryID = Convert.ToInt32(lbtnCategory.CommandArgument);
            Product pr = new Product(productID, _connectionString);

            if (lbtnCategory.CommandName == "Add")
                pr.AddToCatagory(categoryID);
            else
                pr.RemoveFromCatagory(categoryID);

            repProductCategories.DataSource = pr.GetCategoriesDataSet();
            repProductCategories.DataBind();
        }

        protected void repImages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField hdn = (HiddenField)e.Item.FindControl("hdnImageID");
            HiddenField hdnIsDefault = (HiddenField)e.Item.FindControl("hdnIsDefault");
            LinkButton lbtnMakeDefault = (LinkButton)e.Item.FindControl("lbtnMakeDefault");
            bool isDefault = false;

            if (hdnIsDefault.Value == "1")
                isDefault = true;

            if (hdn.Value == "-1")
            {
                LinkButton lnkImage = (LinkButton)e.Item.FindControl("lnkImage");
                LinkButton lbtnUploadImage = (LinkButton)e.Item.FindControl("lbtnUploadImage");
                Panel pnlOverlay = (Panel)e.Item.FindControl("pnlOverlay");

                if (lnkImage != null && lbtnMakeDefault != null && lbtnUploadImage != null)
                {
                    lnkImage.OnClientClick = "OpenFileUpload();return false;";
                    lnkImage.CssClass = "image-upload";

                    lbtnMakeDefault.Visible = false;

                    lbtnUploadImage.OnClientClick = "OpenFileUpload();return false;";
                    lbtnUploadImage.Visible = true;

                    pnlOverlay.Visible = false;
                }
            }
            else if (isDefault)
            {
                lbtnMakeDefault.Enabled = false;
                lbtnMakeDefault.Text = "Default Image";
                lbtnMakeDefault.CssClass = "btn btn-primary btn-fullwidth";
                lbtnMakeDefault.Attributes.Add("disabled","");
            }
        }

        protected void repImages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField hdnProductID = (HiddenField)e.Item.FindControl("hdnProductID");
            HiddenField hdn = (HiddenField)e.Item.FindControl("hdnImageID");

            int imgID = Convert.ToInt32(hdn.Value), productID = Convert.ToInt32(hdnProductID.Value);
            Product pr = new Product(productID, _connectionString);

            if (e.CommandName == "Delete")
            {
                if (hdn.Value != "-1")
                {
                    string confirmValue = Request.Form["confirm_value"];
                    if (confirmValue == "Yes")
                    {
                        HiddenField hdnImageName = (HiddenField)e.Item.FindControl("hdnImageName");
                        string imageName = hdnImageName.Value;

                        if (imgID > 0 && productID > 0)
                        {
                            ImageProcessing ip = new ImageProcessing(imageName);

                            System.IO.File.Delete(ip.OriginalPath + ip.ImageName + ip.FileExtension);
                            System.IO.File.Delete(ip.CompressedPath + ip.ImageName + ip.FileExtension);
                            System.IO.File.Delete(ip.ThumbnailPath + ip.ImageName + ip.FileExtension);
                            pr.DeleteImage(imgID);
                        }

                        repImages.DataSource = pr.GetAdminImagesDataSet();
                        repImages.DataBind();
                    }
                }
            }
            else if (e.CommandName == "MakeDefault")
            {
                if (imgID > 0 && productID > 0)
                {
                    pr.SetDefaultImage(imgID);
                }

                repImages.DataSource = pr.GetAdminImagesDataSet();
                repImages.DataBind();
            }
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            Product pr = new Product();
            decimal price;
            int quantity;
            pr.ConnectionString = _connectionString;

            pr.Name = txtProductName.Text;
            pr.ProductNum = txtProductNum.Text;
            pr.Description = txtDescription.Text;
            if (decimal.TryParse(txtPrice.Text, out price))
                pr.Price = price;
            if (int.TryParse(txtQuantity.Text, out quantity))
                pr.Quantity = quantity;
            pr.SoldOut = chkSoldOut.Checked;
            pr.BestSeller = chkBestSeller.Checked;

            if (lbtnUpdate.CommandName == "Update")
            {
                int productID = (int)grdProducts.DataKeys[grdProducts.SelectedIndex].Value;

                if (productID > 0)
                {
                    pr.ProductID = productID;
                    pr.UpdateDatabase();
                    pnlUpdateSuccess.Visible = true;
                }
            }
            else if (lbtnUpdate.CommandName == "Add")
            {
                //Add Product
                pr.AddNewProduct();

                //Populate Images Repeater
                repImages.DataSource = pr.GetAdminImagesDataSet();
                repImages.DataBind();

                //Populate Categories Repeater
                repProductCategories.DataSource = pr.GetCategoriesDataSet();
                repProductCategories.DataBind();

                //Reset Button
                lbtnUpdate.CommandName = "Update";
                lbtnUpdate.Text = "Update";

                //Re-Enable Panels
                pnlUpdateSuccess.Visible = true;
                pnlProductCategories.Visible = true;
                pnlProductImages.Visible = true;
            }
        }

        protected void grdProducts_DataBound(object sender, EventArgs e)
        {
            if (grdProducts.Rows.Count == 0)
                grdProducts.CssClass = "table table-condensed no-margin";
            else if (grdProducts.PageCount > 1)
                grdProducts.CssClass = "table table-striped table-condensed table-hover no-margin table-hover-paged";
            else
                grdProducts.CssClass = "table table-striped table-condensed table-hover no-margin";
        }

        protected void lbtnAddNew_Click(object sender, EventArgs e)
        {
            ClearProductInfo();
            lbtnUpdate.CommandName = "Add";
            lbtnUpdate.Text = "Add Product";

            pnlProductInfo.Visible = true;
            pnlProductImages.Visible = false;
            pnlProductCategories.Visible = false;
            pnlProducts.Visible = false;
            pnlProductsFilter.Visible = false;
        }

        private void ClearProductInfo()
        {
            txtProductName.Text = "";
            txtProductNum.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";
            chkBestSeller.Checked = false;
            chkSoldOut.Checked = false;
        }

        protected void grdProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int productID = Convert.ToInt32(grdProducts.DataKeys[e.RowIndex].Value);

            Product pr = new Product(productID, _connectionString);

            pr.DeleteProduct();

            PopulateProductsGrid();
        }
    }
}