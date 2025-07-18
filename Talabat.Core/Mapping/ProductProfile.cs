using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Repository.Data.Dtos;

namespace Talabat.Core.Mapping
{
    class ProductProfile : Profile
    {
        public ProductProfile() {
            CreateMap<Product, ProductDto>().
                ForMember(d => d.BrandName, 
                option => option.MapFrom(s => s.Brand.Name));

            CreateMap<ProductBrand, BrandTypeDto>();
            
            CreateMap<ProductType, BrandTypeDto>();
		}

    }
}
