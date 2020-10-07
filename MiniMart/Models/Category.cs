using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniMart.Models
{
    public class Category
    {
        public string Name { get; set; }

        public int Id { get; set; }
        public List<Product> Products { get; set; }
    }
}