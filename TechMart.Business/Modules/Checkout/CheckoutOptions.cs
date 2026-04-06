namespace TechMart.Business.Modules.Checkout;

public class CheckoutOptions
{
    public const string SectionName = "Checkout";

    public decimal TaxRatePercent { get; set; }

    public decimal ShippingFee { get; set; }
}
