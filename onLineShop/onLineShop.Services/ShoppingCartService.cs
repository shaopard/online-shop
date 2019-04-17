using onLineShop.Contracts.Repositories;
using onLineShop.DAL.Repositories;
using onLineShop.Model;
using onLineShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using onLineShop.DAL.Data;
using System.Transactions;

namespace onLineShop.Services
{
    public class ShoppingCartService
    {
        IRepositoryBase<ShoppingCart> carts;

        public const string CART_SESSION_NAME = "onLineShopping Cart";

        public ShoppingCartService(IRepositoryBase<ShoppingCart> carts)
        {
            //if (carts == null)
            //{
            //    carts = 
            //}
            this.carts = carts;
        }

        private ShoppingCart CreateNewShoppingCart(HttpContextBase httpContext)
        {
            //creeaza session
            var cookie = new HttpCookie(CART_SESSION_NAME);

            //creaza noul shopping cart si seteaza momentul crearii
            var shoppingCart = new ShoppingCart();
            shoppingCart.Date = DateTime.Now;
            shoppingCart.ShoppingCartId = new int();
            shoppingCart.ShoppingCartItems = new List<CartItem>();

            carts.Insert(shoppingCart);
            carts.Commit();

            //pune in cookie id-ul shopping cartului
            cookie.Value = shoppingCart.ShoppingCartId.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return shoppingCart;
        }

        public bool RemoveFromShoppingCart(HttpContextBase httpContext, int cartItemId)
        {
            bool succes = true;
            ShoppingCart shoppingCart = GetShoppingCart(httpContext);
            CartItem shoppingCartItem;

            shoppingCartItem = shoppingCart.ShoppingCartItems.FirstOrDefault(i => i.ShoppingCartItemId == cartItemId);

            if (shoppingCartItem == null)
            {
                throw new NullReferenceException();
            }

            var itemsToStay = shoppingCart.ShoppingCartItems;
            itemsToStay.Remove(shoppingCartItem);

            shoppingCart.ShoppingCartItems = null;
            shoppingCart.ShoppingCartItems = itemsToStay;

            carts.Commit();
            return succes;
        }

        public bool AddToShoppingCart(HttpContextBase httpContext, int productId, int quantity)
        {
            bool succes = true;
            ShoppingCart shoppingCart = GetShoppingCart(httpContext);
            CartItem shoppingCartItem;

           shoppingCartItem = shoppingCart.ShoppingCartItems.FirstOrDefault(i => i.ProductId == productId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new CartItem()
                {
                    ShoppingCartId = shoppingCart.ShoppingCartId,
                    ProductId = productId,
                    Quantity = quantity
                };
                shoppingCart.ShoppingCartItems.Add(shoppingCartItem);

            }
            else
            {
                shoppingCartItem.Quantity += quantity;
            }
            carts.Commit();
            return succes;
        }

        public ShoppingCart GetShoppingCart(HttpContextBase httpContext)
        {
            var cookie = httpContext.Request.Cookies.Get(CART_SESSION_NAME);
            ShoppingCart shoppingCart;

            var cartId = new int();

            if (cookie != null && int.TryParse(cookie.Value, out cartId))
            {
                shoppingCart = carts.GetById(cartId);
            }
            else
            {
                shoppingCart = CreateNewShoppingCart(httpContext);
            }

            return shoppingCart;
        }
    }
}
