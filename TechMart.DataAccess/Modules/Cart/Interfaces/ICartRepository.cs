using TechMart.Domain.Entities;

namespace TechMart.DataAccess.Modules.Cart.Interfaces;

public interface ICartRepository
{
    Task<IReadOnlyList<CartItem>> GetBySessionIdAsync(string cartSessionId, CancellationToken cancellationToken = default);

    Task<CartItem?> GetTrackedBySessionAndProductAsync(string cartSessionId, int productId, CancellationToken cancellationToken = default);

    Task AddAsync(CartItem item, CancellationToken cancellationToken = default);

    Task UpdateAsync(CartItem item, CancellationToken cancellationToken = default);

    Task RemoveAsync(CartItem item, CancellationToken cancellationToken = default);
}
