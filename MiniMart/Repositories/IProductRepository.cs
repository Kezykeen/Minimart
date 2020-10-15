using System.Linq;
using MiniMart.Models;

namespace MiniMart.Repositories
{
    public interface IProductRepository
    {
        void Add(Product product);
        Product FindProduct(int? id);
        IQueryable<Product> GetProductWithCategory();
        void Remove(Product product);
    }
}