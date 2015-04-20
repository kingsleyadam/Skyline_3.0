using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProductInfo;
using System.Configuration;

namespace Skyline_3._0.admin
{
    public partial class category : System.Web.UI.Page
    {
        string _connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdCategories.DataSource = Category.GetCategoryAdminDataSet(false, _connectionString);
                grdCategories.DataBind();
            }
        }

        protected void lbtnAddNew_Click(object sender, EventArgs e)
        {
            pnlNewCategory.Visible = true;
            pnlCategories.Visible = false;
        }

        protected void grdCategories_PreRender(object sender, EventArgs e)
        {
            GridView gr = (GridView)sender;
            if (gr.Rows.Count > 0)
            {
                gr.HeaderRow.TableSection = TableRowSection.TableHeader;

                gr.HeaderRow.TableSection = TableRowSection.TableHeader;
                if (gr.EditIndex != -1)
                    gr.Columns[0].Visible = true;
                else
                    gr.Columns[0].Visible = false;
            }
        }

        protected void grdCategories_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gr = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow && gr.SelectedIndex != e.Row.RowIndex)
            {
                Button selectButton = (Button)e.Row.FindControl("EditButton");
                int colCount = gr.Columns.Count;

                for (int i = 2; i < colCount; i++)
                {
                    e.Row.Cells[i].Attributes["OnClick"] = ClientScript.GetPostBackEventReference(selectButton, "");
                }
            }
        }

        protected void grdCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdCategories.EditIndex = e.NewEditIndex;
            grdCategories.SelectedIndex = e.NewEditIndex;

            //Rebind
            grdCategories.DataSource = Category.GetCategoryAdminDataSet(false, _connectionString);
            grdCategories.DataBind();
        }

        protected void grdCategories_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdCategories.EditIndex = -1;
            grdCategories.SelectedIndex = -1;
            //Rebind
            grdCategories.DataSource = Category.GetCategoryAdminDataSet(false, _connectionString);
            grdCategories.DataBind();
        }

        protected void grdCategories_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView gr = (GridView)sender;
            GridViewRow gvr = gr.Rows[e.RowIndex];

            int categoryID = Convert.ToInt32(gr.DataKeys[e.RowIndex].Value);

            if (categoryID > 0)
            {
                TextBox txtName = (TextBox)gvr.Cells[2].FindControl("txtName");
                CheckBox cb = (CheckBox)gvr.Cells[5].FindControl("chkIsActive");

                if (txtName != null && cb != null)
                {
                    Category cat = new Category(categoryID, txtName.Text, cb.Checked, _connectionString);
                    cat.UpdateDataBase();

                    gr.EditIndex = -1;
                    gr.SelectedIndex = -1;

                    //Rebind
                    grdCategories.DataSource = Category.GetCategoryAdminDataSet(false, _connectionString);
                    grdCategories.DataBind();
                }
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            pnlCategories.Visible = true;
            pnlNewCategory.Visible = false;
        }

        protected void lbtnAddCategory_Click(object sender, EventArgs e)
        {
            Category cat = new Category(txtCategoryName.Text, txtAbbrev.Text, _connectionString);
            cat.AddToDataBase();

            grdCategories.DataSource = Category.GetCategoryAdminDataSet(false, _connectionString);
            grdCategories.DataBind();

            pnlCategories.Visible = true;
            pnlNewCategory.Visible = false;
        }

        protected void grdCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                GridView gr = (GridView)sender;
                int categoryID = Convert.ToInt32(gr.DataKeys[e.RowIndex].Value);

                Category cat = new Category(categoryID, _connectionString);
                cat.DeleteFromDataBase();

                if (gr.EditIndex == e.RowIndex)
                {
                    gr.EditIndex = -1;
                    gr.SelectedIndex = -1;
                }

                grdCategories.DataSource = Category.GetCategoryAdminDataSet(false, _connectionString);
                grdCategories.DataBind();
            }
        }
    }
}