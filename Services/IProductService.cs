using ProductApi.Models;

namespace ProductApi.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetFormattedProductsAsync(IEnumerable<Product> products);
}