using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TechMart.Domain.Entities;
using TechMart.Presentation.Modules.Products.ViewModels;

namespace TechMart.Presentation.Modules.Products
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductCreateViewModel, Product>();
            CreateMap<ProductEditViewModel, Product>();

            CreateMap<Product, ProductEditViewModel>();
            CreateMap<Product, ProductListViewModel>();
        }
    }
}
