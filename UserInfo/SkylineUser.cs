using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfo
{
    public class SkylineUser
    {
        private Guid _userID;
        private int _skylineID;
        private string _userName;
        private string _fName;
        private string _lName;
        private string _email;
        private DateTime _lastLoginDate;
        private bool _notifications;
        private string _companyName;
        private string _connectionString;

        public SkylineUser(Guid userID, string dbConnection)
        {
            UserID = userID;
            ConnectionString = dbConnection;
            DataSet userDS = GetDataSet();

            if (userDS.Tables["UserInfo"].Rows.Count > 0)
            {
                SkylineID = (int)userDS.Tables["UserInfo"].Rows[0]["SkylineID"];
                UserName = userDS.Tables["UserInfo"].Rows[0]["UserName"].ToString();
                FirstName = userDS.Tables["UserInfo"].Rows[0]["FName"].ToString();
                LastName = userDS.Tables["UserInfo"].Rows[0]["LName"].ToString();
                Email = userDS.Tables["UserInfo"].Rows[0]["Email"].ToString();
                LastLoginDate = (DateTime)userDS.Tables["UserInfo"].Rows[0]["LastLoginDate"];
                Notifications = (bool)userDS.Tables["UserInfo"].Rows[0]["EmailNotifications"];
                CompanyName = userDS.Tables["UserInfo"].Rows[0]["Company"].ToString();
            }            
        }

        public Guid UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public int SkylineID
        {
            get { return _skylineID; }
            set { _skylineID = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string FirstName
        {
            get { return _fName; }
            set { _fName = value; }
        }

        public string LastName
        {
            get { return _lName; }
            set { _lName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public DateTime LastLoginDate
        {
            get { return _lastLoginDate; }
            set { _lastLoginDate = value; }
        }

        public bool Notifications
        {
            get { return _notifications; }
            set { _notifications = value; }
        }

        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public void UpdateDatabase() 
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("usrUpdateUser", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramSkylineID = new SqlParameter("@SkylineID", SqlDbType.Int);
                paramSkylineID.Direction = ParameterDirection.Input;
                SqlParameter paramFName = new SqlParameter("@FName", SqlDbType.NVarChar, 600);
                paramFName.Direction = ParameterDirection.Input;
                SqlParameter paramLName = new SqlParameter("@LName", SqlDbType.NVarChar, 600);
                paramLName.Direction = ParameterDirection.Input;
                SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 600);
                paramEmail.Direction = ParameterDirection.Input;
                SqlParameter paramCompany = new SqlParameter("@Company", SqlDbType.NVarChar, 1000);
                paramCompany.Direction = ParameterDirection.Input;
                SqlParameter paramNotifications = new SqlParameter("@EmailNotifications", SqlDbType.Bit);
                paramNotifications.Direction = ParameterDirection.Input;

                paramSkylineID.Value = SkylineID;
                paramFName.Value = FirstName;
                paramLName.Value = LastName;
                paramEmail.Value = Email;
                paramCompany.Value = CompanyName;
                paramNotifications.Value = Notifications;

                cmd.Parameters.Add(paramSkylineID);
                cmd.Parameters.Add(paramFName);
                cmd.Parameters.Add(paramLName);
                cmd.Parameters.Add(paramEmail);
                cmd.Parameters.Add(paramCompany);
                cmd.Parameters.Add(paramNotifications);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private DataSet GetDataSet()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admGetUsersInfo", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserID = new SqlParameter("@UserID", SqlDbType.NVarChar, 1000);
                paramUserID.Direction = ParameterDirection.Input;

                paramUserID.Value = UserID.ToString();

                cmd.Parameters.Add(paramUserID);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("UserInfo");
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
