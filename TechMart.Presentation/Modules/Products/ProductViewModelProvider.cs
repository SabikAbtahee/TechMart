using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TechMart.Business.Modules.Products.Interfaces;
using TechMart.Domain.Entities;
using TechMart.Presentation.Modules.Products.Interfaces;
using TechMart.Presentation.Modules.Products.ViewModels;

namespace TechMart.Presentation.Modules.Products
{
    public class ProductViewModelProvider : IProductViewModelProvider
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;


        public ProductViewModelProvider(IProductService productService,IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task AddAsync(ProductCreateViewModel product)
        {
            var addedProduct = _mapper.Map<Product>(product);
            await _productService.AddAsync(addedProduct);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }

            return await _productService.DeleteAsync(product.Id);
        }

        public async Task<IReadOnlyList<ProductListViewModel>> GetAllAsync()
        {
            var productList = await _productService.GetAllAsync();
            return _mapper.Map<IReadOnlyList<ProductListViewModel>>(productList);
        }

        public async Task<ProductEditViewModel?> GetByIdAsync(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return null;

            return _mapper.Map<ProductEditViewModel>(product);
        }

        public async Task UpdateAsync(ProductEditViewModel product)
        {
            var productUpdated = _mapper.Map<Product>(product);
            await _productService.UpdateAsync(productUpdated);
        }
    }
}
