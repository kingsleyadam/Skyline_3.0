using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    public class ChangeLog
    {
        private int _iterationID;
        private string _version;
        private string _framework;
        private string _language;
        private DateTime _dateApplied;
        private bool _isCurrent;
        private string _repo;
        private string _connectionString;

        public ChangeLog(int m_interatinID, string m_connectionString)
        {
            IterationID = m_interatinID;
            ConnectionString = m_connectionString;

            DataSet userDS = GetDataSet();

            if (userDS.Tables["InterationDetails"].Rows.Count > 0)
            {
                Version = userDS.Tables["InterationDetails"].Rows[0]["Version"].ToString();
                Framework = userDS.Tables["InterationDetails"].Rows[0]["Framework"].ToString();
                Language = userDS.Tables["InterationDetails"].Rows[0]["Language"].ToString();
                DateApplied = (DateTime)userDS.Tables["InterationDetails"].Rows[0]["DateApplied"];
                IsCurrent = (bool)userDS.Tables["InterationDetails"].Rows[0]["IsCurrent"];
                Repository = userDS.Tables["InterationDetails"].Rows[0]["Repository"].ToString();
            }      
        }


        public int IterationID
        {
            get { return _iterationID; }
            set { _iterationID = value; }
        }

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public string Framework
        {
            get { return _framework; }
            set { _framework = value; }
        }

        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        public DateTime DateApplied
        {
            get { return _dateApplied; }
            set { _dateApplied = value; }
        }

        public bool IsCurrent
        {
            get { return _isCurrent; }
            set { _isCurrent = value; }
        }

        public string Repository
        {
            get { return _repo; }
            set { _repo = value; }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        private DataSet GetDataSet()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admGetVersionDetails", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserID = new SqlParameter("@IterationID", SqlDbType.Int);
                paramUserID.Direction = ParameterDirection.Input;

                paramUserID.Value = IterationID;

                cmd.Parameters.Add(paramUserID);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("InterationDetails");
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }

        public static DataSet GetAllVersions(string ConnectionString)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admGetVersion", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("Interations");
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }

        public static DataSet GetVersionChangeLogDataSet(int iterationID, string ConnectionString)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admGetVersionChangeLog", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserID = new SqlParameter("@IterationID", SqlDbType.Int);
                paramUserID.Direction = ParameterDirection.Input;

                paramUserID.Value = iterationID;

                cmd.Parameters.Add(paramUserID);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("Interations");
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
