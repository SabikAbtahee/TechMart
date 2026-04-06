using TechMart.Business.Modules.Orders.Interfaces;
using TechMart.DataAccess.Modules.Orders.Interfaces;
using TechMart.Domain.Entities;

namespace TechMart.Business.Modules.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IReadOnlyList<Order>> ListByCartSessionIdAsync(string cartSessionId, CancellationToken cancellationToken = default)
    {
        return await _orderRepository.ListByCartSessionIdAsync(cartSessionId, cancellationToken);
    }

    public async Task<Order?> GetByIdForSessionAsync(int orderId, string cartSessionId, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdWithItemsAsync(orderId, cancellationToken);
        if (order == null || order.CartSessionId != cartSessionId)
            return null;

        return order;
    }
}
