using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TechMart.DataAccess.Data;
using TechMart.DataAccess.Modules.Orders.Interfaces;
using TechMart.Domain.Entities;

namespace TechMart.DataAccess.Modules.Orders;

public class OrderRepository : IOrderRepository
{
    private readonly TechMartDbContext _dbContext;

    public OrderRepository(TechMartDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int?> PlaceOrderAsync(OrderPlaceRequest request, CancellationToken cancellationToken = default)
    {
        await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        var cartItems = await _dbContext.CartItems
            .Where(c => c.CartSessionId == request.CartSessionId)
            .ToListAsync(cancellationToken);

        if (cartItems.Count == 0)
        {
            await transaction.RollbackAsync(cancellationToken);
            return null;
        }

        var productIds = cartItems.Select(c => c.ProductId).Distinct().ToList();
        var products = await _dbContext.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        var productById = products.ToDictionary(p => p.Id);

        foreach (var line in cartItems)
        {
            if (!productById.TryGetValue(line.ProductId, out var product))
            {
                await transaction.RollbackAsync(cancellationToken);
                return null;
            }

            if (line.Quantity > product.StockQuantity)
            {
                await transaction.RollbackAsync(cancellationToken);
                return null;
            }
        }

        decimal subtotal = 0;
        foreach (var line in cartItems)
        {
            var product = productById[line.ProductId];
            subtotal += product.Price * line.Quantity;
        }

        var tax = Math.Round(subtotal * request.TaxRatePercent / 100m, 2, MidpointRounding.AwayFromZero);
        var shipping = request.ShippingFee;
        var total = subtotal + tax + shipping;

        var now = DateTime.UtcNow;
        var order = new Order
        {
            CartSessionId = request.CartSessionId,
            CustomerName = request.CustomerName,
            Phone = request.Phone,
            Email = request.Email,
            ShippingAddress = request.ShippingAddress,
            OrderDate = now,
            CreatedDate = now,
            UpdatedDate = now,
            Status = "Placed",
            SubTotal = subtotal,
            TaxAmount = tax,
            ShippingAmount = shipping,
            TotalAmount = total
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync(cancellationToken);

        foreach (var line in cartItems)
        {
            var product = productById[line.ProductId];
            var orderItem = new OrderItem
            {
                OrderId = order.Id,
                ProductId = line.ProductId,
                Quantity = line.Quantity,
                UnitPrice = product.Price,
                CreatedDate = now,
                UpdatedDate = now
            };
            _dbContext.OrderItems.Add(orderItem);
            product.StockQuantity -= line.Quantity;
        }

        _dbContext.CartItems.RemoveRange(cartItems);
        await _dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return order.Id;
    }

    public async Task<Order?> GetByIdWithItemsAsync(int orderId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders
            .AsNoTracking()
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> ListByCartSessionIdAsync(string cartSessionId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders
            .AsNoTracking()
            .Where(o => o.CartSessionId == cartSessionId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync(cancellationToken);
    }
}
