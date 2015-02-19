using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glossary;
using System.Configuration;

namespace Skyline_3._0
{
    public partial class about_us : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
                GlossaryDataSet glossaryDS = new GlossaryDataSet(3, "0", connectionString);
                dlAboutUs.DataSource = glossaryDS.GetDataSet();
                dlAboutUs.DataBind();
            }
        }
    }
}