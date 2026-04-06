namespace TechMart.Presentation.Modules.Cart.ViewModels;

public class CartLineViewModel
{
    public int CartItemId { get; set; }

    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal LineSubtotal => UnitPrice * Quantity;
}
