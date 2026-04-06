using Microsoft.AspNetCore.Mvc;
using TechMart.Presentation.Modules.Orders.Interfaces;

namespace TechMart.Controllers;

public class OrderController : Controller
{
    private readonly IOrderViewModelProvider _orderViewModelProvider;

    public OrderController(IOrderViewModelProvider orderViewModelProvider)
    {
        _orderViewModelProvider = orderViewModelProvider;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var model = await _orderViewModelProvider.GetIndexAsync(cancellationToken);
        return View(model);
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var model = await _orderViewModelProvider.GetDetailsAsync(id, cancellationToken);
        if (model == null)
            return NotFound();

        return View(model);
    }
}
