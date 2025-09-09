using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Talabat.Core.Dtos.BasketDtos;
using Talabat.Core.Services.Contract;

namespace Talabat.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IBasketService _basketService;

		public BasketController(IBasketService basketService)
		{
			_basketService = basketService;
		}

		[HttpGet]
		// GET: api/Basket?Key
		public async Task<ActionResult<BasketDto>> GetBasket(string Key)
		{
			var basket = await _basketService.GetBasketAsync(Key);
			return Ok(basket);
		}

		[HttpPost]
		// POST: api/Basket
		public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
		{
			var Basket = await _basketService.CreateOrUpdateBasketAsync(basket);
			return Ok(Basket);
		}

		[HttpDelete("{Key}")]
		// DELETE: api/Basket
		public async Task<ActionResult<bool>> DeleteBasketAsync(string Key)
		{
			var Result = await _basketService.DeleteBasketAsync(Key);
			return Ok(Result);
		}

	}
}
