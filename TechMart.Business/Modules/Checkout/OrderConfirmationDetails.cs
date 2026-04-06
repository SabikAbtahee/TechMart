namespace TechMart.Business.Modules.Checkout;

public class OrderConfirmationDetails
{
    public int OrderId { get; init; }

    public DateTime OrderDate { get; init; }

    public decimal SubTotal { get; init; }

    public decimal TaxAmount { get; init; }

    public decimal ShippingAmount { get; init; }

    public decimal TotalAmount { get; init; }

    public IReadOnlyList<OrderLineDetails> Lines { get; init; } = Array.Empty<OrderLineDetails>();
}

public class OrderLineDetails
{
    public string ProductName { get; init; } = string.Empty;

    public int Quantity { get; init; }

    public decimal UnitPrice { get; init; }
}
