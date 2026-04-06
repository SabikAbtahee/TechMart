using TechMart.Domain.Entities;

namespace TechMart.Business.Modules.Cart.Interfaces;

public interface ICartService
{
    Task<IReadOnlyList<CartItem>> GetCartItemsAsync(string cartSessionId, CancellationToken cancellationToken = default);

    Task AddAsync(string cartSessionId, int productId, int quantity, CancellationToken cancellationToken = default);

    Task IncrementAsync(string cartSessionId, int productId, CancellationToken cancellationToken = default);

    Task DecrementAsync(string cartSessionId, int productId, CancellationToken cancellationToken = default);

    Task SetQuantityAsync(string cartSessionId, int productId, int quantity, CancellationToken cancellationToken = default);

    Task RemoveLineAsync(string cartSessionId, int productId, CancellationToken cancellationToken = default);

    Task ClearAsync(string cartSessionId, CancellationToken cancellationToken = default);

    decimal GetSubtotal(IReadOnlyList<CartItem> items, IReadOnlyDictionary<int, decimal> unitPriceByProductId);
}
