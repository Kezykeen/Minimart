using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniMart.Models
{
    public class Product
    {
        [Required]
        public string Name { get; set; }

        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Number must be between 0 and 100")]
        public int Price { get; set; }

        [Required]
        public string ArtUrl { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }
    }
}