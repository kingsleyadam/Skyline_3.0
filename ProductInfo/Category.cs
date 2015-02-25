using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductInfo
{
    public class Category
    {
        private string _connectionString;

        public Category(string dbConnection)
        {
            ConnectionString = dbConnection;
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public DataSet GetCategoryDataSet(bool includeAll, out int newProducts)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);
            newProducts = 0;

            using (SqlCommand cmd = new SqlCommand("prdGetCategoriesWNewProducts", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramIncludeAll = new SqlParameter("@IncludeAll", SqlDbType.Bit);
                paramIncludeAll.Direction = ParameterDirection.Input;
                SqlParameter paramNewProducts = new SqlParameter("@NewProducts", SqlDbType.Int);
                paramNewProducts.Direction = ParameterDirection.ReturnValue;

                paramIncludeAll.Value = includeAll;

                cmd.Parameters.Add(paramIncludeAll);
                cmd.Parameters.Add(paramNewProducts);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("Categories");
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                        newProducts = Convert.ToInt32(paramNewProducts.Value);
                    }
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }
    }
}
