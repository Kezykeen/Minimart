using MiniMart.Models;
using MiniMart.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniMart.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;
        public IProductRepository ProductRepo { get; private set; }
        public ICategoryRepository CategoryRepo { get; private set; }
        public ICheckOutRepository CheckOutRepo { get; private set; }
        public IShoppingCartRepository ShoppingCartRepo { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            db = context;
            ProductRepo = new ProductRepository(context);
            CategoryRepo = new CategoryRepository(context);
            ShoppingCartRepo = new ShoppingCartRepository();
            CheckOutRepo = new CheckOutRepository();
        }

        public void Complete()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public System.Data.Entity.Infrastructure.DbEntityEntry<Product> Entry(Product product)
        {
            return db.Entry(product);
        }
    }
}