using System;
using System.Collections.Generic;
using System.Text;
using TechMart.Presentation.Modules.Products.ViewModels;

namespace TechMart.Presentation.Modules.Products.Interfaces
{
    public interface IProductViewModelProvider
    {
        Task<IReadOnlyList<ProductListViewModel>> GetAllAsync();

        Task<ProductEditViewModel?> GetByIdAsync(int id);

        Task AddAsync(ProductCreateViewModel product);

        Task UpdateAsync(ProductEditViewModel product);

        Task<bool> DeleteAsync(int id);
    }
}
