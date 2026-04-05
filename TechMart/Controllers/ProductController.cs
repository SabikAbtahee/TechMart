using Microsoft.AspNetCore.Mvc;
using TechMart.Presentation.Modules.Products.Interfaces;

namespace TechMart.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductViewModelProvider _productViewModelProvider;

        public ProductController(IProductViewModelProvider productViewModelProvider)
        {
            _productViewModelProvider = productViewModelProvider;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productViewModelProvider.GetAllAsync();
            return View(products);
        }
    }
}
