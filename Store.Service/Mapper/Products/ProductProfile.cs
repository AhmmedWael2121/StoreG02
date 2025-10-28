using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Domain.Entities.Products;
using Store.Shared.DTOs.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Mapper.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration _configuration)
        {
            CreateMap<Product, ProductResponse>()
                .ForMember( x=>x.Name         ,op=>op.MapFrom(src  =>  src.Name ))
                .ForMember( x=>x.Id           ,op=>op.MapFrom(src  =>  src.Id   ))
                .ForMember( x=>x.Price        ,op=>op.MapFrom(src  =>  src.Price))
                 .ForMember( x=>x.PictureUrl  ,op=>op.MapFrom(src  =>  $"{_configuration["baseUrl"]}/{src.PictureUrl}"   ))
                //.ForMember(x => x.PictureUrl, op => op.MapFrom(src =>new ProductUrlPicResolver(_configuration)))
                .ForMember( x=>x.Description ,op=>op.MapFrom(src =>  src.Description  ))
                .ForMember( d=> d.Brand      ,op=>op.MapFrom(src => src.Brand.Name    ))
                .ForMember( d=> d.Type       ,op=>op.MapFrom(src => src.Type.Name     ));

            CreateMap<ProductBrand, BrandTypeResponse>();
            CreateMap<ProductType, BrandTypeResponse>();
        }
    }
}
