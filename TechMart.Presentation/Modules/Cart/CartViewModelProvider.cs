using TechMart.Business.Modules.Cart.Interfaces;
using TechMart.Business.Modules.Products.Interfaces;
using TechMart.Presentation.Modules.Cart.Interfaces;
using TechMart.Presentation.Modules.Cart.ViewModels;

namespace TechMart.Presentation.Modules.Cart;

public class CartViewModelProvider : ICartViewModelProvider
{
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    private readonly ICartSessionIdProvider _cartSessionIdProvider;

    public CartViewModelProvider(
        ICartService cartService,
        IProductService productService,
        ICartSessionIdProvider cartSessionIdProvider)
    {
        _cartService = cartService;
        _productService = productService;
        _cartSessionIdProvider = cartSessionIdProvider;
    }

    public async Task AddAsync(int productId, int quantity, CancellationToken cancellationToken = default)
    {
        var sessionId = _cartSessionIdProvider.GetCartSessionId();
        await _cartService.AddAsync(sessionId, productId, quantity, cancellationToken);
    }

    public async Task IncrementAsync(int productId, CancellationToken cancellationToken = default)
    {
        var sessionId = _cartSessionIdProvider.GetCartSessionId();
        await _cartService.IncrementAsync(sessionId, productId, cancellationToken);
    }

    public async Task DecrementAsync(int productId, CancellationToken cancellationToken = default)
    {
        var sessionId = _cartSessionIdProvider.GetCartSessionId();
        await _cartService.DecrementAsync(sessionId, productId, cancellationToken);
    }

    public async Task<CartPageViewModel> GetCartPageAsync(CancellationToken cancellationToken = default)
    {
        var sessionId = _cartSessionIdProvider.GetCartSessionId();
        var items = await _cartService.GetCartItemsAsync(sessionId, cancellationToken);

        var lines = new List<CartLineViewModel>();

        foreach (var line in items)
        {
            var product = await _productService.GetByIdAsync(line.ProductId);
            if (product == null)
                continue;

            lines.Add(new CartLineViewModel
            {
                CartItemId = line.Id,
                ProductId = product.Id,
                ProductName = product.Name,
                ImageUrl = product.ImageUrl,
                UnitPrice = product.Price,
                Quantity = line.Quantity
            });
        }

        var subtotal = lines.Sum(l => l.LineSubtotal);
        var totalUnits = lines.Sum(l => l.Quantity);

        return new CartPageViewModel
        {
            Lines = lines,
            Subtotal = subtotal,
            TotalUnits = totalUnits
        };
    }
}
