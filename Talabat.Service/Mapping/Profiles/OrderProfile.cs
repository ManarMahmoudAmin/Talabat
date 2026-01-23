using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos.AuthDto;
using Talabat.Core.Dtos.OrderDtos;
using Talabat.Core.Entities.OrderModule;
using Talabat.Service.Mapping.Resolvers;

namespace Talabat.Service.Mapping.Profiles
{
	class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<AddressDto, Address>();
			CreateMap<Order, OrderToReturnDto>()
				.ForMember(dest => dest.DeliveryMethod, op => op.MapFrom(src => src.DeliveryMethod.ShortName))
				.ForMember(dest => dest.DeliveryMethodCost, op => op.MapFrom(src => src.DeliveryMethod.Cost));

			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(dest => dest.ProductId, op => op.MapFrom(src => src.Product.ProductId))
				.ForMember(dest => dest.ProductName, op => op.MapFrom(src => src.Product.ProductName))
				.ForMember(dest => dest.PictureUrl, op => op.MapFrom(src => src.Product.PictureUrl))
				.ForMember(dest => dest.PictureUrl, op => op.MapFrom<OrderItemPictureUrlResolver>());
		}
	}
}