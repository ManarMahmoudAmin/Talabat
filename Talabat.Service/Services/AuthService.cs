using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.API.Extensions;
using Talabat.Core.Dtos.AuthDto;
using Talabat.Core.Entities.IdentityModule;
using Talabat.Core.Exceptions;
using Talabat.Core.Services.Contract;

namespace Talabat.Service.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;
		private readonly IMapper _mapper;

		public AuthService(UserManager<AppUser> userManager,
			SignInManager<AppUser> signInManager,
			ITokenService tokenService,
			IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
			_mapper = mapper;
		}
		public async Task<UserDto> LoginAsync(LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email);
			if (user is null)
				throw new UnauthorizedException("Invalid Login.");
			
			var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

			if (result.IsLockedOut)
				throw new UnauthorizedException("Your account is locked. Please try again later.");

			if (result.IsNotAllowed)
				throw new UnauthorizedException("Your account is not confirmed yet.");

			if (!result.Succeeded)
				throw new UnauthorizedException("Invalid Login.");
			else
				return new UserDto()
				{
					Email = user.Email,
					DisplayName = user.DisplayName,
					Token = await _tokenService.CreateTokenAsync(user, _userManager)
				};
		}

		public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
		{
			if (await CheckEmailExists(registerDto.Email))
				return null;
			var user = new AppUser()
			{
				Email = registerDto.Email,
				DisplayName = registerDto.DisplayName,
				UserName = registerDto.Email.Split('@')[0],
				PhoneNumber = registerDto.PhoneNumber
			};
			var result = await _userManager.CreateAsync(user, registerDto.Password);

			if (!result.Succeeded)
			{
				var errors = String.Join(", ", result.Errors.Select(e => e.Description));
				throw new BadRequestException($"Registration failed: {errors}");
			}

			else
				return new UserDto()
				{
					Email = user.Email,
					DisplayName = user.DisplayName,
					Token = await _tokenService.CreateTokenAsync(user, _userManager)
				};
		}
		public async Task<bool> CheckEmailExists(string email)
			=> await _userManager.FindByEmailAsync(email) is not null;

		public async Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal)
		{
			var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);	
			var user = await _userManager.FindByEmailAsync(email!);

			return new UserDto()
			{
				Email = user.Email,
				DisplayName = user.DisplayName,
				Token = await _tokenService.CreateTokenAsync(user, _userManager)
			};
		}

		public async Task<AddressDto> GetCurrentUserAddress(ClaimsPrincipal claimsPrincipal)
		{
			//var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
			//var user = await _userManager.FindByEmailAsync(email!);
			var user = await _userManager.FindUserWithAddressAsync(claimsPrincipal);
			var mappedAddress = _mapper.Map<AddressDto>(user.Address);
			return mappedAddress;
		}

		public async Task<AddressDto> UpdateUserAddress(ClaimsPrincipal User, AddressDto updatedAddress)
		{
			var user = await _userManager.FindUserWithAddressAsync(User);
			var mappedAddress = _mapper.Map<Address>(updatedAddress);
			mappedAddress.Id = user.Address.Id;
			user.Address = mappedAddress;
			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded)
				throw new BadRequestException("Address is not found");

			return updatedAddress;

		}

	}
}
