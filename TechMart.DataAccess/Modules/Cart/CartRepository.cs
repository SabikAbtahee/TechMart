using Microsoft.EntityFrameworkCore;
using TechMart.DataAccess.Data;
using TechMart.DataAccess.Modules.Cart.Interfaces;
using TechMart.Domain.Entities;

namespace TechMart.DataAccess.Modules.Cart;

public class CartRepository : ICartRepository
{
    private readonly TechMartDbContext _dbContext;

    public CartRepository(TechMartDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<CartItem>> GetBySessionIdAsync(string cartSessionId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CartItems
            .AsNoTracking()
            .Where(c => c.CartSessionId == cartSessionId)
            .OrderBy(c => c.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<CartItem?> GetTrackedBySessionAndProductAsync(string cartSessionId, int productId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CartItems
            .FirstOrDefaultAsync(c => c.CartSessionId == cartSessionId && c.ProductId == productId, cancellationToken);
    }

    public async Task AddAsync(CartItem item, CancellationToken cancellationToken = default)
    {
        await _dbContext.CartItems.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CartItem item, CancellationToken cancellationToken = default)
    {
        _dbContext.CartItems.Update(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(CartItem item, CancellationToken cancellationToken = default)
    {
        _dbContext.CartItems.Remove(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ClearBySessionIdAsync(string cartSessionId, CancellationToken cancellationToken = default)
    {
        var items = await _dbContext.CartItems
            .Where(c => c.CartSessionId == cartSessionId)
            .ToListAsync(cancellationToken);

        if (items.Count == 0)
            return;

        _dbContext.CartItems.RemoveRange(items);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
