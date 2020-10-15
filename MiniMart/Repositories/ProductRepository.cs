using MiniMart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniMart.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext db;

        public ProductRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public Product FindProduct(int? id)
        {
            return db.Product.Find(id);
        }

        public IQueryable<Product> GetProductWithCategory()
        {
            return db.Product.Include("Category");
        }

        public void Add(Product product)
        {
            db.Product.Add(product);
        }

        public void Remove(Product product)
        {
            db.Product.Remove(product);
        }
    }
}