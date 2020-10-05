using MiniMart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniMart.ViewModels
{
    public class ProductCategoryViewModel
    {
        public Product product { get; set; }
        public Category category { get; set; }
    }
}