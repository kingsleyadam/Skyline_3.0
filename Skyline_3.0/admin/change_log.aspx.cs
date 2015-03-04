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
                ddAllVersions.DataSource = ChangeLog.GetAllVersions(connectionString);
                ddAllVersions.DataBind();

                int iterationID = Convert.ToInt32(ddAllVersions.SelectedValue);
                PopulateVersionInfo(iterationID);

                grdChangeLog.DataSource = ChangeLog.GetVersionChangeLogDataSet(iterationID, connectionString);
                grdChangeLog.DataBind();
            }
        }

        private void PopulateVersionInfo(int iterationID)
        {
            ChangeLog cl = new ChangeLog(iterationID, connectionString);

            lblVersion.Text = cl.Version;
            lblFramework.Text = cl.Framework;
            lblLanguage.Text = cl.Language;
            lblDateApplied.Text = cl.DateApplied.ToShortDateString();
            lblRepo.Text = cl.Repository;
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
    }
}