using TechMart.Business.Modules.Cart.Interfaces;
using TechMart.DataAccess.Modules.Cart.Interfaces;
using TechMart.DataAccess.Modules.Products.Interfaces;
using TechMart.Domain.Entities;

namespace TechMart.Business.Modules.Cart;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyList<CartItem>> GetCartItemsAsync(string cartSessionId, CancellationToken cancellationToken = default)
    {
        return await _cartRepository.GetBySessionIdAsync(cartSessionId, cancellationToken);
    }

    public async Task AddAsync(string cartSessionId, int productId, int quantity, CancellationToken cancellationToken = default)
    {
        if (quantity < 1)
            quantity = 1;

        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null || product.StockQuantity < 1)
            return;

        var existing = await _cartRepository.GetTrackedBySessionAndProductAsync(cartSessionId, productId, cancellationToken);
        var now = DateTime.UtcNow;

        if (existing != null)
        {
            var next = existing.Quantity + quantity;
            if (next > product.StockQuantity)
                next = product.StockQuantity;
            existing.Quantity = next;
            existing.UpdatedDate = now;
            await _cartRepository.UpdateAsync(existing, cancellationToken);
            return;
        }

        var addQty = Math.Min(quantity, product.StockQuantity);
        var item = new CartItem
        {
            CartSessionId = cartSessionId,
            ProductId = productId,
            Quantity = addQty,
            CreatedDate = now,
            UpdatedDate = now
        };
        await _cartRepository.AddAsync(item, cancellationToken);
    }

    public async Task IncrementAsync(string cartSessionId, int productId, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null || product.StockQuantity < 1)
            return;

        var existing = await _cartRepository.GetTrackedBySessionAndProductAsync(cartSessionId, productId, cancellationToken);
        var now = DateTime.UtcNow;

        if (existing == null)
        {
            await AddAsync(cartSessionId, productId, 1, cancellationToken);
            return;
        }

        if (existing.Quantity >= product.StockQuantity)
            return;

        existing.Quantity += 1;
        existing.UpdatedDate = now;
        await _cartRepository.UpdateAsync(existing, cancellationToken);
    }

    public async Task DecrementAsync(string cartSessionId, int productId, CancellationToken cancellationToken = default)
    {
        var existing = await _cartRepository.GetTrackedBySessionAndProductAsync(cartSessionId, productId, cancellationToken);
        if (existing == null)
            return;

        var now = DateTime.UtcNow;
        if (existing.Quantity <= 1)
        {
            await _cartRepository.RemoveAsync(existing, cancellationToken);
            return;
        }

        existing.Quantity -= 1;
        existing.UpdatedDate = now;
        await _cartRepository.UpdateAsync(existing, cancellationToken);
    }

    public decimal GetSubtotal(IReadOnlyList<CartItem> items, IReadOnlyDictionary<int, decimal> unitPriceByProductId)
    {
        decimal sum = 0;
        foreach (var line in items)
        {
            if (unitPriceByProductId.TryGetValue(line.ProductId, out var price))
                sum += price * line.Quantity;
        }
        return sum;
    }
}
