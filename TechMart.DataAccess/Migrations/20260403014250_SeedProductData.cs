using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechMart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "Macbook", "https://example.com/laptop.jpg", "Laptop", 4999.99m, 10 },
                    { 2, "Iphone", "https://example.com/smartphone.jpg", "Smartphone", 999.99m, 20 },
                    { 3, "Airpod", "https://example.com/headphones.jpg", "Headphones", 199.99m, 15 },
                    { 4, "Apple Watch", "https://example.com/smartwatch.jpg", "Smartwatch", 299.99m, 12 },
                    { 5, "Ipad", "https://example.com/tablet.jpg", "Tablet", 1999.99m, 18 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
