using TechMart.Domain.Entities;

namespace TechMart.DataAccess.Modules.Orders.Interfaces;

public interface IOrderRepository
{
    Task<int?> PlaceOrderAsync(OrderPlaceRequest request, CancellationToken cancellationToken = default);

    Task<Order?> GetByIdWithItemsAsync(int orderId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Order>> ListByCartSessionIdAsync(string cartSessionId, CancellationToken cancellationToken = default);
}
