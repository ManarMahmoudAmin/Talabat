using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos.AuthDto;
using Talabat.Core.Entities.IdentityModule;

namespace Talabat.Service.Mapping.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>();
        }
    }
}
