using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProductInfo
{
    [Serializable]
    public class Cart
    {
        private List<CartItem> _items;

        public Cart()
        {

        }

        public List<CartItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public static Cart Instance
        {
            get
            {
                Cart c = null;
                if (HttpContext.Current.Session["ASPNETShoppingCart"] == null)
                {
                    c = new Cart();
                    c.Items = new List<CartItem>();
                    HttpContext.Current.Session.Add("ASPNETShoppingCart", c);
                }
                else
                {
                    c = (Cart)HttpContext.Current.Session["ASPNETShoppingCart"];
                }
                return c;
            }
        }

        public void AddItem(Product prod)
        {
            CartItem newItem = new CartItem(prod.ProductID, prod.ProductNum, prod.Name, prod.Price, prod.Thumbnail);

            if (Items.Contains(newItem))
            {
                foreach (CartItem ci in Items)
                {
                    if (ci.Equals(newItem))
                    {
                        ci.Quantity += 1;
                        return;
                    }
                }
            }
            else
            {
                newItem.Quantity = 1;
                Items.Add(newItem);
            }
        }

        public void RemoveItem(Product prod)
        {
            CartItem crtItem = Cart.Instance.Items.Find(x => x.ProductID == prod.ProductID);

            Items.Remove(crtItem);
        }

        public void SetItemQuantity(Product prod, int quantity)
        {
            if (quantity == 0)
            {
                RemoveItem(prod);
                return;
            }
            else
            {
                CartItem crtItem = Cart.Instance.Items.Find(x => x.ProductID == prod.ProductID);
                crtItem.Quantity = quantity;
            }
        }

        public decimal GetSubTotal()
        {
            decimal subTotal = 0;

            foreach (CartItem ci in Items) 
            {
                subTotal += ci.TotalPrice;
            }

            return subTotal;
        }

        public CartItem GetCartItem(Product prod)
        {
            CartItem crtItem = Cart.Instance.Items.Find(x => x.ProductID == prod.ProductID);
            return crtItem;
        }
    }
}
