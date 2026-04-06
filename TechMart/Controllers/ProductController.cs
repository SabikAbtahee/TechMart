using Microsoft.AspNetCore.Mvc;
using TechMart.Business.Modules.Products;
using TechMart.Presentation.Modules.Products.Interfaces;
using TechMart.Presentation.Modules.Products.ViewModels;
using TechMart.Presentation.Modules.Shared.Interfaces;

namespace TechMart.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductViewModelProvider _productViewModelProvider;
        private readonly IFileStorageService _fileStorageService;

        public ProductController(IProductViewModelProvider productViewModelProvider, IFileStorageService fileStorageService)
        {
            _productViewModelProvider = productViewModelProvider;
            _fileStorageService = fileStorageService;
        }

        // GET: Product/Index
        public async Task<IActionResult> Index()
        {
            var products = await _productViewModelProvider.GetAllAsync();
            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productViewModelProvider.GetByIdAsync(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel model, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

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

        // GET: Product/Edit/5
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

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditViewModel model, IFormFile? imageFile)
        {
            if (id != model.Id)
            {
                TempData["ErrorMessage"] = "Product ID mismatch.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

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

        // GET: Product/Delete/5
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

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Get product to access image path before deletion
                var product = await _productViewModelProvider.GetByIdAsync(id);
                
                await _productViewModelProvider.DeleteAsync(id);
                
                // Delete physical image file if exists
                if (product != null && !string.IsNullOrEmpty(product.ImageUrl))
                {
                    _fileStorageService.DeleteFile(product.ImageUrl);
                }
                
                TempData["SuccessMessage"] = "Product deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting product: {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
