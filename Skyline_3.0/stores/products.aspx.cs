using ProductInfo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Skyline_3._0.stores
{
    public partial class products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
                //Set Global Variables
                ViewState["pageGrpSize"] = 5;
                ViewState["pageNum"] = 1;
                ViewState["pageGrp"] = 1;
                ViewState["itemsPerPage"] = 8;
                ViewState["sortBy"] = "Default";


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

                //Specific Item
                int productID;
                if (Request.QueryString["i"] != null && int.TryParse(Request.QueryString["i"], out productID))
                {
                    ViewState["searchString"] = productID.ToString();
                    ViewState["searchField"] = "ProductID";

                    PopulateListView();

                    if (lvProducts.Items.Count == 1)
                    {
                        LinkButton btnSelect = (LinkButton)lvProducts.Items[0].FindControl("btnSelect");
                        ProductSelected((object)btnSelect);
                    }
                    else
                    {
                        ViewState["searchString"] = "";
                        ViewState["searchField"] = "AllFields";

                        //Populate Products
                        PopulateListView();
                    }
                }
                else //Normal
                {
                    ViewState["searchString"] = "";
                    ViewState["searchField"] = "AllFields";

                    //Populate Products
                    PopulateListView();
                }

                if (Cart.Instance.Items.Count == 0)
                {
                    pnlCartItemCount.Visible = false;
                }
                else
                {
                    pnlCartItemCount.Visible = true;
                    lblCartItemCount.Text = Cart.Instance.Items.Count.ToString();
                }
            }

            txtSearch.Focus();
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            LinkButton lnkBtn = (LinkButton)sender;

            if (lnkBtn.CommandName == "ChangePage")
            {
                int pageNum = (int)ViewState["pageNum"];
                int pageGrpSize = (int)ViewState["pageGrpSize"];

                if (lnkBtn.CommandArgument == "FirstPage")
                    pageNum = 1;
                else if (lnkBtn.CommandArgument == "PreviousPage")
                    pageNum--;
                else if (lnkBtn.CommandArgument == "NextPage")
                    pageNum++;
                else if (lnkBtn.CommandArgument == "LastPage")
                    pageNum = (int)ViewState["totalPages"];
                else
                    pageNum = Convert.ToInt32(lnkBtn.CommandArgument);

                ViewState["pageGrp"] = Convert.ToInt32(Math.Ceiling((decimal)pageNum / (decimal)pageGrpSize)); ;
                ViewState["pageNum"] = pageNum;

                PopulateListView();
            }
            else if (lnkBtn.CommandName == "ChangeGrp")
            {
                int pageGrp = (int)ViewState["pageGrp"], pageNum = (int)ViewState["pageNum"], pageGrpSize = (int)ViewState["pageGrpSize"];

                if (lnkBtn.CommandArgument == "NextGrp")
                {
                    pageGrp++;
                    pageNum = ((pageGrp - 1) * pageGrpSize) + 1;
                }
                else if (lnkBtn.CommandArgument == "PrevGrp")
                {
                    pageGrp--;
                    pageNum = ((pageGrp - 1) * pageGrpSize) + 1;
                    pageNum = (pageNum + (pageGrpSize - 1));
                }

                ViewState["pageGrp"] = pageGrp;
                ViewState["pageNum"] = pageNum;

                PopulateListView();
            }
        }

        protected void SortSelected(object sender, EventArgs e)
        {
            LinkButton lnkBtn = (LinkButton)sender;

            string sortBy = lnkBtn.CommandArgument;
            ViewState["sortBy"] = sortBy;
            PopulateListView();

            btnSortBy.Text = "Sort By: <strong>" + lnkBtn.Text + "</strong> <span class='caret'></span>";
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
            PopulateListView();
        }

        protected void SearchInSelected(object sender, EventArgs e)
        {
            LinkButton lnkBtn = (LinkButton)sender;
            string searchField = lnkBtn.CommandArgument;
            string searchText = lnkBtn.Text;

            btnSearch.Text = "Search: " + searchText;
            ViewState["searchField"] = searchField;

            PopulateListView();
        }

        protected void SelectPage(object sender, EventArgs e)
        {
            LinkButton lnkBtn = (LinkButton)sender;
            int pageNum = Convert.ToInt32(lnkBtn.CommandArgument);
            lnkPageDropDown.Text = pageNum.ToString() + " <span class='caret'></span>";

            ViewState["pageNum"] = pageNum;
            PopulateListView();
        }

        protected void Add2Cart(object sender, EventArgs e)
        {
            LinkButton btnAdd2Cart = (LinkButton)sender;
            int productID = Convert.ToInt32(btnAdd2Cart.CommandArgument), quantity = 1;
            Product prod = new Product();

            if (btnAdd2Cart.CommandName == "Add2CartFromInfo")
            {
                if (productID > 0)
                {
                    if (!int.TryParse(txtQuantityFromInfo.Text, out quantity))
                        quantity = 1;

                    decimal price = Convert.ToDecimal(hdnProductInfoPrice.Value);
                    string productNum = lblProductNum.Text;
                    string productName = lblProductName.Text;
                    string imgThumb = imgProductImg.ImageUrl;

                    prod.ProductID = productID;
                    prod.ProductNum = productNum;
                    prod.Name = productName;
                    prod.Price = price;
                    prod.Thumbnail = imgThumb;

                    if (quantity == 0)
                        txtQuantityFromInfo.Text = "1";
                }
            }
            else
            {
                Panel pnlThumnnail = (Panel)btnAdd2Cart.Parent;
                Panel pnlAdd2Order = (Panel)pnlThumnnail.Parent;
                ListViewDataItem lvDataitem = (ListViewDataItem)pnlAdd2Order.Parent;
                int lvIndex = lvDataitem.DisplayIndex;

                if (productID > 0)
                {
                    TextBox txtQuantity = (TextBox)lvProducts.Items[lvIndex].FindControl("txtQuantity");
                    HiddenField hdnPrice = (HiddenField)lvProducts.Items[lvIndex].FindControl("hdnPrice");
                    HiddenField hdnImgThumb = (HiddenField)lvProducts.Items[lvIndex].FindControl("hdnImgThumb");
                    Label lblProductNum = (Label)lvProducts.Items[lvIndex].FindControl("lblProductNum");
                    LinkButton btnSelect = (LinkButton)lvProducts.Items[lvIndex].FindControl("btnSelect");

                    if (txtQuantity != null && hdnPrice != null && lblProductNum != null && btnSelect != null && hdnImgThumb != null)
                    {
                        if (!int.TryParse(txtQuantity.Text, out quantity))
                            quantity = 1;

                        decimal price = Convert.ToDecimal(hdnPrice.Value);
                        string productNum = lblProductNum.Text;
                        string productName = btnSelect.Text;
                        string imgThumb = hdnImgThumb.Value;

                        prod.ProductID = productID;
                        prod.ProductNum = productNum;
                        prod.Name = productName;
                        prod.Price = price;
                        prod.Thumbnail = imgThumb;

                        if (quantity == 0)
                            txtQuantity.Text = "1";
                    }
                }
            }

            if (prod != null)
            {
                if (!Cart.Instance.Items.Contains(new CartItem(prod)))
                {
                    Cart.Instance.AddItem(prod);
                }

                Cart.Instance.SetItemQuantity(prod, quantity);

                if (btnAdd2Cart.CommandName == "Add2CartFromInfo")
                    AdjustLVInfoByCartItem(new CartItem(prod));

                if (quantity > 0)
                    btnAdd2Cart.Text = "Update Quantity";
                else
                    btnAdd2Cart.Text = "Add To Order";

                if (Cart.Instance.Items.Count == 0)
                {
                    pnlCartItemCount.Visible = false;
                }
                else
                {
                    pnlCartItemCount.Visible = true;
                    lblCartItemCount.Text = Cart.Instance.Items.Count.ToString();
                }
            }
        }

        private AllProducts PopulateListView()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
            int catID = (int)ViewState["categoryID"];
            int pageNum = (int)ViewState["pageNum"];
            int itemsPerPage = (int)ViewState["itemsPerPage"];
            string searchString = ViewState["searchString"].ToString();
            string searchField = ViewState["searchField"].ToString();
            string sortBy = ViewState["sortBy"].ToString();

            AllProducts ap = new AllProducts(catID, searchString, searchField, sortBy, pageNum, itemsPerPage, connectionString);
            DataSet ds = ap.GetPagedDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvProducts.DataSource = ds;
                lvProducts.DataBind();
                ViewState["totalPages"] = ap.TotalPages;
            }
            else
            {
                lvProducts.DataSource = null;
                lvProducts.DataBind();
            }
            PopulatePager(ap);

            return ap;
        }

        private void PopulatePager(AllProducts ap)
        {
            if (ap.TotalPages > 0)
            {
                //Get Global Variables
                int pageGrpSize = (int)ViewState["pageGrpSize"];
                int pageNum = (int)ViewState["pageNum"];
                int pageGrp = (int)ViewState["pageGrp"];
                int maxPageGrp = (int)Math.Ceiling((decimal)ap.TotalPages / (decimal)pageGrpSize);

                //Populate Pager
                repPages.DataSource = ap.GetPagerDataSet(false, true);
                repPages.DataBind();

                lnkPageDropDown.Text = pageNum.ToString() + " <span class='caret'></span>";
                lblPageInfo1.Text = "Page: ";
                lblPageInfo2.Text = " of " + ap.TotalPages;

                pnlPageSelect.Visible = true;
                pnlProductHeader.Visible = true;

                if (pageNum == ap.TotalPages)
                {
                    lnkLastPage.CssClass = "disabled";
                    lnkLastPage.Enabled = false;

                    lnkNextPage.CssClass = "disabled";
                    lnkNextPage.Enabled = false;
                }
                else
                {
                    lnkLastPage.CssClass = "";
                    lnkLastPage.Enabled = true;

                    lnkNextPage.CssClass = "page-btn-primary";
                    lnkNextPage.Enabled = true;
                }

                if (pageNum == 1)
                {
                    lnkFirstPage.CssClass = "disabled";
                    lnkFirstPage.Enabled = false;

                    lnkPrevPage.CssClass = "disabled";
                    lnkPrevPage.Enabled = false;

                }
                else
                {
                    lnkFirstPage.CssClass = "";
                    lnkFirstPage.Enabled = true;

                    lnkPrevPage.CssClass = "page-btn-primary";
                    lnkPrevPage.Enabled = true;
                }

            }
            else
            {
                pnlPageSelect.Visible = false;
                pnlProductHeader.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
                ViewState["searchString"] = txtSearch.Text;
            else
                ViewState["searchString"] = "";

            ViewState["pageNum"] = 1;
            ViewState["pageGrp"] = 1;
            PopulateListView();

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

            PopulateListView();
        }

        protected void lvProducts_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Panel pnlThumnnail = (Panel)e.Item.FindControl("pnlThumnnail");
            Panel pnlPrice = (Panel)e.Item.FindControl("pnlPrice");
            Panel pnlAdd2Order = (Panel)e.Item.FindControl("pnlAdd2Order");

            HiddenField hdnIsBestSeller = (HiddenField)e.Item.FindControl("hdnIsBestSeller");
            HiddenField hdnSoldOut = (HiddenField)e.Item.FindControl("hdnSoldOut");
            bool isBestSeller = Convert.ToBoolean(hdnIsBestSeller.Value);
            bool isSoldOut = Convert.ToBoolean(hdnSoldOut.Value);

            if (isBestSeller)
                pnlThumnnail.CssClass = "thumbnail thumbnail-bestseller";

            if (Roles.IsUserInRole("Demo"))
                pnlPrice.Visible = false;

            if (isSoldOut || !Roles.IsUserInRole("Order"))
            {
                if (pnlAdd2Order != null)
                    pnlAdd2Order.Visible = false;
            }
            else
            {
                if (pnlAdd2Order != null)
                {
                    pnlAdd2Order.Visible = true;
                }

                TextBox txtQuantity = (TextBox)e.Item.FindControl("txtQuantity");
                LinkButton btnAdd2Cart = (LinkButton)e.Item.FindControl("btnAdd2Cart");

                if (txtQuantity != null && btnAdd2Cart != null)
                {
                    int productID = Convert.ToInt32(btnAdd2Cart.CommandArgument);
                    CartItem ci = Cart.Instance.Items.Find(x => x.ProductID == productID);

                    if (ci != null)
                    {
                        txtQuantity.Text = ci.Quantity.ToString();
                        btnAdd2Cart.Text = "Update Quantity";
                    }
                    else
                        btnAdd2Cart.Text = "Add To Order";
                }
            }
        }

        //Project Specific Info
        protected void ProductSelected(object sender, EventArgs e = null)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
            LinkButton lnkSelected = (LinkButton)sender;
            int productID = Convert.ToInt32(lnkSelected.CommandArgument);
            Panel pnlThumnnail = (Panel)lnkSelected.Parent;
            ListViewDataItem lvDataItem = (ListViewDataItem)pnlThumnnail.Parent;

            if (productID > 0 && pnlThumnnail != null && lvDataItem != null)
            {
                Product prod = GetProductFromLVItem(lvDataItem);

                btnAdd2CartFromInfo.CommandArgument = prod.ProductID.ToString();
                lblProductName.Text = prod.Name;
                imgProductImg.ImageUrl = prod.FullImage;
                lnkProductImage.NavigateUrl = prod.OriginalImage;
                lblProductNum.Text = prod.ProductNum;
                lblProductDescription.Text = prod.Description;

                lblProductInfoPrice.Text = prod.Price.ToString("c");
                hdnProductInfoPrice.Value = prod.Price.ToString();

                if (prod.SoldOut)
                {
                    pnlSoldOut.Visible = true;
                    pnlProductInfoAdd2Order.Visible = false;
                }
                else
                {
                    pnlSoldOut.Visible = false;
                    pnlProductInfoAdd2Order.Visible = true;
                }

                if (!Roles.IsUserInRole("Order"))
                    pnlProductInfoAdd2Order.Visible = false;

                if (Roles.IsUserInRole("Demo"))
                    pnlPrice.Visible = false;

                if (prod.BestSeller)
                    pnlBestSeller.Visible = true;
                else
                    pnlBestSeller.Visible = false;

                if (!Cart.Instance.Items.Contains(new CartItem(prod)))
                {
                    btnAdd2CartFromInfo.Text = "Add To Cart";
                    txtQuantityFromInfo.Text = "1";
                }
                else
                {
                    CartItem ci = Cart.Instance.Items.Find(x => x.ProductID == productID);
                    btnAdd2CartFromInfo.Text = "Update Quantity";
                    txtQuantityFromInfo.Text = ci.Quantity.ToString();
                }


                //Populate Other Image
                DataSet imgDataSet = prod.GetImagesDataSet(false, connectionString);
                repProductImages.DataSource = imgDataSet;
                repProductImages.DataBind();

                pnlProductInfo.Visible = true;
                pnlProductsGrid.Visible = false;
                pnlProductsFilter.Visible = false;

                txtQuantityFromInfo.Focus();
            }

        }

        private Product GetProductFromLVItem(ListViewDataItem lvDataItem)
        {
            Product prod = new Product();

            HiddenField hdnProductID = (HiddenField)lvDataItem.FindControl("hdnProductID");
            HiddenField hdnPrice = (HiddenField)lvDataItem.FindControl("hdnPrice");
            HiddenField hdnImgThumb = (HiddenField)lvDataItem.FindControl("hdnImgThumb");
            HiddenField hdnImgURL = (HiddenField)lvDataItem.FindControl("hdnImgURL");
            HiddenField hdnImgOrig = (HiddenField)lvDataItem.FindControl("hdnImgOrig");
            HiddenField hdnDescription = (HiddenField)lvDataItem.FindControl("hdnDescription");
            HiddenField hdnBestSeller = (HiddenField)lvDataItem.FindControl("hdnBestSeller");
            HiddenField hdnSoldOut = (HiddenField)lvDataItem.FindControl("hdnSoldOut");
            Label lblProductNum = (Label)lvDataItem.FindControl("lblProductNum");
            LinkButton btnSelect = (LinkButton)lvDataItem.FindControl("btnSelect");

            if (hdnProductID != null)
            {
                int productID;
                if (int.TryParse(hdnProductID.Value, out productID))
                {
                    prod.ProductID = productID;
                }
            }

            if (btnSelect != null)
            {
                string name = btnSelect.Text;
                prod.Name = name;
            }

            if (hdnImgThumb != null)
            {
                string productNum = lblProductNum.Text;
                prod.ProductNum = productNum;
            }

            if (hdnPrice != null)
            {
                decimal price;
                if (decimal.TryParse(hdnPrice.Value, out price))
                {
                    prod.Price = price;
                }
            }

            if (hdnImgThumb != null)
            {
                string thumbNail = hdnImgThumb.Value;
                prod.Thumbnail = thumbNail;
            }

            if (hdnImgURL != null)
            {
                string fullImage = hdnImgURL.Value;
                prod.FullImage = fullImage;
            }

            if (hdnImgOrig != null)
            {
                string originalImage = hdnImgOrig.Value;
                prod.OriginalImage = originalImage;
            }

            if (hdnDescription != null)
            {
                string description = hdnDescription.Value;
                prod.Description = description;
            }

            if (hdnBestSeller != null)
            {
                bool bestSeller;
                if (bool.TryParse(hdnBestSeller.Value, out bestSeller))
                {
                    prod.BestSeller = bestSeller;
                }
            }

            if (hdnSoldOut != null)
            {
                bool soldOut;
                if (bool.TryParse(hdnSoldOut.Value, out soldOut))
                {
                    prod.SoldOut = soldOut;
                }
            }

            return prod;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string searchField = ViewState["searchField"].ToString();
            if (searchField == "ProductID")
            {
                ViewState["searchString"] = "";
                ViewState["searchField"] = "AllFields";
                PopulateListView();
            }
            pnlProductInfo.Visible = false;
            pnlProductsGrid.Visible = true;
            pnlProductsFilter.Visible = true;
        }

        private void AdjustLVInfoByCartItem(CartItem ci)
        {
            ListViewItem lvItem = null;

            foreach (ListViewItem lv in lvProducts.Items)
            {
                HiddenField hdnProductID = (HiddenField)lv.FindControl("hdnProductID");
                int lvProductID = Convert.ToInt32(hdnProductID.Value);

                if (lvProductID == ci.ProductID)
                {
                    lvItem = lv;
                    break;
                }
            }

            if (lvItem != null)
            {
                LinkButton btnAdd2Cart = (LinkButton)lvItem.FindControl("btnAdd2Cart");
                TextBox txtQuantity = (TextBox)lvItem.FindControl("txtQuantity");
                if (!Cart.Instance.Items.Contains(ci))
                {
                    btnAdd2Cart.Text = "Add To Cart";
                    txtQuantity.Text = "1";
                }
                else
                {
                    CartItem ci2 = Cart.Instance.Items.Find(x => x.ProductID == ci.ProductID);
                    btnAdd2Cart.Text = "Update Quantity";
                    txtQuantity.Text = ci2.Quantity.ToString();
                }
            }
        }
    }
}