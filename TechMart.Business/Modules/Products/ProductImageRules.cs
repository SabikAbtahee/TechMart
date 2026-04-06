namespace TechMart.Business.Modules.Products
{
    public static class ProductImageRules
    {
        public static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png"];

        public static string FileInputAccept => string.Join(",", AllowedExtensions);
    }
}
