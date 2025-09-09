using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos.BasketDtos;
using Talabat.Core.Entities.BasketModule;

namespace Talabat.Service.Mapping.Profiles
{
    public class BasketProfile : Profile
    {
		public BasketProfile()
		{
			CreateMap<CustomerBasket, BasketDto>().ReverseMap();
			CreateMap<BasketItem, BasketItemDto>().ReverseMap();
		}
	}
}
