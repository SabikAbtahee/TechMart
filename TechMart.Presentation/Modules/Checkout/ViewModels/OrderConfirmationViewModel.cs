namespace TechMart.Presentation.Modules.Checkout.ViewModels;

public class OrderConfirmationViewModel
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal SubTotal { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal ShippingAmount { get; set; }

    public decimal TotalAmount { get; set; }

    public IReadOnlyList<OrderConfirmationLineViewModel> Lines { get; set; } = Array.Empty<OrderConfirmationLineViewModel>();
}

public class OrderConfirmationLineViewModel
{
    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal LineTotal => UnitPrice * Quantity;
}
