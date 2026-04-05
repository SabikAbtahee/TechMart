using System;
using System.Collections.Generic;
using System.Text;
using TechMart.Presentation.Modules.Shared.ViewModels;

namespace TechMart.Presentation.Modules.Products.ViewModels
{
    public class ProductListViewModel : BaseViewModel
    {

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public int StockQuantity { get; set; }


    }
}
