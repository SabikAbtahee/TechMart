using System;
using System.Collections.Generic;
using System.Text;
using TechMart.Business.Exceptions;
using TechMart.Business.Modules.Products.Interfaces;
using TechMart.DataAccess.Modules.Products.Interfaces;
using TechMart.Domain.Entities;

namespace TechMart.Business.Modules.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> AddAsync(Product product)
        {
            product.CreatedDate = DateTime.UtcNow;
            product.UpdatedDate = DateTime.UtcNow;
            return await _productRepository.AddAsync(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) {
                throw new InvalidUserInputException("Product does not exist");
            }
            return await _productRepository.DeleteAsync(product);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Product product)
        {
            product.UpdatedDate = DateTime.UtcNow;

            await _productRepository.UpdateAsync(product);
        }
    }
}
