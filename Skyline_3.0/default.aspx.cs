using Glossary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Skyline_3._0
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
                GlossaryDataSet glossaryDS = new GlossaryDataSet(1, "L", connectionString);
                GlossaryDataSet glossaryDSRight = new GlossaryDataSet(1, "R", connectionString);


                dlHomeLeft.DataSource = glossaryDS.GetDataSet();
                dlHomeLeft.DataBind();

                dlHomeRight.DataSource = glossaryDSRight.GetDataSet();
                dlHomeRight.DataBind();
            }
        }
    }
}