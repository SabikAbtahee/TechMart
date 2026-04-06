using Microsoft.AspNetCore.Mvc;

namespace TechMart.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Product", new { area = "Admin" });
    }
}
