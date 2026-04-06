namespace TechMart.Presentation.Modules.Orders.ViewModels;

public class OrderDetailsViewModel
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string ShippingAddress { get; set; } = string.Empty;

    public decimal SubTotal { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal ShippingAmount { get; set; }

    public decimal TotalAmount { get; set; }

    public IReadOnlyList<OrderLineRowViewModel> Lines { get; set; } = Array.Empty<OrderLineRowViewModel>();
}

public class OrderLineRowViewModel
{
    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal LineTotal => UnitPrice * Quantity;
}
