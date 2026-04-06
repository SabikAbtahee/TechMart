using TechMart.Presentation.Modules.Orders.ViewModels;

namespace TechMart.Presentation.Modules.Orders.Interfaces;

public interface IOrderViewModelProvider
{
    Task<OrderIndexViewModel> GetIndexAsync(CancellationToken cancellationToken = default);

    Task<OrderDetailsViewModel?> GetDetailsAsync(int orderId, CancellationToken cancellationToken = default);
}
