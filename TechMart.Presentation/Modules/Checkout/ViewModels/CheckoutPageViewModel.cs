using System.ComponentModel.DataAnnotations;
using TechMart.Presentation.Modules.Cart.ViewModels;

namespace TechMart.Presentation.Modules.Checkout.ViewModels;

public class CheckoutPageViewModel
{
    public IReadOnlyList<CartLineViewModel> Lines { get; set; } = Array.Empty<CartLineViewModel>();

    public decimal Subtotal { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal ShippingAmount { get; set; }

    public decimal Total { get; set; }

    [Required]
    [Display(Name = "Full name")]
    [StringLength(200)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Phone")]
    [StringLength(50)]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    [StringLength(256)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Shipping address")]
    [StringLength(500)]
    public string ShippingAddress { get; set; } = string.Empty;
}
