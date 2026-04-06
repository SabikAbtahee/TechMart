using TechMart.Business.Modules.Orders.Interfaces;
using TechMart.Presentation.Modules.Cart.Interfaces;
using TechMart.Presentation.Modules.Orders.Interfaces;
using TechMart.Presentation.Modules.Orders.ViewModels;

namespace TechMart.Presentation.Modules.Orders;

public class OrderViewModelProvider : IOrderViewModelProvider
{
    private readonly IOrderService _orderService;
    private readonly ICartSessionIdProvider _cartSessionIdProvider;

    public OrderViewModelProvider(IOrderService orderService, ICartSessionIdProvider cartSessionIdProvider)
    {
        _orderService = orderService;
        _cartSessionIdProvider = cartSessionIdProvider;
    }

    public async Task<OrderIndexViewModel> GetIndexAsync(CancellationToken cancellationToken = default)
    {
        var sessionId = _cartSessionIdProvider.GetCartSessionId();
        var orders = await _orderService.ListByCartSessionIdAsync(sessionId, cancellationToken);

        var rows = orders.Select(o => new OrderSummaryRowViewModel
        {
            Id = o.Id,
            OrderDate = o.OrderDate,
            TotalAmount = o.TotalAmount,
            Status = o.Status
        }).ToList();

        return new OrderIndexViewModel { Orders = rows };
    }

    public async Task<OrderDetailsViewModel?> GetDetailsAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var sessionId = _cartSessionIdProvider.GetCartSessionId();
        var order = await _orderService.GetByIdForSessionAsync(orderId, sessionId, cancellationToken);
        if (order == null)
            return null;

        var lines = order.OrderItems.Select(oi => new OrderLineRowViewModel
        {
            ProductName = oi.Product?.Name ?? $"Product #{oi.ProductId}",
            Quantity = oi.Quantity,
            UnitPrice = oi.UnitPrice
        }).ToList();

        return new OrderDetailsViewModel
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            Status = order.Status,
            CustomerName = order.CustomerName,
            Phone = order.Phone,
            Email = order.Email,
            ShippingAddress = order.ShippingAddress,
            SubTotal = order.SubTotal,
            TaxAmount = order.TaxAmount,
            ShippingAmount = order.ShippingAmount,
            TotalAmount = order.TotalAmount,
            Lines = lines
        };
    }
}
