using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserInfo;

namespace Skyline_3._0.admin
{
    public partial class user_list : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridView();
            }
        }

        protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != grdUsers.EditIndex)
            {
                Button selectButton = (Button)e.Row.FindControl("EditButton");
                if (selectButton != null)
                {
                    e.Row.Attributes["OnClick"] = ClientScript.GetPostBackEventReference(selectButton, "");
                }
            }
        }

        protected void grdUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdUsers.EditIndex = e.NewEditIndex;
            PopulateGridView();
        }

        protected void grdUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdUsers.EditIndex = -1;
            PopulateGridView();
        }

        private void PopulateGridView()
        {
            DataSet ds = SkylineUser.GetAllUsers(connectionString);

            if (ds.Tables["UserList"].Rows.Count > 0)
            {
                grdUsers.CssClass = grdUsers.CssClass + " table-hover";
                grdUsers.DataSource = ds;
            }
            else
                grdUsers.DataSource = null;

            grdUsers.DataBind();
        }

        protected void grdUsers_PreRender(object sender, EventArgs e)
        {
            GridView gr = (GridView)sender;
            if (gr.Rows.Count > 0)
            {
                gr.HeaderRow.TableSection = TableRowSection.TableHeader;
                if (gr.EditIndex != -1)
                    gr.Columns[0].Visible = true;
                else
                    gr.Columns[0].Visible = false;
            }
                

        }

        protected void grdUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView gr = (GridView)sender;
            GridViewRow gvr = gr.Rows[e.RowIndex];
            string username = gvr.Cells[3].Text;
            MembershipUser mu = Membership.GetUser(username);

            if (mu != null)
            {
                SkylineUser su = new SkylineUser(new Guid(mu.ProviderUserKey.ToString()), connectionString);
                TextBox txtEmail = (TextBox)gvr.Cells[4].FindControl("txtEmail");
                CheckBox cb = (CheckBox)gvr.Cells[5].FindControl("chkIsLockedOut");

                if (txtEmail != null && cb != null)
                {
                    string email = txtEmail.Text;
                    bool isLockedOut = cb.Checked;
                    su.UpdateAdminDataBase(email, isLockedOut);

                    gr.EditIndex = -1;
                    PopulateGridView();
                }
            }
            
        }

        protected void grdUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUsers.PageIndex = e.NewPageIndex;
            PopulateGridView();
        }
    }
}