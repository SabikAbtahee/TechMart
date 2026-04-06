using TechMart.Presentation.Modules.Checkout.ViewModels;

namespace TechMart.Presentation.Modules.Checkout.Interfaces;

public interface ICheckoutViewModelProvider
{
    Task<CheckoutPageViewModel?> GetCheckoutPageAsync(CancellationToken cancellationToken = default);

    Task<OrderConfirmationViewModel?> GetConfirmationAsync(int orderId, CancellationToken cancellationToken = default);
}
