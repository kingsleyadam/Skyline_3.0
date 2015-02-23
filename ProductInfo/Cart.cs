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

        public Cart Instance
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
            CartItem newItem = new CartItem(prod.ProductID, prod.ProductNum, prod.Name, prod.Price);

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
            CartItem ri = new CartItem(prod.ProductID, prod.ProductNum, prod.Name, prod.Price);

            foreach (CartItem ci in Items)
            {
                if (ci.Equals(ri))
                {
                    Items.Remove(ri);
                }
            }
        }

        public void SetItemQuantity(Product prod, int quantity)
        {
            if (quantity == 0)
            {
                RemoveItem(prod);
                return;
            }

            CartItem ui = new CartItem(prod.ProductID, prod.ProductNum, prod.Name, prod.Price);

            foreach (CartItem ci in Items)
            {
                if (ci.Equals(ui))
                {
                    ci.Quantity = quantity;
                }
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
            CartItem crtItem = null, fc = new CartItem(prod.ProductID, prod.ProductNum, prod.Name, prod.Price);

            foreach (CartItem ci in Items)
            {
                if (ci.Equals(fc))
                {
                    crtItem = ci;
                    break;
                }
            }

            return crtItem;
        }
    }
}
