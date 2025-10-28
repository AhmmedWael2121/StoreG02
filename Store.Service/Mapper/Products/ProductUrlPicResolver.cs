using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Domain.Entities.Products;
using Store.Shared.DTOs.ProductDto;
namespace Store.Service.Mapper.Products
{
    public class ProductUrlPicResolver(IConfiguration _configuration) : IValueResolver<Product, ProductResponse, string>
    {
        public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                // 
                return $"{_configuration["baseUrl"]}/{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
