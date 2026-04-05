using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TechMart.DataAccess.Data;
using TechMart.DataAccess.Modules.Products.Interfaces;
using TechMart.Domain.Entities;

namespace TechMart.DataAccess.Modules.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly TechMartDbContext _dbContext;

        public ProductRepository(TechMartDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            var deletedCount = await _dbContext.SaveChangesAsync();
            return deletedCount > 0;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _dbContext.Products.AsNoTracking().ToListAsync();

        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
