using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos.AuthDto;
using Talabat.Core.Entities.IdentityModule;

namespace Talabat.Core.Services.Contract
{
    public interface IAuthService
    {
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<bool> CheckEmailExists(string email);
        Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);
        Task<AddressDto> GetCurrentUserAddress(ClaimsPrincipal claimsPrincipal);
        Task<AddressDto> UpdateUserAddress(ClaimsPrincipal User, AddressDto updatedAddress);

	}
}
