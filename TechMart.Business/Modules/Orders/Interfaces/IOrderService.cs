using TechMart.Domain.Entities;

namespace TechMart.Business.Modules.Orders.Interfaces;

public interface IOrderService
{
    Task<IReadOnlyList<Order>> ListByCartSessionIdAsync(string cartSessionId, CancellationToken cancellationToken = default);

    Task<Order?> GetByIdForSessionAsync(int orderId, string cartSessionId, CancellationToken cancellationToken = default);
}
