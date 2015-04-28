using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    public class ChangeLogEntry
    {
        private int _changeLogID;
        private int _changeTypeID;
        private string _description;
        private ChangeLog _changeLog;

        public ChangeLogEntry(ChangeLog m_ChangeLog)
        {
            ChLog = m_ChangeLog;
        }

        public ChangeLogEntry(ChangeLog m_ChangeLog, int m_ChangeLogID)
        {
            ChangeLogID = m_ChangeLogID;
            ChLog = m_ChangeLog;
            DataSet ds = GetDataSet();

            if (ds.Tables["ChangeLogID"].Rows.Count == 1)
            {
                ChangeTypeID = (int)ds.Tables["ChangeLogID"].Rows[0]["ChangeTypeID"];
                Description = ds.Tables["ChangeLogID"].Rows[0]["Description"].ToString();
            }  
        }

        public int ChangeLogID
        {
            get { return _changeLogID; }
            set { _changeLogID = value; }
        }

        public int ChangeTypeID
        {
            get { return _changeTypeID; }
            set { _changeTypeID = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public ChangeLog ChLog
        {
            get { return _changeLog; }
            set { _changeLog = value; }
        }

        private DataSet GetDataSet()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ChLog.ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admGetChangeLogRecords", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramChangeLogID = new SqlParameter("@ChangeLogID", SqlDbType.Int);
                paramChangeLogID.Direction = ParameterDirection.Input;

                paramChangeLogID.Value = ChangeLogID;

                cmd.Parameters.Add(paramChangeLogID);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("ChangeLogID");
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }

        public void UpdateDataBase()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ChLog.ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admUpdateChangeLogInfo", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramChangeLogID = new SqlParameter("@ChangeLogID", SqlDbType.Int);
                paramChangeLogID.Direction = ParameterDirection.Input;
                SqlParameter paramChangeTypeID = new SqlParameter("@ChangeTypeID", SqlDbType.Int);
                paramChangeTypeID.Direction = ParameterDirection.Input;
                SqlParameter paramDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 4000);
                paramDescription.Direction = ParameterDirection.Input;

                paramChangeLogID.Value = ChangeLogID;
                paramChangeTypeID.Value = ChangeTypeID;
                paramDescription.Value = Description;

                cmd.Parameters.Add(paramChangeLogID);
                cmd.Parameters.Add(paramChangeTypeID);
                cmd.Parameters.Add(paramDescription);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void AddToDataBase()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ChLog.ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admAdd2ChangeLog", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramIterationID = new SqlParameter("@IterationID", SqlDbType.Int);
                paramIterationID.Direction = ParameterDirection.Input;
                SqlParameter paramChangeTypeID = new SqlParameter("@ChangeTypeID", SqlDbType.Int);
                paramChangeTypeID.Direction = ParameterDirection.Input;
                SqlParameter paramDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 4000);
                paramDescription.Direction = ParameterDirection.Input;

                paramIterationID.Value = ChLog.IterationID;
                paramChangeTypeID.Value = ChangeTypeID;
                paramDescription.Value = Description;

                cmd.Parameters.Add(paramIterationID);
                cmd.Parameters.Add(paramChangeTypeID);
                cmd.Parameters.Add(paramDescription);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveFromDataBase()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ChLog.ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admRemoveFromChangeLog", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramChangeLogID = new SqlParameter("@ChangeLogID", SqlDbType.Int);
                paramChangeLogID.Direction = ParameterDirection.Input;

                paramChangeLogID.Value = ChangeLogID;

                cmd.Parameters.Add(paramChangeLogID);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }

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

        public ChangeLog(string m_connectionString)
        {
            ConnectionString = m_connectionString;
        }

        public ChangeLog(int m_interatinID, string m_connectionString)
        {
            IterationID = m_interatinID;
            ConnectionString = m_connectionString;

            DataSet userDS = GetDataSet();

            if (userDS.Tables["InterationDetails"].Rows.Count > 0)
            {
                DateTime dateApply;

                Version = userDS.Tables["InterationDetails"].Rows[0]["Version"].ToString();
                Framework = userDS.Tables["InterationDetails"].Rows[0]["Framework"].ToString();
                Language = userDS.Tables["InterationDetails"].Rows[0]["Language"].ToString();

                if (DateTime.TryParse(userDS.Tables["InterationDetails"].Rows[0]["DateApplied"].ToString(), out dateApply))
                    DateApplied = dateApply;

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

        public static DataSet GetAllLogTypeDataSet(string ConnectionString)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admGetChangeLogType", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("LogTypes");
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

        public void UpdateDataBase()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admUpdateSiteInfo", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramIterationID = new SqlParameter("@IterationID", SqlDbType.Int);
                paramIterationID.Direction = ParameterDirection.Input;
                SqlParameter paramVersion = new SqlParameter("@Version", SqlDbType.VarChar, 10);
                paramVersion.Direction = ParameterDirection.Input;
                SqlParameter paramFramework = new SqlParameter("@Framework", SqlDbType.NVarChar, 100);
                paramFramework.Direction = ParameterDirection.Input;
                SqlParameter paramLanguage = new SqlParameter("@Language", SqlDbType.NVarChar, 100);
                paramLanguage.Direction = ParameterDirection.Input;
                SqlParameter paramRepository = new SqlParameter("@Repository", SqlDbType.NVarChar, 100);
                paramRepository.Direction = ParameterDirection.Input;

                paramIterationID.Value = IterationID;
                paramVersion.Value = Version;
                paramFramework.Value = Framework;
                paramLanguage.Value = Language;
                if (Repository != "None" || Repository.Length > 0)
                    paramRepository.Value = Repository;
                else
                    paramRepository.Value = DBNull.Value;

                cmd.Parameters.Add(paramIterationID);
                cmd.Parameters.Add(paramVersion);
                cmd.Parameters.Add(paramFramework);
                cmd.Parameters.Add(paramLanguage);
                cmd.Parameters.Add(paramRepository);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }


        public void UpdateCurrent(bool updateDate)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admUpdateSiteInfoMakeCurrent", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramIterationID = new SqlParameter("@IterationID", SqlDbType.Int);
                paramIterationID.Direction = ParameterDirection.Input;
                SqlParameter paramUpdateDate = new SqlParameter("@UpdateDate", SqlDbType.Bit);
                paramUpdateDate.Direction = ParameterDirection.Input;

                paramIterationID.Value = IterationID;
                paramUpdateDate.Value = updateDate;

                cmd.Parameters.Add(paramIterationID);
                cmd.Parameters.Add(paramUpdateDate);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void AddToDataBase()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admAdd2SiteInfo", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramVersion = new SqlParameter("@Version", SqlDbType.VarChar, 10);
                paramVersion.Direction = ParameterDirection.Input;
                SqlParameter paramFramework = new SqlParameter("@Framework", SqlDbType.NVarChar, 100);
                paramFramework.Direction = ParameterDirection.Input;
                SqlParameter paramLanguage = new SqlParameter("@Language", SqlDbType.NVarChar, 100);
                paramLanguage.Direction = ParameterDirection.Input;
                SqlParameter paramRepository = new SqlParameter("@Repository", SqlDbType.NVarChar, 100);
                paramRepository.Direction = ParameterDirection.Input;

                paramVersion.Value = Version;
                paramFramework.Value = Framework;
                paramLanguage.Value = Language;
                if (Repository != "None" || Repository.Length > 0)
                    paramRepository.Value = Repository;
                else
                    paramRepository.Value = DBNull.Value;

                cmd.Parameters.Add(paramVersion);
                cmd.Parameters.Add(paramFramework);
                cmd.Parameters.Add(paramLanguage);
                cmd.Parameters.Add(paramRepository);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
