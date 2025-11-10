using PetShop.Core.DTOs;

namespace PetShop.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(Guid id);
        Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
        Task<ProductDto?> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto);
        Task<bool> DeleteProductAsync(Guid id);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string? name, string? category, decimal? minPrice, decimal? maxPrice);
    }
}
