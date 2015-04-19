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
        private int _categoryID;
        private string _name;
        private bool _isActive;
        private string _abbrev;
        private string _connectionString;

        public Category(string dbConnection)
        {
            ConnectionString = dbConnection;
        }

        public Category(int categoryID, string dbConnection)
        {
            CategoryID = categoryID;
            ConnectionString = dbConnection;
        }

        public Category(string name, string abbreviation, string dbConnection)
        {
            Name = name;
            Abbreviation = abbreviation;
            ConnectionString = dbConnection;
        }

        public Category(int categoryID, string name, bool isActive, string dbConnection)
        {
            CategoryID = categoryID;
            Name = name;
            IsActive = isActive;
            ConnectionString = dbConnection;
        }

        public int CategoryID
        {
            get { return _categoryID; }
            set { _categoryID = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public string Abbreviation
        {
            get { return _abbrev; }
            set { _abbrev = value; }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public DataSet UpdateDataBase()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admCategoryUpdateCategory", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramCategoryID = new SqlParameter("@CategoryID", SqlDbType.Int);
                paramCategoryID.Direction = ParameterDirection.Input;
                SqlParameter paramName = new SqlParameter("@Name", SqlDbType.NVarChar, 200);
                paramName.Direction = ParameterDirection.Input;
                SqlParameter paramIsActive = new SqlParameter("@IsActive", SqlDbType.Bit);
                paramIsActive.Direction = ParameterDirection.Input;

                paramCategoryID.Value = CategoryID;
                paramName.Value = Name;
                paramIsActive.Value = IsActive; 

                cmd.Parameters.Add(paramCategoryID);
                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramIsActive);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

            return ds;
        }

        public DataSet AddToDataBase()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admCategoryAddCategory", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramName = new SqlParameter("@Name", SqlDbType.NVarChar, 200);
                paramName.Direction = ParameterDirection.Input;
                SqlParameter paramAbbrev = new SqlParameter("@Abbrev", SqlDbType.NVarChar, 200);
                paramAbbrev.Direction = ParameterDirection.Input;

                paramName.Value = Name;
                paramAbbrev.Value = Abbreviation;

                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramAbbrev);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

            return ds;
        }

        public DataSet DeleteFromDataBase()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admCategoryDeleteCategory", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramCategoryID = new SqlParameter("@CategoryID", SqlDbType.Int);
                paramCategoryID.Direction = ParameterDirection.Input;

                paramCategoryID.Value = CategoryID;

                cmd.Parameters.Add(paramCategoryID);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

            return ds;
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

        public static DataSet GetCategoryAdminDataSet(bool includeAll, string connectionString)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connectionString);

            using (SqlCommand cmd = new SqlCommand("admCategoryGetCategories", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramIncludeAll = new SqlParameter("@IncludeAll", SqlDbType.Bit);
                paramIncludeAll.Direction = ParameterDirection.Input;

                paramIncludeAll.Value = includeAll;

                cmd.Parameters.Add(paramIncludeAll);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("Categories");
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
