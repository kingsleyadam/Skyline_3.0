using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfo
{
    public class UserOrder
    {
        private int _orderID;
        private string _orderNum;
        private string _company;
        private string _fName;
        private string _lName;
        private string _email;
        private string _address1;
        private string _address2;
        private string _city;
        private string _state;
        private string _phone;
        private string _zipCode;
        private DateTime _orderDate;
        private string _orderStatus;
        private int _skylineID;
        private decimal _totalPrice;
        private string _connectionString;

        public UserOrder(int m_orderID, string dbConnection)
        {
            OrderID = m_orderID;
            ConnectionString = dbConnection;
            DataSet orderDS = GetDataSet();

            if (orderDS.Tables["Order"].Rows.Count > 0)
            {
                OrderNum = orderDS.Tables["Order"].Rows[0]["OrderNum"].ToString();
                CompanyName = orderDS.Tables["Order"].Rows[0]["Company"].ToString();
                FirstName = orderDS.Tables["Order"].Rows[0]["FirstName"].ToString();
                LastName = orderDS.Tables["Order"].Rows[0]["LastName"].ToString();
                Email = orderDS.Tables["Order"].Rows[0]["Email"].ToString();
                Address1 = orderDS.Tables["Order"].Rows[0]["Address1"].ToString();
                Address2 = orderDS.Tables["Order"].Rows[0]["Address2"].ToString();
                City = orderDS.Tables["Order"].Rows[0]["City"].ToString();
                State = orderDS.Tables["Order"].Rows[0]["State"].ToString();
                Phone = orderDS.Tables["Order"].Rows[0]["Phone"].ToString();
                ZipCode = orderDS.Tables["Order"].Rows[0]["ZipCode"].ToString();
                OrderDate = (DateTime)orderDS.Tables["Order"].Rows[0]["Date"];
                Status = orderDS.Tables["Order"].Rows[0]["OrderStatus"].ToString();
                SkylineID = (int)orderDS.Tables["Order"].Rows[0]["SkylineID"];
                TotalPrice = (decimal)orderDS.Tables["Order"].Rows[0]["SumOfTotal"];
            }   
        }

        public int OrderID
        {
            get { return _orderID; }
            set { _orderID = value; }
        }

        public string OrderNum
        {
            get { return _orderNum; }
            set { _orderNum = value; }
        }

        public string CompanyName
        {
            get { return _company; }
            set { _company = value; }
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

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public string ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = value; }
        }

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        public string Status
        {
            get { return _orderStatus; }
            set { _orderStatus = value; }
        }

        public int SkylineID
        {
            get { return _skylineID; }
            set { _skylineID = value; }
        }

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
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

            using (SqlCommand cmd = new SqlCommand("admGetOrderInfo", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramOrderID = new SqlParameter("@OrderID", SqlDbType.Int);
                paramOrderID.Direction = ParameterDirection.Input;

                paramOrderID.Value = OrderID;

                cmd.Parameters.Add(paramOrderID);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("Order");
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }

        public DataSet GetItemDataSet()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admGetOrderProducts", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramOrderID = new SqlParameter("@OrderID", SqlDbType.Int);
                paramOrderID.Direction = ParameterDirection.Input;

                paramOrderID.Value = OrderID;

                cmd.Parameters.Add(paramOrderID);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("OrderItems");
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
