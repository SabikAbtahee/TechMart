using Microsoft.AspNetCore.Mvc;
using TechMart.Business.Modules.Products;
using TechMart.Presentation.Modules.Products.Interfaces;
using TechMart.Presentation.Modules.Products.ViewModels;
using TechMart.Presentation.Modules.Shared.Interfaces;

namespace TechMart.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IProductViewModelProvider _productViewModelProvider;
    private readonly IFileStorageService _fileStorageService;

    public ProductController(IProductViewModelProvider productViewModelProvider, IFileStorageService fileStorageService)
    {
        _productViewModelProvider = productViewModelProvider;
        _fileStorageService = fileStorageService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productViewModelProvider.GetAllAsync();
        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateViewModel model, IFormFile? imageFile)
    {
        if (model.Price < 0)
            ModelState.AddModelError(nameof(model.Price), "Price must be zero or greater.");

        if (!ModelState.IsValid)
            return View(model);

        try
        {
            model.ImageUrl = await _fileStorageService.SaveFileAsync(
                imageFile,
                "images/products",
                ProductImageRules.AllowedExtensions);

            await _productViewModelProvider.AddAsync(model);
            TempData["SuccessMessage"] = "Product created successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating product: {ex.Message}");
            return View(model);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productViewModelProvider.GetByIdAsync(id);
        if (product == null)
        {
            TempData["ErrorMessage"] = "Product not found.";
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductEditViewModel model, IFormFile? imageFile)
    {
        if (id != model.Id)
        {
            TempData["ErrorMessage"] = "Product ID mismatch.";
            return RedirectToAction(nameof(Index));
        }

        if (model.Price < 0)
            ModelState.AddModelError(nameof(model.Price), "Price must be zero or greater.");

        if (!ModelState.IsValid)
            return View(model);

        try
        {
            model.ImageUrl = await _fileStorageService.SaveFileAsync(
                imageFile,
                "images/products",
                ProductImageRules.AllowedExtensions,
                model.ImageUrl);

            await _productViewModelProvider.UpdateAsync(model);
            TempData["SuccessMessage"] = "Product updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error updating product: {ex.Message}");
            return View(model);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productViewModelProvider.GetByIdAsync(id);
        if (product == null)
        {
            TempData["ErrorMessage"] = "Product not found.";
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var product = await _productViewModelProvider.GetByIdAsync(id);

            await _productViewModelProvider.DeleteAsync(id);

            if (product != null && !string.IsNullOrEmpty(product.ImageUrl))
                _fileStorageService.DeleteFile(product.ImageUrl);

            TempData["SuccessMessage"] = "Product deleted successfully!";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error deleting product: {ex.Message}";
        }

        return RedirectToAction(nameof(Index));
    }
}
