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
            string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
            int catID = (int)ViewState["categoryID"];
            string searchString = ViewState["searchString"].ToString();
            string searchField = ViewState["searchField"].ToString();
            string sortBy = ViewState["sortBy"].ToString();

            AllProducts ap = new AllProducts(connectionString);

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
            }
        }

        protected void grdProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gr = (GridView)sender;
            gr.PageIndex = e.NewPageIndex;
            PopulateProductsGrid();
        }
    }
}