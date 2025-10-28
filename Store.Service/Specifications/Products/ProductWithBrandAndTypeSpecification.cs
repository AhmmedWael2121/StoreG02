using Store.Domain.Entities.Products;
using Store.Shared.DTOs.ProductDto;
namespace Store.Service.Specifications.Products
{
    public class ProductWithBrandAndTypeSpecification: BaseSpecification<int,Product>
    {
        public ProductWithBrandAndTypeSpecification(int id):base(p=>p.Id == id){ApplyIncludes();}
        public ProductWithBrandAndTypeSpecification() : base(null){ApplyIncludes();}
        public ProductWithBrandAndTypeSpecification(ProductQueryParams productQuery) : base
            (op=>
            (!productQuery.BrandId.HasValue ||op.BrandId == productQuery.BrandId)
            && (!productQuery.TypeId.HasValue || op.TypeId == productQuery.TypeId)
            && ( string.IsNullOrEmpty(productQuery.Search) ||op.Name.ToLower().Contains(productQuery.Search.ToLower() ) 
            ))
        {
            if (!string.IsNullOrEmpty(productQuery.Sort))
            {
                switch (productQuery.Sort.ToLower())
                {
                    case "priceasc":
                        OrderBy = p => p.Price;
                        break;
                    case "pricedesc":
                        OrderByDesc = p => p.Price;
                        break;
                    default:
                        OrderBy = p => p.Name;
                        break;
                }
            }
                ApplyIncludes();
            Pagination(productQuery.PageNumber , productQuery.PageSize);
        }
        private void ApplyIncludes()
        {
            IncludesExp.Add(b => b.Brand);
            IncludesExp.Add(b => b.Type);

        }
    }
}
