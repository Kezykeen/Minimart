using MiniMart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniMart.Repositories
{
    public class CheckOutRepository : ICheckOutRepository
    {
        private readonly ApplicationDbContext db;

        public CheckOutRepository()
        {
            db = new ApplicationDbContext();
        }

        public bool CheckOrderValidity(int id)
        {
            // Validate if the order exists
            return db.Order.Any(
                o => o.OrderId == id);
        }

        public Order GetOrder(int id)
        {
            return db.Order.Find(id);
        }
    }
}