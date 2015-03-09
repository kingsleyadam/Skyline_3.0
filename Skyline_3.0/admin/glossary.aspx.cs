using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glossary;
using System.Configuration;
using System.Data;

namespace Skyline_3._0.admin
{
    public partial class glossary : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet pageDS = GlossaryDataSet.GetPagesDataSet(connectionString);
                ddPage.DataSource = pageDS;
                ddPage.DataBind();

                ddPageSelection.DataSource = pageDS;
                ddPageSelection.DataBind();

                GlossaryDataSet gds = new GlossaryDataSet(Convert.ToInt32(ddPage.SelectedValue), connectionString);
                grdGlossary.DataSource = gds.GetDataSetByPage();
                grdGlossary.DataBind();
            }
        }

        protected void grdGlossary_PreRender(object sender, EventArgs e)
        {
            GridView gr = (GridView)sender;
            if (gr.Rows.Count > 0)
                gr.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void grdGlossary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button selectButton = (Button)e.Row.FindControl("SelectButton");
                int colCount = grdGlossary.Columns.Count;
                for (int i = 1; i < colCount; i++)
                {
                    e.Row.Cells[i].Attributes["OnClick"] = ClientScript.GetPostBackEventReference(selectButton, "");
                }
            }
        }

        protected void grdGlossary_SelectedIndexChanged(object sender, EventArgs e)
        {
            int glossID = Convert.ToInt32(grdGlossary.SelectedValue);

            if (glossID > 0)
            {
                GlossaryText gt = new GlossaryText(glossID, connectionString);

                ListItem pi;

                pi = ddPageSelection.Items.FindByValue(gt.PageID.ToString());

                if (pi != null)
                    ddPageSelection.SelectedIndex = ddPageSelection.Items.IndexOf(pi);
                else
                    ddPageSelection.SelectedIndex = 0;

                txtGlossaryText.Text = gt.Text;
                txtDispOrder.Text = gt.DisplayOrder.ToString();
                txtLocation.Text = gt.Location;

                pnlAddEditGlossaryText.Visible = true;
                btnUpdate.CommandName = "Update";
                btnUpdate.Text = "Update";
            }
        }

        protected void ddPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlAddEditGlossaryText.Visible = false;
            grdGlossary.Visible = true;
            GlossaryDataSet gds = new GlossaryDataSet(Convert.ToInt32(ddPage.SelectedValue), connectionString);
            grdGlossary.DataSource = gds.GetDataSetByPage();
            grdGlossary.DataBind();
            grdGlossary.SelectedIndex = -1;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            grdGlossary.SelectedIndex = -1;
            pnlAddEditGlossaryText.Visible = false;
            grdGlossary.Visible = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (btnUpdate.CommandName == "Update")
            {
                GlossaryText gt = new GlossaryText(connectionString);
                int dispOrder = 0;
                gt.GlossID = Convert.ToInt32(grdGlossary.SelectedValue);
                gt.PageID = Convert.ToInt32(ddPageSelection.SelectedValue);
                gt.Text = txtGlossaryText.Text;

                if (int.TryParse(txtDispOrder.Text, out dispOrder))
                    gt.DisplayOrder = dispOrder;
                else
                    gt.DisplayOrder = 0;

                gt.Location = txtLocation.Text;

                gt.UpdateDatabase();
            }
            else
            {
                GlossaryText gt = new GlossaryText(connectionString);
                int dispOrder = 0;

                gt.PageID = Convert.ToInt32(ddPageSelection.SelectedValue);
                gt.Text = txtGlossaryText.Text;

                if (int.TryParse(txtDispOrder.Text, out dispOrder))
                    gt.DisplayOrder = dispOrder;
                else
                    gt.DisplayOrder = 0;

                gt.Location = txtLocation.Text;

                gt.Add2DataBase();

                GlossaryDataSet gds = new GlossaryDataSet(gt.PageID, connectionString);
                grdGlossary.DataSource = gds.GetDataSetByPage();
                grdGlossary.DataBind();

                ListItem pi;

                pi = ddPage.Items.FindByValue(gt.PageID.ToString());

                if (pi != null)
                    ddPage.SelectedIndex = ddPage.Items.IndexOf(pi);
                else
                    ddPage.SelectedIndex = 0;
            }

            pnlAddEditGlossaryText.Visible = false;
            grdGlossary.Visible = true;
        }

        protected void btnAddText_Click(object sender, EventArgs e)
        {
            int pageID = Convert.ToInt32(ddPage.SelectedValue);
            int dispOrder = grdGlossary.Rows.Count + 1;
            btnUpdate.CommandName = "Add";
            btnUpdate.Text = "Add";
            pnlAddEditGlossaryText.Visible = true;
            grdGlossary.Visible = false;

            ListItem pi;

            pi = ddPageSelection.Items.FindByValue(pageID.ToString());

            if (pi != null)
                ddPageSelection.SelectedIndex = ddPageSelection.Items.IndexOf(pi);
            else
                ddPageSelection.SelectedIndex = 0;

            txtGlossaryText.Text = "";
            txtDispOrder.Text = dispOrder.ToString();
            txtLocation.Text = "";
        }

        protected void grdGlossary_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int glossID = (int)grdGlossary.DataKeys[e.RowIndex].Value;

            GlossaryText gt = new GlossaryText(glossID, connectionString);
            gt.DeleteFromDatabase();
            grdGlossary.SelectedIndex = -1;
            pnlAddEditGlossaryText.Visible = false;


            GlossaryDataSet gds = new GlossaryDataSet(Convert.ToInt32(ddPage.SelectedValue), connectionString);
            grdGlossary.DataSource = gds.GetDataSetByPage();
            grdGlossary.DataBind();
        }
    }
}