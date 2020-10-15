using System.Data.Entity;
using MiniMart.Models;

namespace MiniMart.Repositories
{
    public interface ICategoryRepository
    {
        DbSet<Category> GetCategory();
    }
}