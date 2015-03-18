using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary
{
    public class GlossaryText
    {
        private int _glossID;
        private int _pageID;
        private string _text;
        private int _dispOrder;
        private string _location;
        private string _dbConnection;

        public GlossaryText(string mConnectionString)
        {
            ConnectionString = mConnectionString;
        }

        public GlossaryText(int mGlossID, string mConnectionString)
        {
            GlossID = mGlossID;
            ConnectionString = mConnectionString;

            DataSet ds = GetDataSetByGlossID();

            if (ds.Tables["Glossary"].Rows.Count == 1)
            {
                PageID = (int)ds.Tables["Glossary"].Rows[0]["PageID"];
                Text = ds.Tables["Glossary"].Rows[0]["Text"].ToString();
                DisplayOrder = (int)ds.Tables["Glossary"].Rows[0]["DispOrder"];
                Location = ds.Tables["Glossary"].Rows[0]["Location"].ToString();
            }
        }

        public GlossaryText(int mGlossID, int mPageID, string mText, int mDispOrder, string mLocation, string mConnectionString)
        {
            GlossID = mGlossID;
            PageID = mPageID;
            Text = mText;
            DisplayOrder = mDispOrder;
            Location = mLocation;
            ConnectionString = mConnectionString;
        }


        public int GlossID
        {
            get { return _glossID; }
            set { _glossID = value; }
        }
        public int PageID
        {
            get { return _pageID; }
            set { _pageID = value; }
        }
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        public int DisplayOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }
        public string ConnectionString
        {
            get { return _dbConnection; }
            set { _dbConnection = value; }
        }

        private DataSet GetDataSetByGlossID()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admGetGlossaryByGlossID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramGlossID = new SqlParameter("@GlossID", SqlDbType.Int);
                paramGlossID.Direction = ParameterDirection.Input;

                paramGlossID.Value = GlossID;

                cmd.Parameters.Add(paramGlossID);

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

        public void UpdateDatabase()
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admUpdateGlossary", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramGlossID = new SqlParameter("@GlossID", SqlDbType.Int);
                paramGlossID.Direction = ParameterDirection.Input;
                SqlParameter paramPageID = new SqlParameter("@PageID", SqlDbType.Int);
                paramPageID.Direction = ParameterDirection.Input;
                SqlParameter paramText = new SqlParameter("@Text", SqlDbType.NVarChar, 4000);
                paramText.Direction = ParameterDirection.Input;
                SqlParameter paramDispOrder = new SqlParameter("@DispOrder", SqlDbType.Int);
                paramDispOrder.Direction = ParameterDirection.Input;
                SqlParameter paramLocation = new SqlParameter("@Location", SqlDbType.VarChar, 50);
                paramLocation.Direction = ParameterDirection.Input;

                paramGlossID.Value = GlossID;
                paramPageID.Value = PageID;
                paramText.Value = Text;
                paramDispOrder.Value = DisplayOrder;
                paramLocation.Value = Location;

                cmd.Parameters.Add(paramGlossID);
                cmd.Parameters.Add(paramPageID);
                cmd.Parameters.Add(paramText);
                cmd.Parameters.Add(paramDispOrder);
                cmd.Parameters.Add(paramLocation);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Add2DataBase()
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admAdd2Glossary", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramPageID = new SqlParameter("@PageID", SqlDbType.Int);
                paramPageID.Direction = ParameterDirection.Input;
                SqlParameter paramText = new SqlParameter("@Text", SqlDbType.NVarChar, 4000);
                paramText.Direction = ParameterDirection.Input;
                SqlParameter paramDispOrder = new SqlParameter("@DispOrder", SqlDbType.Int);
                paramDispOrder.Direction = ParameterDirection.Input;
                SqlParameter paramLocation = new SqlParameter("@Location", SqlDbType.VarChar, 50);
                paramLocation.Direction = ParameterDirection.Input;

                paramPageID.Value = PageID;
                paramText.Value = Text;
                paramDispOrder.Value = DisplayOrder;
                paramLocation.Value = Location;

                cmd.Parameters.Add(paramPageID);
                cmd.Parameters.Add(paramText);
                cmd.Parameters.Add(paramDispOrder);
                cmd.Parameters.Add(paramLocation);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteFromDatabase()
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admDeleteGlossary", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramGlossID = new SqlParameter("@GlossID", SqlDbType.Int);
                paramGlossID.Direction = ParameterDirection.Input;

                paramGlossID.Value = GlossID;

                cmd.Parameters.Add(paramGlossID);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
