namespace TechMart.Presentation.Modules.Cart.ViewModels;

public class CartPageViewModel
{
    public IReadOnlyList<CartLineViewModel> Lines { get; set; } = Array.Empty<CartLineViewModel>();

    public decimal Subtotal { get; set; }

    public int TotalUnits { get; set; }
}
