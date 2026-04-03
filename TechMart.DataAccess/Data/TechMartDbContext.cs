using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TechMart.Domain.Entities;

namespace TechMart.DataAccess.Data
{
    public class TechMartDbContext : DbContext
    {
        public TechMartDbContext(DbContextOptions<TechMartDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .ToTable("Product")
                .Property(p => p.Price)
                .HasPrecision(18, 4);

            modelBuilder.Entity<OrderItem>()
                .ToTable("OrderItem")
                .Property(p => p.UnitPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .ToTable("Order")
                .Property(p => p.TotalAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<CartItem>()
                .ToTable("CartItem");


            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "Macbook", Price = 4999.99m, StockQuantity = 10, ImageUrl = "https://example.com/laptop.jpg" },
                new Product { Id = 2, Name = "Smartphone", Description = "Iphone", Price = 999.99m, StockQuantity = 20, ImageUrl = "https://example.com/smartphone.jpg" },
                new Product { Id = 3, Name = "Headphones", Description = "Airpod", Price = 199.99m, StockQuantity = 15, ImageUrl = "https://example.com/headphones.jpg" },
                new Product { Id = 4, Name = "Smartwatch", Description = "Apple Watch", Price = 299.99m, StockQuantity = 12, ImageUrl = "https://example.com/smartwatch.jpg" },
                new Product { Id = 5, Name = "Tablet", Description = "Ipad", Price = 1999.99m, StockQuantity = 18, ImageUrl = "https://example.com/tablet.jpg" }
                );
        }

    }
}
