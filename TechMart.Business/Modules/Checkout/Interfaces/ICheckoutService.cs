using TechMart.Business.Modules.Checkout;

namespace TechMart.Business.Modules.Checkout.Interfaces;

public interface ICheckoutService
{
    Task<PlaceOrderResult> PlaceOrderAsync(
        string cartSessionId,
        string customerName,
        string phone,
        string email,
        string shippingAddress,
        CancellationToken cancellationToken = default);

    Task<OrderConfirmationDetails?> GetOrderForConfirmationAsync(
        int orderId,
        string cartSessionId,
        CancellationToken cancellationToken = default);
}
