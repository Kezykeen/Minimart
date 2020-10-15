using MiniMart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniMart.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.     
                    Guid tempCartId = Guid.NewGuid();
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        public ShoppingCartRepository GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCartRepository();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public ShoppingCartRepository GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public int GetCount()
        {
            int? count = (from cartItems in db.Cart
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            return count ?? 0;
        }

        public int AddToCart(Product product)
        {
            Cart cartItem = GetCartItem(product);

            var itemCount = 0;

            if (cartItem == null)
            {
                //Create a new cart item if no cart item exists.
                cartItem = new Cart
                {
                    ProductId = product.Id,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                db.Cart.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            itemCount = cartItem.Count;
            db.SaveChanges();
            return itemCount;
        }

        private Cart GetCartItem(Product product)
        {
            return db.Cart.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == product.Id);
        }

        public List<Cart> GetCartItems()
        {
            return db.Cart.Where(
                c => c.CartId == ShoppingCartId).ToList();
        }

        public double GetCartItemCount()
        {
            return db.Cart.Where(c => c.CartId == ShoppingCartId).ToArray().Length;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetails
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Product.Price,
                    Quantity = item.Count
                };

                orderTotal += (item.Count * item.Product.Price);

                db.OrderDetails.Add(orderDetail);
            }

            order.Total = orderTotal;

            //Save Order
            db.Order.Add(order);
            db.SaveChanges();

            EmptyCart();

            return order.OrderId;
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = GetCartItems();

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }

            db.SaveChanges();
        }

        public decimal GetTotal()
        {
            decimal? total = (from items in db.Cart
                              where items.CartId == ShoppingCartId
                              select (int?)items.Count *
                              items.Product.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int RemoveFromCart(int Id)
        {
            Cart cartItem = GetCartById(Id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }

                else
                {
                    db.Cart.Remove(cartItem);
                }

                db.SaveChanges();
            }

            return itemCount;
        }

        public Cart GetCartById(int Id)
        {
            return db.Cart.Single(
                c => c.CartId == ShoppingCartId
                && c.RecordId == Id);
        }

        public void EmptyCart()
        {
            var cartItems = GetCartItems();

            foreach (var cartItem in cartItems)
            {
                db.Cart.Remove(cartItem);
            }

            db.SaveChanges();
        }

        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
        }
    }
}