using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Images;

namespace Skyline_3._0.admin
{
    public partial class products_image : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private DataSet GetProductImageDataSet()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connectionString);

            using (SqlCommand cmd = new SqlCommand("admGetProductImageInfo", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("ProductImages");
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }

        protected void btnDoTheWork_Click(object sender, EventArgs e)
        {
            DataSet ds = GetProductImageDataSet();

            Page.Server.MapPath("");

            if (ds.Tables["ProductImages"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["ProductImages"].Rows)
                {
                    string imgName = dr["ImgName"].ToString();
                    string origPath = dr["OrigPath"].ToString();
                    ImageProcessing ip = new ImageProcessing(imgName, origPath);
                    ip.ProcessImage();
                }
            }
        }
    }
}