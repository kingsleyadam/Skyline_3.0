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

                string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
                AllProducts ap = new AllProducts((int)ViewState["categoryID"], (int)ViewState["pageNum"], (int)ViewState["itemsPerPage"], connectionString);
                PopulateListView(ap);
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
            LinkButton lnkBtn = (LinkButton)sender;

            if (lnkBtn.CommandName == "ChangePage")
            {
                int catID = (int)ViewState["categoryID"];
                int pageNum = (int)ViewState["pageNum"];
                int perPage = (int)ViewState["itemsPerPage"];
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

                AllProducts ap = new AllProducts(catID, pageNum, perPage, connectionString);
                PopulateListView(ap);
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

                AllProducts ap = new AllProducts((int)ViewState["categoryID"], (int)ViewState["pageNum"], (int)ViewState["itemsPerPage"], connectionString);
                PopulateListView(ap);
            }
        }

        private void PopulateListView(AllProducts ap)
        {
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
                    phPrevPage.Visible = false;
                    phFirstPage.Visible = false;
                }
                else
                {
                    phPrevPage.Visible = true;
                    phFirstPage.Visible = true;
                }

                if (pageNum == ap.TotalPages)
                {
                    phNextPage.Visible = false;
                    phLastPage.Visible = false;
                }
                else
                {
                    phNextPage.Visible = true;
                    phLastPage.Visible = true;
                }
            }
            else
            {
                pnlPageSelect.Visible = false;
            }
        }
    }
}