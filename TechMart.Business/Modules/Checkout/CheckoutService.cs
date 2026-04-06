using Microsoft.Extensions.Options;
using TechMart.Business.Modules.Cart.Interfaces;
using TechMart.Business.Modules.Checkout.Interfaces;
using TechMart.DataAccess.Modules.Orders;
using TechMart.DataAccess.Modules.Orders.Interfaces;

namespace TechMart.Business.Modules.Checkout;

public class CheckoutService : ICheckoutService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartService _cartService;
    private readonly IOptionsSnapshot<CheckoutOptions> _options;

    public CheckoutService(
        IOrderRepository orderRepository,
        ICartService cartService,
        IOptionsSnapshot<CheckoutOptions> options)
    {
        _orderRepository = orderRepository;
        _cartService = cartService;
        _options = options;
    }

    public async Task<PlaceOrderResult> PlaceOrderAsync(
        string cartSessionId,
        string customerName,
        string phone,
        string email,
        string shippingAddress,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            return PlaceOrderResult.Fail("Name is required.");
        if (string.IsNullOrWhiteSpace(phone))
            return PlaceOrderResult.Fail("Phone is required.");
        if (string.IsNullOrWhiteSpace(email))
            return PlaceOrderResult.Fail("Email is required.");
        if (string.IsNullOrWhiteSpace(shippingAddress))
            return PlaceOrderResult.Fail("Shipping address is required.");

        var cartItems = await _cartService.GetCartItemsAsync(cartSessionId, cancellationToken);
        if (cartItems.Count == 0)
            return PlaceOrderResult.Fail("Your cart is empty.");

        var opts = _options.Value;
        var request = new OrderPlaceRequest
        {
            CartSessionId = cartSessionId,
            CustomerName = customerName.Trim(),
            Phone = phone.Trim(),
            Email = email.Trim(),
            ShippingAddress = shippingAddress.Trim(),
            TaxRatePercent = opts.TaxRatePercent,
            ShippingFee = opts.ShippingFee
        };

        var orderId = await _orderRepository.PlaceOrderAsync(request, cancellationToken);
        if (orderId == null)
            return PlaceOrderResult.Fail("Could not complete the order. Items may no longer be in stock.");

        return PlaceOrderResult.Ok(orderId.Value);
    }

    public async Task<OrderConfirmationDetails?> GetOrderForConfirmationAsync(
        int orderId,
        string cartSessionId,
        CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdWithItemsAsync(orderId, cancellationToken);
        if (order == null || order.CartSessionId != cartSessionId)
            return null;

        var lines = order.OrderItems.Select(oi => new OrderLineDetails
        {
            ProductName = oi.Product?.Name ?? $"Product #{oi.ProductId}",
            Quantity = oi.Quantity,
            UnitPrice = oi.UnitPrice
        }).ToList();

        return new OrderConfirmationDetails
        {
            OrderId = order.Id,
            OrderDate = order.OrderDate,
            SubTotal = order.SubTotal,
            TaxAmount = order.TaxAmount,
            ShippingAmount = order.ShippingAmount,
            TotalAmount = order.TotalAmount,
            Lines = lines
        };
    }
}
