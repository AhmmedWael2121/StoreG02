using Store.Shared.DTOs;
using Store.Shared.DTOs.ProductDto;

namespace Store.Service.Abstractions.Products
{
    public interface IProductService
    { 
     public Task<PaginationResponse<ProductResponse>> GetAllProductAsync(ProductQueryParams productQuery);
     public Task<ProductResponse> GetProductById(int id);
     public Task<IEnumerable<BrandTypeResponse>> GetAllBrandAsync();
     public Task<IEnumerable<BrandTypeResponse>> GetAllTypeAsync();

    }
}
