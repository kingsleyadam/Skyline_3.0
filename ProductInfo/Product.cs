using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductInfo
{
    [Serializable]
    public class Product
    {
        private int _id;
        private string _productNum;
        private string _name;
        private string _description;
        private decimal _price;
        private string _fullImage;
        private string _thumbNail;
        private string _originalImage;
        private bool _bestSeller;
        private bool _soldOut;
        private int _quantity;
        private string _connectionString;

        public Product()
        {

        }

        public Product(int m_productID, string connectionString)
        {
            ProductID = m_productID;
            ConnectionString = connectionString;
            DataSet ds = GetDataSet();

            if (ds.Tables["Product"].Rows.Count > 0)
            {
                ProductNum = ds.Tables["Product"].Rows[0]["ProductNum"].ToString();
                Name = ds.Tables["Product"].Rows[0]["Name"].ToString();
                Description = ds.Tables["Product"].Rows[0]["Description"].ToString();
                Price = Convert.ToDecimal(ds.Tables["Product"].Rows[0]["Price"].ToString());
                FullImage = ds.Tables["Product"].Rows[0]["imgURL"].ToString();
                Thumbnail = ds.Tables["Product"].Rows[0]["imgThumb"].ToString();
                OriginalImage = ds.Tables["Product"].Rows[0]["imgOrig"].ToString();
                BestSeller = (bool)ds.Tables["Product"].Rows[0]["IsBestSeller"];
                SoldOut = (bool)ds.Tables["Product"].Rows[0]["IsSoldOut"];
                Quantity = Convert.ToInt32(ds.Tables["Product"].Rows[0]["Quantity"].ToString());
            }
        }

        public Product(int m_productID, string m_productNum, string m_name, decimal m_price)
        {
            ProductID = m_productID;
            ProductNum = m_productNum;
            Name = m_name;
            Price = m_price;
            FullImage = "";
            OriginalImage = "";
            Thumbnail = "";
        }

        public Product(int m_productID, string m_productNum, string m_name, decimal m_price, string m_thumbNail)
        {
            ProductID = m_productID;
            ProductNum = m_productNum;
            Name = m_name;
            Price = m_price;
            FullImage = "";
            OriginalImage = "";
            Thumbnail = m_thumbNail;
        }

        public Product(int m_productID, string m_productNum, string m_name, decimal m_price, string m_thumbNail, string m_fullImage, string m_originalImage, string m_description, bool m_bestSeller, bool m_soldOut)
        {
            ProductID = m_productID;
            ProductNum = m_productNum;
            Name = m_name;
            Description = m_description;
            Price = m_price;
            FullImage = m_fullImage;
            OriginalImage = m_originalImage;
            Thumbnail = m_thumbNail;
            BestSeller = m_bestSeller;
            SoldOut = m_soldOut;
        }
        
        public int ProductID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string ProductNum
        {
            get { return _productNum; }
            set { _productNum = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string Thumbnail
        {
            get { return _thumbNail; }
            set { _thumbNail = value; }
        }

        public string FullImage
        {
            get { return _fullImage; }
            set { _fullImage = value; }
        }

        public string OriginalImage
        {
            get { return _originalImage; }
            set { _originalImage = value; }
        }

        public bool BestSeller
        {
            get { return _bestSeller; }
            set { _bestSeller = value; }
        }

        public bool SoldOut
        {
            get { return _soldOut; }
            set { _soldOut = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
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

            using (SqlCommand cmd = new SqlCommand("admGetProductInfoDataset", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserID = new SqlParameter("@ProductID", SqlDbType.Int);
                paramUserID.Direction = ParameterDirection.Input;

                paramUserID.Value = ProductID;

                cmd.Parameters.Add(paramUserID);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("Product");
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }

        public DataSet GetImagesDataSet(bool showDefault, string dbConnection)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(dbConnection);

            using (SqlCommand cmd = new SqlCommand("prdGetProductsImage", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramProductID = new SqlParameter("@ProductID", SqlDbType.Int);
                paramProductID.Direction = ParameterDirection.Input;
                SqlParameter paramShowDefault = new SqlParameter("@ShowDefault", SqlDbType.Bit);
                paramShowDefault.Direction = ParameterDirection.Input;

                paramProductID.Value = ProductID;
                paramShowDefault.Value = showDefault;

                cmd.Parameters.Add(paramProductID);
                cmd.Parameters.Add(paramShowDefault);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("Images");
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }

        public DataSet GetAdminImagesDataSet()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admGetProductImages", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramProductID = new SqlParameter("@ProductID", SqlDbType.Int);
                paramProductID.Direction = ParameterDirection.Input;

                paramProductID.Value = ProductID;

                cmd.Parameters.Add(paramProductID);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("Images");
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }

        public DataSet GetCategoriesDataSet()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admGetProductCategories", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramProductID = new SqlParameter("@ProductID", SqlDbType.Int);
                paramProductID.Direction = ParameterDirection.Input;

                paramProductID.Value = ProductID;

                cmd.Parameters.Add(paramProductID);

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

        public void AddToCatagory(int categoryID)
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admAddCategories", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramProductID = new SqlParameter("@ProductID", SqlDbType.Int);
                paramProductID.Direction = ParameterDirection.Input;
                SqlParameter paramCategoryID = new SqlParameter("@CategoryID", SqlDbType.Int);
                paramCategoryID.Direction = ParameterDirection.Input;

                paramProductID.Value = ProductID;
                paramCategoryID.Value = categoryID;

                cmd.Parameters.Add(paramProductID);
                cmd.Parameters.Add(paramCategoryID);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void RemoveFromCatagory(int categoryID)
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admRemoveCategories", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramProductID = new SqlParameter("@ProductID", SqlDbType.Int);
                paramProductID.Direction = ParameterDirection.Input;
                SqlParameter paramCategoryID = new SqlParameter("@CategoryID", SqlDbType.Int);
                paramCategoryID.Direction = ParameterDirection.Input;

                paramProductID.Value = ProductID;
                paramCategoryID.Value = categoryID;

                cmd.Parameters.Add(paramProductID);
                cmd.Parameters.Add(paramCategoryID);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public string AddImage(bool isDefault, string fileExt)
        {
            string imgName = "";
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admAddProductImage", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramProductID = new SqlParameter("@ProductID", SqlDbType.Int);
                paramProductID.Direction = ParameterDirection.Input;
                SqlParameter paramDefault = new SqlParameter("@IsDefault", SqlDbType.Bit);
                paramDefault.Direction = ParameterDirection.Input;
                SqlParameter paramFileExt = new SqlParameter("@fileExt", SqlDbType.VarChar, 10);
                paramFileExt.Direction = ParameterDirection.Input;
                SqlParameter paramImgName = new SqlParameter("@imgName", SqlDbType.VarChar, 200);
                paramImgName.Direction = ParameterDirection.Output;

                paramProductID.Value = ProductID;
                paramDefault.Value = isDefault;
                paramFileExt.Value = fileExt;

                cmd.Parameters.Add(paramProductID);
                cmd.Parameters.Add(paramDefault);
                cmd.Parameters.Add(paramFileExt);
                cmd.Parameters.Add(paramImgName);

                con.Open();

                cmd.ExecuteNonQuery();

                imgName = paramImgName.Value.ToString();

                con.Close();

                return imgName;
            }
        }

        public void DeleteImage(int imageID)
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admDeleteImage", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramImageID = new SqlParameter("@ImageID", SqlDbType.Int);
                paramImageID.Direction = ParameterDirection.Input;

                paramImageID.Value = imageID;

                cmd.Parameters.Add(paramImageID);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void SetDefaultImage(int imageID)
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admUpdateProductImageDefault", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramImageID = new SqlParameter("@ImageID", SqlDbType.Int);
                paramImageID.Direction = ParameterDirection.Input;

                paramImageID.Value = imageID;

                cmd.Parameters.Add(paramImageID);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void UpdateDatabase()
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admUpdateProduct", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramProductID = new SqlParameter("@ProductID", SqlDbType.Int);
                paramProductID.Direction = ParameterDirection.Input;
                SqlParameter paramName = new SqlParameter("@Name", SqlDbType.NVarChar, 500);
                paramName.Direction = ParameterDirection.Input;
                SqlParameter paramProductNum = new SqlParameter("@ProductNum", SqlDbType.NVarChar, 50);
                paramProductNum.Direction = ParameterDirection.Input;
                SqlParameter paramDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000);
                paramDescription.Direction = ParameterDirection.Input;
                SqlParameter paramPrice = new SqlParameter("@Price", SqlDbType.SmallMoney);
                paramPrice.Direction = ParameterDirection.Input;
                SqlParameter paramQuantity = new SqlParameter("@Quantity", SqlDbType.Int);
                paramQuantity.Direction = ParameterDirection.Input;
                SqlParameter paramimgURL = new SqlParameter("@imgURL", SqlDbType.NVarChar, 100);
                paramimgURL.Direction = ParameterDirection.Input;
                SqlParameter paramIsSoldOut = new SqlParameter("@IsSoldOut", SqlDbType.Bit);
                paramIsSoldOut.Direction = ParameterDirection.Input;
                SqlParameter paramIsBestSeller = new SqlParameter("@IsBestSeller", SqlDbType.Bit);
                paramIsBestSeller.Direction = ParameterDirection.Input;
                
                paramProductID.Value = ProductID;
                paramName.Value = Name;
                paramProductNum.Value = ProductNum;
                paramDescription.Value = Description;
                paramPrice.Value = Price;
                paramQuantity.Value = Quantity;
                paramimgURL.Value = "";
                paramIsSoldOut.Value = SoldOut;
                paramIsBestSeller.Value = BestSeller;

                cmd.Parameters.Add(paramProductID);
                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramProductNum);
                cmd.Parameters.Add(paramDescription);
                cmd.Parameters.Add(paramPrice);
                cmd.Parameters.Add(paramQuantity);
                cmd.Parameters.Add(paramimgURL);
                cmd.Parameters.Add(paramIsSoldOut);
                cmd.Parameters.Add(paramIsBestSeller);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void AddNewProduct()
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admAddProduct", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramName = new SqlParameter("@Name", SqlDbType.NVarChar, 500);
                paramName.Direction = ParameterDirection.Input;
                SqlParameter paramProductNum = new SqlParameter("@ProductNum", SqlDbType.NVarChar, 50);
                paramProductNum.Direction = ParameterDirection.Input;
                SqlParameter paramDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000);
                paramDescription.Direction = ParameterDirection.Input;
                SqlParameter paramPrice = new SqlParameter("@Price", SqlDbType.SmallMoney);
                paramPrice.Direction = ParameterDirection.Input;
                SqlParameter paramQuantity = new SqlParameter("@Quantity", SqlDbType.Int);
                paramQuantity.Direction = ParameterDirection.Input;
                SqlParameter paramimgURL = new SqlParameter("@imgURL", SqlDbType.NVarChar, 100);
                paramimgURL.Direction = ParameterDirection.Input;
                SqlParameter paramIsSoldOut = new SqlParameter("@IsSoldOut", SqlDbType.Bit);
                paramIsSoldOut.Direction = ParameterDirection.Input;
                SqlParameter paramIsBestSeller = new SqlParameter("@IsBestSeller", SqlDbType.Bit);
                paramIsBestSeller.Direction = ParameterDirection.Input;
                SqlParameter paramProductID = new SqlParameter("@ProductID", SqlDbType.Int);
                paramProductID.Direction = ParameterDirection.Output;

                paramName.Value = Name;
                paramProductNum.Value = ProductNum;
                paramDescription.Value = Description;
                paramPrice.Value = Price;
                paramQuantity.Value = Quantity;
                paramimgURL.Value = "";
                paramIsSoldOut.Value = SoldOut;
                paramIsBestSeller.Value = BestSeller;

                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramProductNum);
                cmd.Parameters.Add(paramDescription);
                cmd.Parameters.Add(paramPrice);
                cmd.Parameters.Add(paramQuantity);
                cmd.Parameters.Add(paramimgURL);
                cmd.Parameters.Add(paramIsSoldOut);
                cmd.Parameters.Add(paramIsBestSeller);
                cmd.Parameters.Add(paramProductID);

                con.Open();

                cmd.ExecuteNonQuery();

                ProductID = Convert.ToInt32(paramProductID.Value);

                con.Close();
            }
        }

        public void DeleteProduct()
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admDeleteProduct", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramProductID = new SqlParameter("@ProductID", SqlDbType.Int);
                paramProductID.Direction = ParameterDirection.Input;

                paramProductID.Value = ProductID;

                cmd.Parameters.Add(paramProductID);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }
    }
}
