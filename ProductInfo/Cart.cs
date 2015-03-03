using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UserInfo;

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

        public void SubmitCart(SkylineUserAddress sua, out int orderID, string ConnectionString)
        {
            orderID = 0;

            AddOrderInfo(sua, out orderID, ConnectionString);

            if (orderID > 0)
            {
                foreach (CartItem ci in Cart.Instance.Items)
                {
                    AddProduct2Order(orderID, ci.ProductID, ci.Quantity, ci.UnitPrice, ci.TotalPrice, ConnectionString);
                }
            }

            //Clear the cart
            Cart.Instance.Items.Clear();
        }

        private void AddOrderInfo(SkylineUserAddress sua, out int orderID, string ConnectionString)
        {
            orderID = 0;
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("ordAddOrder", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramCompany = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 500);
                paramCompany.Direction = ParameterDirection.Input;
                SqlParameter paramFName = new SqlParameter("@FName", SqlDbType.NVarChar, 300);
                paramFName.Direction = ParameterDirection.Input;
                SqlParameter paramLName = new SqlParameter("@LName", SqlDbType.NVarChar, 300);
                paramLName.Direction = ParameterDirection.Input;
                SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 500);
                paramEmail.Direction = ParameterDirection.Input;
                SqlParameter paramAddress1 = new SqlParameter("@Address1", SqlDbType.NVarChar, 500);
                paramAddress1.Direction = ParameterDirection.Input;
                SqlParameter paramAddress2 = new SqlParameter("@Address2", SqlDbType.NVarChar, 500);
                paramAddress2.Direction = ParameterDirection.Input;
                SqlParameter paramCityID = new SqlParameter("@CityID", SqlDbType.Int);
                paramCityID.Direction = ParameterDirection.Input;
                SqlParameter paramStateID = new SqlParameter("@StateID", SqlDbType.Int);
                paramStateID.Direction = ParameterDirection.Input;
                SqlParameter paramZipCode = new SqlParameter("@ZipCode", SqlDbType.Int);
                paramZipCode.Direction = ParameterDirection.Input;
                SqlParameter paramPhone = new SqlParameter("@Phone", SqlDbType.NVarChar, 100);
                paramPhone.Direction = ParameterDirection.Input;
                SqlParameter paramUsername = new SqlParameter("@Username", SqlDbType.NVarChar, 512);
                paramUsername.Direction = ParameterDirection.Input;
                SqlParameter paramOrderID = new SqlParameter("@OrderID", SqlDbType.Int);
                paramOrderID.Direction = ParameterDirection.Output;
                SqlParameter paramCity = new SqlParameter("@City", SqlDbType.NVarChar, 1000);
                paramCity.Direction = ParameterDirection.Input;
                SqlParameter paramState = new SqlParameter("@State", SqlDbType.NVarChar, 1000);
                paramState.Direction = ParameterDirection.Input;

                paramCompany.Value = sua.CompanyName;
                paramFName.Value = sua.FirstName;
                paramLName.Value = sua.LastName;
                paramEmail.Value = sua.Email;
                paramAddress1.Value = sua.Address1;
                if (sua.Address2.Length == 0)
                    paramAddress2.Value = DBNull.Value;
                else
                    paramAddress2.Value = sua.Address2;
                paramCityID.Value = 0;
                paramStateID.Value = 0;
                paramZipCode.Value = sua.ZipCode;
                paramPhone.Value = sua.PhoneNumber;
                paramUsername.Value = sua.User.UserName;
                paramCity.Value = sua.City;
                paramState.Value = sua.State;

                cmd.Parameters.Add(paramCompany);
                cmd.Parameters.Add(paramFName);
                cmd.Parameters.Add(paramLName);
                cmd.Parameters.Add(paramEmail);
                cmd.Parameters.Add(paramAddress1);
                cmd.Parameters.Add(paramAddress2);
                cmd.Parameters.Add(paramCityID);
                cmd.Parameters.Add(paramStateID);
                cmd.Parameters.Add(paramZipCode);
                cmd.Parameters.Add(paramPhone);
                cmd.Parameters.Add(paramUsername);
                cmd.Parameters.Add(paramOrderID);
                cmd.Parameters.Add(paramCity);
                cmd.Parameters.Add(paramState);

                con.Open();

                cmd.ExecuteNonQuery();

                orderID = (int)paramOrderID.Value;
            }
        }

        private void AddProduct2Order(int orderID, int productID, int quantity, decimal price, decimal totalPrice, string ConnectionString)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("ordAddOrderProduct", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramOrderID = new SqlParameter("@OrderID", SqlDbType.Int);
                paramOrderID.Direction = ParameterDirection.Input;
                SqlParameter paramProductID = new SqlParameter("@ProductID", SqlDbType.Int);
                paramProductID.Direction = ParameterDirection.Input;
                SqlParameter paramQuantity = new SqlParameter("@Quantity", SqlDbType.Int);
                paramQuantity.Direction = ParameterDirection.Input;
                SqlParameter paramPrice = new SqlParameter("@Price", SqlDbType.SmallMoney);
                paramPrice.Direction = ParameterDirection.Input;
                SqlParameter paramTotalPrice = new SqlParameter("@TotalPrice", SqlDbType.SmallMoney);
                paramTotalPrice.Direction = ParameterDirection.Input;

                paramOrderID.Value = orderID;
                paramProductID.Value = productID;
                paramQuantity.Value = quantity;
                paramPrice.Value = price;
                paramTotalPrice.Value = totalPrice;

                cmd.Parameters.Add(paramOrderID);
                cmd.Parameters.Add(paramProductID);
                cmd.Parameters.Add(paramQuantity);
                cmd.Parameters.Add(paramPrice);
                cmd.Parameters.Add(paramTotalPrice);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
