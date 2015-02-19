using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Glossary
{
    public class GlossaryDataSet
    {
        private int _page;
        private string _location;
        private string _connectionString;

        public GlossaryDataSet(int pageNum, string pageLocation, string connectionStr)
        {
            Page = pageNum;
            Location = pageLocation;
            ConnectionString = connectionStr;
        }

        public int Page
        {
            get { return _page; }
            set { _page = value; }
        }
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public DataSet GetDataSet()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admGetGlossary", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramPageID = new SqlParameter("@PageID", SqlDbType.Int);
                paramPageID.Direction = ParameterDirection.Input;
                SqlParameter paramLocation = new SqlParameter("@Location", SqlDbType.VarChar, 10);
                paramLocation.Direction = ParameterDirection.Input;

                paramPageID.Value = Page;
                paramLocation.Value = Location;

                cmd.Parameters.Add(paramPageID);
                cmd.Parameters.Add(paramLocation);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("Glossary");
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }
    }
}
