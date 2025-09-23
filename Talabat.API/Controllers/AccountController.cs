using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Dtos.AuthDto;
using Talabat.Core.Services.Contract;

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

		// POST: api/Account/Register
		[HttpPost("Register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			var result = await _authService.RegisterAsync(model);
			return Ok(result);	
		}
	}
}
