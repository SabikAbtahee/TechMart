using TechMart.Presentation.Modules.Cart.ViewModels;

namespace TechMart.Presentation.Modules.Cart.Interfaces;

public interface ICartViewModelProvider
{
    Task<CartPageViewModel> GetCartPageAsync(CancellationToken cancellationToken = default);

    Task AddAsync(int productId, int quantity, CancellationToken cancellationToken = default);

    Task IncrementAsync(int productId, CancellationToken cancellationToken = default);

    Task DecrementAsync(int productId, CancellationToken cancellationToken = default);
}
