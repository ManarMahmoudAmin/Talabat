using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Dtos.AuthDto;
using Talabat.Core.Entities.IdentityModule;
using Talabat.Core.Services.Contract;
using Talabat.Core.Shared.ErrorModels;

namespace Talabat.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AccountController(IAuthService authService)
		{
			_authService = authService;
		}

		// POST: api/Account/Login
		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var result = await _authService.LoginAsync(model);
			return Ok(result);
		}

		// POST: api/Accounts/Register
		[HttpPost("Register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			var result = await _authService.RegisterAsync(model);
			if (result is null)
			{
				return BadRequest(new ErrorToReturn()
				{
					StatusCode = 400, ErrorMessage = $"{model.Email} is already in use"
				});
			}
			return Ok(result);
		}

		//GET : api/Accounts/GetCurrentUser
		[Authorize]
		[HttpGet("GetCurrentUser")]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var result = await _authService.GetCurrentUser(User);
			return Ok(result);

		}

		//GET : api/Accounts/CurrentUserAddress
		[Authorize]
		[HttpGet("address")]
		public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
		{
			var result = await _authService.GetCurrentUserAddress(User);
			return Ok(result);
		}

		//PUT : api/Account/Address
		[Authorize]
		[HttpPut("Address")]
		public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto updatedAddress)
		{
			var result = await _authService.UpdateUserAddress(User, updatedAddress);
			return Ok(result);
		}

		//GET : api/Account/EmailExists
		[HttpGet("EmailExists")]
		public async Task<ActionResult<bool>> CheckEmailExists(string email)
		{
			return await _authService.CheckEmailExists(email);
		}
	}
}
