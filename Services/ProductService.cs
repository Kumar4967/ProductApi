using ProductApi.Models;

namespace ProductApi.Services;

public class ProductService : IProductService
{
    private readonly IConfiguration _config;

    public ProductService(IConfiguration config) => _config = config;

    public Task<IEnumerable<Product>> GetFormattedProductsAsync(IEnumerable<Product> products)
    {
        var maxItems = _config.GetValue<int>("MaxItems", 100);

        var result = products
            .Where(p => p.Stock > 0)              
            .OrderBy(p => p.Name)                 
            .Take(maxItems)
            .Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name.ToUpper(),           
                Price = Math.Round(p.Price, 2),    
                Category = p.Category,
                Stock = p.Stock
            });

        return Task.FromResult(result);
    }
}