using AutoMapper;
using Store.Domain.Contracts;
using Store.Service.Abstractions;
using Store.Service.Abstractions.Products;
using Store.Service.Products;
namespace Store.Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceManager
    {
        // Initialize ProductService with the provided UnitOfWork and Mapper
        // using the concrete implementation from Store.Service.Products namespace 
        // as shown in the related code snippets. 
        // This allows ServiceManager to provide access to product-related services.
        public  IProductService ProductService { get; } =  new ProductService(_unitOfWork, _mapper);
    }
}
