namespace TechMart.Presentation.Modules.Orders.ViewModels;

public class OrderIndexViewModel
{
    public IReadOnlyList<OrderSummaryRowViewModel> Orders { get; set; } = Array.Empty<OrderSummaryRowViewModel>();
}

public class OrderSummaryRowViewModel
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = string.Empty;
}
