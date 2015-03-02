using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfo
{
    public class SkylineUserAddress
    {
        private SkylineUser _skylineUser;
        private string _companyName;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _address1;
        private string _address2;
        private string _city;
        private string _state;
        private string _zipCode;
        private string _phoneNumber;
        private string _connectionString;

        public SkylineUserAddress(SkylineUser m_SkylineUser)
        {
            User = m_SkylineUser;
            ConnectionString = m_SkylineUser.ConnectionString;

            DataSet ds = GetDataSet();

            if (ds.Tables["AddressInfo"].Rows.Count == 1)
            {
                CompanyName = ds.Tables["AddressInfo"].Rows[0]["Company"].ToString();
                FirstName = ds.Tables["AddressInfo"].Rows[0]["FirstName"].ToString();
                LastName = ds.Tables["AddressInfo"].Rows[0]["LastName"].ToString();
                Email = ds.Tables["AddressInfo"].Rows[0]["Email"].ToString();
                Address1 = ds.Tables["AddressInfo"].Rows[0]["Address1"].ToString();
                Address2 = ds.Tables["AddressInfo"].Rows[0]["Address2"].ToString();
                City = ds.Tables["AddressInfo"].Rows[0]["City"].ToString();
                State = ds.Tables["AddressInfo"].Rows[0]["State"].ToString();
                ZipCode = ds.Tables["AddressInfo"].Rows[0]["ZipCode"].ToString();
                PhoneNumber = ds.Tables["AddressInfo"].Rows[0]["Phone"].ToString();
            }
        }

        public SkylineUser User
        {
            get { return _skylineUser; }
            set { _skylineUser = value; }
        }

        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }

        public string Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        public string ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
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

            using (SqlCommand cmd = new SqlCommand("OrdGetLastShippingInfo", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramSkylineID = new SqlParameter("@SkylineID", SqlDbType.Int);
                paramSkylineID.Direction = ParameterDirection.Input;

                paramSkylineID.Value = User.SkylineID;

                cmd.Parameters.Add(paramSkylineID);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("AddressInfo");
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

        public SkylineUser(string emailAddress, string dbConnection)
        {
            Email = emailAddress;
            ConnectionString = dbConnection;
            DataSet userDS = GetDataSetByEmail();

            if (userDS.Tables["UserInfo"].Rows.Count == 1)
            {
                UserID = new Guid(userDS.Tables["UserInfo"].Rows[0]["UserID"].ToString());
                SkylineID = (int)userDS.Tables["UserInfo"].Rows[0]["SkylineID"];
                UserName = userDS.Tables["UserInfo"].Rows[0]["UserName"].ToString();
                FirstName = userDS.Tables["UserInfo"].Rows[0]["FName"].ToString();
                LastName = userDS.Tables["UserInfo"].Rows[0]["LName"].ToString();
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

        public DataSet GetWebSiteOrders()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("usrGetUserOrders", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserID = new SqlParameter("@UserID", SqlDbType.NVarChar, 1000);
                paramUserID.Direction = ParameterDirection.Input;

                paramUserID.Value = UserID.ToString();

                cmd.Parameters.Add(paramUserID);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("WebOrders");
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }
                    ds.Tables.Add(dt);
                }
            }

            return ds;
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

        private DataSet GetDataSetByEmail()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admGetUsersInfoByEmail", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 512);
                paramEmail.Direction = ParameterDirection.Input;

                paramEmail.Value = Email;

                cmd.Parameters.Add(paramEmail);

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
