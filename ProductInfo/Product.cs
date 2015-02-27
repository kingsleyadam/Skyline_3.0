﻿using System;
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

        public Product()
        {

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
    }
}
