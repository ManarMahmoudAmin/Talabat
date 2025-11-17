using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Dtos.AuthDto;
using Talabat.Core.Services.Contract;

namespace Talabat.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AccountsController(IAuthService authService)
		{
			_authService = authService;
		}

		// POST: api/Accounts/Login
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
	}
}
