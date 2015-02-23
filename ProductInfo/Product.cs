﻿using System;
using System.Collections.Generic;
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
        private decimal _price;

        public Product(int m_productID, string m_productNum, string m_name, decimal m_price)
        {
            ProductID = m_productID;
            ProductNum = m_productNum;
            Name = m_name;
            Price = m_price;
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

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
    }
}
