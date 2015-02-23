using ProductInfo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
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
                //Set Global Variables
                ViewState["pageGrpSize"] = 5;
                ViewState["pageNum"] = 1;
                ViewState["pageGrp"] = 1;
                ViewState["itemsPerPage"] = 8;
                ViewState["categoryID"] = 1;
                ViewState["searchString"] = "";
                ViewState["searchField"] = "AllFields";
                ViewState["sortBy"] = "Default";

                AllProducts ap = PopulateListView();

                //Load Category Dataset
                DataSet catDS = ap.GetCategoryDataSet(false);
                repCategories.DataSource = catDS;
                repCategories.DataBind();
            }
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
            PopulateListView();
        }

        protected void SearchInSelected(object sender, EventArgs e)
        {

        }

        protected void btnSearchCancel_Click(object sender, EventArgs e)
        {

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
            DataSet ds = ap.GetDataSet();
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

                DataSet pagerDS = ap.GetPagerDataSet(true, pageGrp, pageGrpSize);
                repPager.DataSource = pagerDS;
                repPager.DataBind();

                foreach (RepeaterItem ri in repPager.Items)
                {
                    LinkButton lnkBtn = (LinkButton)ri.FindControl("lnkPageNum");
                    HtmlGenericControl liPageNum = (HtmlGenericControl)ri.FindControl("liPageNum");

                    if (lnkBtn != null && liPageNum != null)
                    {
                        if (pageNum == Convert.ToInt32(lnkBtn.CommandArgument))
                        {
                            liPageNum.Attributes.Add("class", "active");
                            lnkBtn.Enabled = false;
                        }
                    }
                }

                pnlPageSelect.Visible = true;

                if (pageGrp == 1)
                    phPagePrevGrp.Visible = false;
                else
                    phPagePrevGrp.Visible = true;

                if (pageGrp == maxPageGrp)
                    phPageNextGrp.Visible = false;
                else
                    phPageNextGrp.Visible = true;

                if (pageNum == 1)
                {
                    phFirstPage.Visible = false;
                }
                else
                {
                    phFirstPage.Visible = true;
                }

                if (pageNum == ap.TotalPages)
                {
                    phLastPage.Visible = false;
                }
                else
                {
                    phLastPage.Visible = true;
                }
            }
            else
            {
                pnlPageSelect.Visible = false;
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

            PopulateListView();
        }
    }
}