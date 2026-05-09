using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(config.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.MapGet("/api/products", async (AppDbContext db, IProductService productService) =>
{
    var products = await db.Products.ToListAsync();
    var formatted = await productService.GetFormattedProductsAsync(products);

    return Results.Json(new
    {
        appName = config["AppName"],
        version = config["Version"],
        products = formatted
    });
});

app.MapGet("/api/config", (IConfiguration config) =>
    Results.Json(new
    {
        appName = config["AppName"],
        version = config["Version"],
        maxItems = config.GetValue<int>("MaxItems")
    }));

app.Run();