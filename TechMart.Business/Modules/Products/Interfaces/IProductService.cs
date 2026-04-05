using System;
using System.Collections.Generic;
using System.Text;
using TechMart.Domain.Entities;

namespace TechMart.Business.Modules.Products.Interfaces
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<Product> AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task<bool> DeleteAsync(int id);
    }
}
