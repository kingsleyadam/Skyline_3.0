using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin;
using System.Configuration;

namespace Skyline_3._0.admin
{
    public partial class change_log : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateAllObjects();
            }
        }

        private void PopulateVersionInfo(int iterationID)
        {
            ChangeLog cl = new ChangeLog(iterationID, connectionString);
            
            lblVersion.Text = cl.Version;
            lblFramework.Text = cl.Framework;
            lblLanguage.Text = cl.Language;
            lblDateApplied.Text = cl.DateApplied.ToShortDateString();

            if (cl.Repository.ToLower().Contains("github"))
            {
                lnkRepo.NavigateUrl = cl.Repository;
                lnkRepo.Text = "GitHub";
                lnkRepo.Enabled = true;
                lnkRepo.CssClass = "";
            }
            else if (cl.Repository.Length > 0 && cl.Repository != "None")
            {
                lnkRepo.NavigateUrl = cl.Repository;
                lnkRepo.Text = "Link";
                lnkRepo.Enabled = true;
                lnkRepo.CssClass = "";
            }
            else
            {
                lnkRepo.NavigateUrl = "";
                lnkRepo.Text = "None";
                lnkRepo.Enabled = false;
                lnkRepo.CssClass = "disabled";
            }
        }

        protected void grdChangeLog_PreRender(object sender, EventArgs e)
        {
            GridView gr = (GridView)sender;
            if (gr.Rows.Count > 0)
                gr.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void ddAllVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iterationID = Convert.ToInt32(ddAllVersions.SelectedValue);
            PopulateVersionInfo(iterationID);

            grdChangeLog.DataSource = ChangeLog.GetVersionChangeLogDataSet(iterationID, connectionString);
            grdChangeLog.DataBind();

            grdChangeLog.SelectedIndex = -1;

            pnlChangeLogID.Visible = false;
        }

        protected void grdChangeLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button selectButton = (Button)e.Row.FindControl("SelectButton");
                if (selectButton != null)
                {
                    e.Row.Attributes["OnClick"] = ClientScript.GetPostBackEventReference(selectButton, "");
                }
            }
        }

        protected void grdChangeLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            int changeLogID = Convert.ToInt32(grdChangeLog.SelectedValue);
            int iterationID = Convert.ToInt32(ddAllVersions.SelectedValue);
            ChangeLog cl = new ChangeLog(iterationID, connectionString);
            ChangeLogEntry cli = new ChangeLogEntry(cl, changeLogID);
            ListItem pi;

            pi = ddChangeType.Items.FindByValue(cli.ChangeTypeID.ToString());

            if (pi != null)
                ddChangeType.SelectedIndex = ddChangeType.Items.IndexOf(pi);
            else
                ddChangeType.SelectedIndex = 0;

            txtDescription.Text = cli.Description;

            pnlChangeLogID.Visible = true;
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            pnlChangeLogID.Visible = false;
            grdChangeLog.SelectedIndex = -1;

            lnkUpdate.Text = "Update";
            lnkUpdate.CommandName = "Update";
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            int iterationID = Convert.ToInt32(ddAllVersions.SelectedValue);
            ChangeLog cl = new ChangeLog(iterationID, connectionString);

            if (lnkUpdate.CommandName == "Update")
            {
                int changeLogID = Convert.ToInt32(grdChangeLog.SelectedValue);
                ChangeLogEntry cli = new ChangeLogEntry(cl, changeLogID);

                cli.ChangeTypeID = Convert.ToInt32(ddChangeType.SelectedValue);
                cli.Description = txtDescription.Text;

                cli.UpdateDataBase();

                grdChangeLog.DataSource = ChangeLog.GetVersionChangeLogDataSet(iterationID, connectionString);
                grdChangeLog.DataBind();
            }
            else if (lnkUpdate.CommandName == "Add")
            {
                if (ddChangeType.SelectedIndex > 0 && txtDescription.Text.Length > 0)
                {
                    ChangeLogEntry cli = new ChangeLogEntry(cl);

                    cli.ChangeTypeID = Convert.ToInt32(ddChangeType.SelectedValue);
                    cli.Description = txtDescription.Text;

                    cli.AddToDataBase();

                    grdChangeLog.DataSource = ChangeLog.GetVersionChangeLogDataSet(iterationID, connectionString);
                    grdChangeLog.DataBind();

                    ddChangeType.SelectedIndex = 0;
                    txtDescription.Text = "";
                }
            }
        }

        protected void btnAddToChangeLog_Click(object sender, EventArgs e)
        {
            lnkUpdate.Text = "Add";
            lnkUpdate.CommandName = "Add";
            ddChangeType.SelectedIndex = 0;
            txtDescription.Text = "";
            grdChangeLog.SelectedIndex = -1;
            pnlChangeLogID.Visible = true;
        }

        protected void lnkEditIteration_Click(object sender, EventArgs e)
        {
            int iterationID = Convert.ToInt32(ddAllVersions.SelectedValue);
            ChangeLog cl = new ChangeLog(iterationID, connectionString);

            txtVersionNumber.Text = cl.Version;
            txtFramework.Text = cl.Framework;
            txtLanguage.Text = cl.Language;
            txtRepository.Text = cl.Repository;

            pnlAddUpdateInteration.Visible = true;
            lnkUpdateIteration.CommandName = "Update";
            lnkUpdateIteration.Text = "Update";
        }

        protected void lnkUpdateIteration_Click(object sender, EventArgs e)
        {
            if (lnkUpdateIteration.CommandName == "Update") 
            {
                int iterationID = Convert.ToInt32(ddAllVersions.SelectedValue);
                ChangeLog cl = new ChangeLog(iterationID, connectionString);

                cl.Version = txtVersionNumber.Text;
                cl.Framework = txtFramework.Text;
                cl.Language = txtLanguage.Text;
                cl.Repository = txtRepository.Text;

                cl.UpdateDataBase();

                PopulateAllObjects(iterationID);
            }
            else if (lnkUpdateIteration.CommandName == "Add")
            {
                ChangeLog cl = new ChangeLog(connectionString);

                cl.Version = txtVersionNumber.Text;
                cl.Framework = txtFramework.Text;
                cl.Language = txtLanguage.Text;
                cl.Repository = txtRepository.Text;

                cl.AddToDataBase();

                PopulateAllObjects();

                pnlAddUpdateInteration.Visible = false;
            }
        }

        protected void lnkCancelIteration_Click(object sender, EventArgs e)
        {
            pnlAddUpdateInteration.Visible = false;
        }

        private void PopulateAllObjects(int iterationID = 0)
        {
            ddAllVersions.DataSource = ChangeLog.GetAllVersions(connectionString);
            ddAllVersions.DataBind();

            ddChangeType.DataSource = ChangeLog.GetAllLogTypeDataSet(connectionString);
            ddChangeType.DataBind();

            if (iterationID == 0)
                iterationID = Convert.ToInt32(ddAllVersions.SelectedValue);
            else
            {
                ListItem li = ddAllVersions.Items.FindByValue(iterationID.ToString());
                ddAllVersions.SelectedIndex = ddAllVersions.Items.IndexOf(li);
            }
                
            PopulateVersionInfo(iterationID);

            grdChangeLog.DataSource = ChangeLog.GetVersionChangeLogDataSet(iterationID, connectionString);
            grdChangeLog.DataBind();
        }

        protected void btnAddNewVersion_Click(object sender, EventArgs e)
        {
            pnlAddUpdateInteration.Visible = true;
            lnkUpdateIteration.CommandName = "Add";
            lnkUpdateIteration.Text = "Add";

            txtVersionNumber.Text = "";
            txtFramework.Text = "";
            txtLanguage.Text = "";
            txtRepository.Text = "";
        }

        protected void btnMakeCurrent_Click(object sender, EventArgs e)
        {
            bool updateDate = false;
            string confirmValue = Request.Form["confirm_value"];

            if (confirmValue == "Yes")
                updateDate = true;

            int iterationID = Convert.ToInt32(ddAllVersions.SelectedValue);
            ChangeLog cl = new ChangeLog(iterationID, connectionString);

            cl.UpdataeCurrent(updateDate);

            PopulateAllObjects(iterationID);
        }
    }
}