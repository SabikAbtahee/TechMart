namespace TechMart.DataAccess.Modules.Orders;

public class OrderPlaceRequest
{
    public string CartSessionId { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string ShippingAddress { get; set; } = string.Empty;

    public decimal TaxRatePercent { get; set; }

    public decimal ShippingFee { get; set; }
}
