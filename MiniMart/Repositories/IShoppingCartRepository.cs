using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using MiniMart.Models;

namespace MiniMart.Repositories
{
    public interface IShoppingCartRepository
    {
        string ShoppingCartId { get; set; }

        int AddToCart(Product product);
        int CreateOrder(Order order);
        void Dispose();
        void EmptyCart();
        ShoppingCartRepository GetCart(HttpContextBase context);
        ShoppingCartRepository GetCart(Controller controller);
        Cart GetCartById(int Id);
        string GetCartId(HttpContextBase context);
        double GetCartItemCount();
        List<Cart> GetCartItems();
        int GetCount();
        decimal GetTotal();
        void MigrateCart(string userName);
        int RemoveFromCart(int Id);
    }
}