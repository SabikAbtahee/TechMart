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
                .Property(p => p.SubTotal)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .Property(p => p.TaxAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .Property(p => p.ShippingAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .Property(p => p.TotalAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.CartSessionId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartItem>()
                .ToTable("CartItem")
                .HasIndex(c => new { c.CartSessionId, c.ProductId })
                .IsUnique();

            modelBuilder.Entity<CartItem>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            var seedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified);
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "NovaBook Pro 16\"", Description = "Thin aluminum ultrabook with all-day battery, 32GB RAM, and a vivid 16-inch display for work and creative apps.", Price = 1899.99m, StockQuantity = 24, ImageUrl = "https://picsum.photos/seed/techmart-novabook/640/400", CreatedDate = seedDate, UpdatedDate = seedDate },
                new Product { Id = 2, Name = "PulsePhone X", Description = "Flagship smartphone with OLED 120Hz screen, 256GB storage, and a triple-lens camera for low-light photos.", Price = 899.99m, StockQuantity = 48, ImageUrl = "https://picsum.photos/seed/techmart-pulsephone/640/400", CreatedDate = seedDate, UpdatedDate = seedDate },
                new Product { Id = 3, Name = "SonicBuds Elite", Description = "Wireless earbuds with hybrid active noise cancellation, transparency mode, and up to 30 hours with the charging case.", Price = 169.99m, StockQuantity = 95, ImageUrl = "https://picsum.photos/seed/techmart-sonicbuds/640/400", CreatedDate = seedDate, UpdatedDate = seedDate },
                new Product { Id = 4, Name = "Atlas Watch Ultra", Description = "GPS smartwatch with heart-rate and SpO2 sensors, rugged case, and week-long battery in smart mode.", Price = 399.99m, StockQuantity = 32, ImageUrl = "https://picsum.photos/seed/techmart-atlaswatch/640/400", CreatedDate = seedDate, UpdatedDate = seedDate },
                new Product { Id = 5, Name = "SlateTab Air 11\"", Description = "Lightweight 11-inch tablet with laminated display, optional stylus support, and stereo speakers for streaming.", Price = 579.99m, StockQuantity = 41, ImageUrl = "https://picsum.photos/seed/techmart-slatetab/640/400", CreatedDate = seedDate, UpdatedDate = seedDate },
                new Product { Id = 6, Name = "VoltCharge GaN 100W", Description = "Compact GaN wall charger with three USB-C ports, intelligent power sharing, and foldable prongs for travel.", Price = 69.99m, StockQuantity = 140, ImageUrl = "https://picsum.photos/seed/techmart-voltcharge/640/400", CreatedDate = seedDate, UpdatedDate = seedDate },
                new Product { Id = 7, Name = "Clarity 27\" 4K Monitor", Description = "27-inch 4K IPS monitor with HDR, slim bezels, and USB-C docking—ideal for design and hybrid office setups.", Price = 419.99m, StockQuantity = 18, ImageUrl = "https://picsum.photos/seed/techmart-clarity27/640/400", CreatedDate = seedDate, UpdatedDate = seedDate },
                new Product { Id = 8, Name = "MechType Pro MK85", Description = "Hot-swappable mechanical keyboard with tactile switches, per-key RGB, and doubleshot PBT keycaps.", Price = 139.99m, StockQuantity = 67, ImageUrl = "https://picsum.photos/seed/techmart-mechtype/640/400", CreatedDate = seedDate, UpdatedDate = seedDate },
                new Product { Id = 9, Name = "GlideMouse Ergo+", Description = "Ergonomic vertical wireless mouse with silent clicks, multi-device pairing, and a comfortable rubber grip.", Price = 54.99m, StockQuantity = 88, ImageUrl = null, CreatedDate = seedDate, UpdatedDate = seedDate },
                new Product { Id = 10, Name = "VaultDrive Portable 2TB", Description = "Pocket-size USB-C SSD rated for fast transfers, shock resistance, and hardware encryption for sensitive files.", Price = 209.99m, StockQuantity = 36, ImageUrl = null, CreatedDate = seedDate, UpdatedDate = seedDate }
                );
        }

    }
}
