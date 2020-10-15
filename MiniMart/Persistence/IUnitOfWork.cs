using System.Data.Entity.Infrastructure;
using MiniMart.Models;
using MiniMart.Repositories;

namespace MiniMart.Persistence
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepo { get; }
        IProductRepository ProductRepo { get; }
        ICheckOutRepository CheckOutRepo { get; }
        IShoppingCartRepository ShoppingCartRepo { get; }

        void Complete();
        void Dispose();
        DbEntityEntry<Product> Entry(Product product);
    }
}