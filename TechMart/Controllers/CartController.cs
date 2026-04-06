using Microsoft.AspNetCore.Mvc;
using TechMart.Presentation.Modules.Cart.Interfaces;

namespace TechMart.Controllers;

public class CartController : Controller
{
    private readonly ICartViewModelProvider _cartViewModelProvider;

    public CartController(ICartViewModelProvider cartViewModelProvider)
    {
        _cartViewModelProvider = cartViewModelProvider;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var model = await _cartViewModelProvider.GetCartPageAsync(cancellationToken);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int productId, int quantity = 1, string? returnUrl = null, CancellationToken cancellationToken = default)
    {
        if (quantity < 1)
            quantity = 1;

        await _cartViewModelProvider.AddAsync(productId, quantity, cancellationToken);
        TempData["CartMessage"] = "Added to cart.";

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return LocalRedirect(returnUrl);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Increment(int productId, string? returnUrl = null, CancellationToken cancellationToken = default)
    {
        await _cartViewModelProvider.IncrementAsync(productId, cancellationToken);
        TempData["CartMessage"] = "Cart updated.";

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return LocalRedirect(returnUrl);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Decrement(int productId, string? returnUrl = null, CancellationToken cancellationToken = default)
    {
        await _cartViewModelProvider.DecrementAsync(productId, cancellationToken);
        TempData["CartMessage"] = "Cart updated.";

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return LocalRedirect(returnUrl);

        return RedirectToAction(nameof(Index));
    }
}
