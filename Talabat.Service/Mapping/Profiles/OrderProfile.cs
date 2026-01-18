using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos.AuthDto;
using Talabat.Core.Dtos.OrderDtos;
using Talabat.Core.Entities.OrderModule;

namespace Talabat.Service.Mapping.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, Address>();
        }
    }
}
