using PetShop.Core.Entities;

namespace PetShop.Core.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> SearchProductsAsync(string? name, string? category, decimal? minPrice, decimal? maxPrice);
    }
}
