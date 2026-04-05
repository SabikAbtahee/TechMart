using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechMart.Domain.Entities
{
    public class Product : BaseEntity
    {

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Product needs a description between 10 to 500 characters")]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive value")]
        public int StockQuantity { get; set; }

        public string? ImageUrl { get; set; }


    }
}
