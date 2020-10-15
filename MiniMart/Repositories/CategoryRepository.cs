using MiniMart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniMart.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext db;

        public CategoryRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public System.Data.Entity.DbSet<Category> GetCategory()
        {
            return db.Category;
        }
    }
}