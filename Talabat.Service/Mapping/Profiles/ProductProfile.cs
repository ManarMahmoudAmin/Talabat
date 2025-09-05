using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Data.Dtos;
using Talabat.Core.Entities.ProductModule;
using Talabat.Service.Mapping.Resolvers;

namespace Talabat.Service.Mapping.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() {
            CreateMap<Product, ProductDto>().
                ForMember(dest => dest.BrandName, op => op.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.PictureUrl, op => op.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductBrand, BrandTypeDto>();
            
            CreateMap<ProductType, BrandTypeDto>();
		}

    }
}
