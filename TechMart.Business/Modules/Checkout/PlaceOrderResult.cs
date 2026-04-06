namespace TechMart.Business.Modules.Checkout;

public class PlaceOrderResult
{
    public bool Success { get; init; }

    public string? ErrorMessage { get; init; }

    public int? OrderId { get; init; }

    public static PlaceOrderResult Ok(int orderId) =>
        new() { Success = true, OrderId = orderId };

    public static PlaceOrderResult Fail(string message) =>
        new() { Success = false, ErrorMessage = message };
}
