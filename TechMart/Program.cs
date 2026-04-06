using Microsoft.EntityFrameworkCore;
using TechMart.Business.Modules.Cart;
using TechMart.Business.Modules.Cart.Interfaces;
using TechMart.Business.Modules.Checkout;
using TechMart.Business.Modules.Checkout.Interfaces;
using TechMart.Business.Modules.Orders;
using TechMart.Business.Modules.Orders.Interfaces;
using TechMart.Business.Modules.Products;
using TechMart.Business.Modules.Products.Interfaces;
using TechMart.DataAccess.Data;
using TechMart.DataAccess.Modules.Cart;
using TechMart.DataAccess.Modules.Cart.Interfaces;
using TechMart.DataAccess.Modules.Orders;
using TechMart.DataAccess.Modules.Orders.Interfaces;
using TechMart.DataAccess.Modules.Products;
using TechMart.DataAccess.Modules.Products.Interfaces;
using TechMart.Middleware;
using TechMart.Presentation.Modules.Cart;
using TechMart.Presentation.Modules.Cart.Interfaces;
using TechMart.Presentation.Modules.Checkout;
using TechMart.Presentation.Modules.Checkout.Interfaces;
using TechMart.Presentation.Modules.Orders;
using TechMart.Presentation.Modules.Orders.Interfaces;
using TechMart.Presentation.Modules.Products;
using TechMart.Presentation.Modules.Products.Interfaces;
using TechMart.Presentation.Modules.Shared.Interfaces;
using TechMart.Presentation.Modules.Shared.Services;

namespace TechMart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<TechMartDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // builder.Services.AddDbContext<TechMartDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAutoMapper(cfg => { }, typeof(ProductMappingProfile).Assembly);

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IProductViewModelProvider, ProductViewModelProvider>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddTransient<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<ICartSessionIdProvider, CartSessionIdProvider>();
            builder.Services.AddScoped<ICartViewModelProvider, CartViewModelProvider>();

            builder.Services.Configure<CheckoutOptions>(builder.Configuration.GetSection(CheckoutOptions.SectionName));
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICheckoutService, CheckoutService>();
            builder.Services.AddScoped<ICheckoutViewModelProvider, CheckoutViewModelProvider>();

            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderViewModelProvider, OrderViewModelProvider>();

            // Shared services
            builder.Services.AddScoped<IFileStorageService, FileStorageService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseSession();
            app.UseMiddleware<EnsureCartSessionMiddleware>();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
