using Microsoft.EntityFrameworkCore;
using TechMart.Business.Modules.Products;
using TechMart.Business.Modules.Products.Interfaces;
using TechMart.DataAccess.Data;
using TechMart.DataAccess.Modules.Products;
using TechMart.DataAccess.Modules.Products.Interfaces;
using TechMart.Presentation.Modules.Products;
using TechMart.Presentation.Modules.Products.Interfaces;

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

            builder.Services.AddAutoMapper(cfg => { }, typeof(ProductMappingProfile).Assembly);

            builder.Services.AddScoped<IProductViewModelProvider, ProductViewModelProvider>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddTransient<IProductRepository, ProductRepository>();


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

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
