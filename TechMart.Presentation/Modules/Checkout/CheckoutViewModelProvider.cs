using Microsoft.Extensions.Options;
using TechMart.Business.Modules.Checkout;
using TechMart.Business.Modules.Checkout.Interfaces;
using TechMart.Presentation.Modules.Cart.Interfaces;
using TechMart.Presentation.Modules.Checkout.Interfaces;
using TechMart.Presentation.Modules.Checkout.ViewModels;

namespace TechMart.Presentation.Modules.Checkout;

public class CheckoutViewModelProvider : ICheckoutViewModelProvider
{
    private readonly ICartViewModelProvider _cartViewModelProvider;
    private readonly ICartSessionIdProvider _cartSessionIdProvider;
    private readonly ICheckoutService _checkoutService;
    private readonly IOptionsSnapshot<CheckoutOptions> _options;

    public CheckoutViewModelProvider(
        ICartViewModelProvider cartViewModelProvider,
        ICartSessionIdProvider cartSessionIdProvider,
        ICheckoutService checkoutService,
        IOptionsSnapshot<CheckoutOptions> options)
    {
        _cartViewModelProvider = cartViewModelProvider;
        _cartSessionIdProvider = cartSessionIdProvider;
        _checkoutService = checkoutService;
        _options = options;
    }

    public async Task<CheckoutPageViewModel?> GetCheckoutPageAsync(CancellationToken cancellationToken = default)
    {
        var cart = await _cartViewModelProvider.GetCartPageAsync(cancellationToken);
        if (cart.Lines.Count == 0)
            return null;

        var opts = _options.Value;
        var tax = Math.Round(cart.Subtotal * opts.TaxRatePercent / 100m, 2, MidpointRounding.AwayFromZero);
        var shipping = opts.ShippingFee;
        var total = cart.Subtotal + tax + shipping;

        return new CheckoutPageViewModel
        {
            Lines = cart.Lines,
            Subtotal = cart.Subtotal,
            TaxAmount = tax,
            ShippingAmount = shipping,
            Total = total
        };
    }

    public async Task<OrderConfirmationViewModel?> GetConfirmationAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var sessionId = _cartSessionIdProvider.GetCartSessionId();
        var details = await _checkoutService.GetOrderForConfirmationAsync(orderId, sessionId, cancellationToken);
        if (details == null)
            return null;

        return new OrderConfirmationViewModel
        {
            OrderId = details.OrderId,
            OrderDate = details.OrderDate,
            SubTotal = details.SubTotal,
            TaxAmount = details.TaxAmount,
            ShippingAmount = details.ShippingAmount,
            TotalAmount = details.TotalAmount,
            Lines = details.Lines.Select(l => new OrderConfirmationLineViewModel
            {
                ProductName = l.ProductName,
                Quantity = l.Quantity,
                UnitPrice = l.UnitPrice
            }).ToList()
        };
    }
}
