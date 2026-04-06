using Microsoft.AspNetCore.Mvc;
using TechMart.Business.Modules.Checkout.Interfaces;
using TechMart.Presentation.Modules.Cart.Interfaces;
using TechMart.Presentation.Modules.Checkout.Interfaces;
using TechMart.Presentation.Modules.Checkout.ViewModels;

namespace TechMart.Controllers;

public class CheckoutController : Controller
{
    private readonly ICheckoutViewModelProvider _checkoutViewModelProvider;
    private readonly ICheckoutService _checkoutService;
    private readonly ICartSessionIdProvider _cartSessionIdProvider;

    public CheckoutController(
        ICheckoutViewModelProvider checkoutViewModelProvider,
        ICheckoutService checkoutService,
        ICartSessionIdProvider cartSessionIdProvider)
    {
        _checkoutViewModelProvider = checkoutViewModelProvider;
        _checkoutService = checkoutService;
        _cartSessionIdProvider = cartSessionIdProvider;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var model = await _checkoutViewModelProvider.GetCheckoutPageAsync(cancellationToken);
        if (model == null)
            return RedirectToAction("Index", "Cart");

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(CheckoutPageViewModel model, CancellationToken cancellationToken)
    {
        var fresh = await _checkoutViewModelProvider.GetCheckoutPageAsync(cancellationToken);
        if (fresh == null)
            return RedirectToAction("Index", "Cart");

        model.Lines = fresh.Lines;
        model.Subtotal = fresh.Subtotal;
        model.TaxAmount = fresh.TaxAmount;
        model.ShippingAmount = fresh.ShippingAmount;
        model.Total = fresh.Total;

        if (!ModelState.IsValid)
            return View(model);

        var sessionId = _cartSessionIdProvider.GetCartSessionId();
        var result = await _checkoutService.PlaceOrderAsync(
            sessionId,
            model.CustomerName,
            model.Phone,
            model.Email,
            model.ShippingAddress,
            cancellationToken);

        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Unable to place order.");
            return View(model);
        }

        return RedirectToAction(nameof(Confirmation), new { id = result.OrderId });
    }

    [HttpGet]
    public async Task<IActionResult> Confirmation(int id, CancellationToken cancellationToken)
    {
        var model = await _checkoutViewModelProvider.GetConfirmationAsync(id, cancellationToken);
        if (model == null)
            return NotFound();

        return View(model);
    }
}
