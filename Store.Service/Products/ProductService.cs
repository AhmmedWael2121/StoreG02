using AutoMapper;
using Store.Domain.Contracts;
using Store.Domain.Entities.Products;
using Store.Service.Abstractions.Products;
using Store.Service.Specifications.Products;
using Store.Shared.DTOs;
using Store.Shared.DTOs.ProductDto;
namespace Store.Service.Products
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<PaginationResponse<ProductResponse>> GetAllProductAsync(ProductQueryParams productQuery)
        {
            //var spec = new BaseSpecification<int , Product >( null ) ;
            //spec.IncludesExp.Add(p => p.Brand)   ;
            //spec.IncludesExp.Add(p => p.Type)    ;

            var spec = new ProductWithBrandAndTypeSpecification(productQuery);
            
            var products = await _unitOfWork.GetRepository<int, Product>().GetAllAsync(spec);
            
            var res = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return new PaginationResponse<ProductResponse>(
                pageNumber: productQuery.PageNumber,pageSize: productQuery.PageSize,0,res
                );
        }
        public async Task<ProductResponse> GetProductById(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecification(id);
            var products = await _unitOfWork.GetRepository<int, Product>().GetAsync(spec);
            var res = _mapper.Map<ProductResponse>(products);
            return res;
        }
        public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandAsync()
        {
            var brands = await _unitOfWork.GetRepository<int, ProductBrand>().GetAllAsync();
            var res = _mapper.Map<IEnumerable<BrandTypeResponse>>(brands);
            return res;
        }
        public async Task<IEnumerable<BrandTypeResponse>> GetAllTypeAsync()
        {
            var products = await _unitOfWork.GetRepository<int, ProductType>().GetAllAsync();
            var res = _mapper.Map<IEnumerable<BrandTypeResponse>>(products);
            return res;
        }

    }
}
