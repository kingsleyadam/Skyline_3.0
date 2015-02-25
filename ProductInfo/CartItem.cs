using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductInfo
{
    [Serializable]
    public class CartItem : IEquatable<CartItem>
    {
        private int _quantity;
        private int _productID;
        private string _productNum;
        private string _name;
        private decimal _price;
        private Product _prod;
        private string _thumbNail;

        public CartItem(Product m_prod)
        {
            ProductID = m_prod.ProductID;
            ProductNum = m_prod.ProductNum;
            Name = m_prod.Name;
            UnitPrice = m_prod.Price;
            Thumbnail = m_prod.Thumbnail;
        }

        public CartItem(int m_productID, string m_productNum, string m_name, decimal m_price)
        {
            ProductID = m_productID;
            ProductNum = m_productNum;
            Name = m_name;
            UnitPrice = m_price;
        }

        public CartItem(int m_productID, string m_productNum, string m_name, decimal m_price, string m_thumbNail)
        {
            ProductID = m_productID;
            ProductNum = m_productNum;
            Name = m_name;
            UnitPrice = m_price;
            Thumbnail = m_thumbNail;
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public int ProductID
        {
            get { return _productID; }
            set { _productID = value; }
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

        public decimal UnitPrice
        {
            get { return _price; }
            set { _price = value; }
        }

        public Product prod
        {
            get
            {
                if (_prod == null)
                {
                    _prod = new Product(ProductID, ProductNum, Name, UnitPrice, Thumbnail);
                }
                return _prod;
            }
        }

        public string Thumbnail
        {
            get { return _thumbNail; }
            set { _thumbNail = value; }
        }

        public decimal TotalPrice
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }

        public bool Equals(CartItem obj)
        {
            return obj.ProductID == this.ProductID;
        }
    }
}
