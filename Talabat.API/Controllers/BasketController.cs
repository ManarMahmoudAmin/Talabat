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
		public async Task<ActionResult<BasketDto>> GetBasket(string id)
		{
			var basket = await _basketService.GetBasketAsync(id);
			if (basket == null) return Ok(new BasketDto { Id = id });
			return Ok(basket);
		}

		[HttpPost]
		// POST: api/Basket
		public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
		{
			var Basket = await _basketService.CreateOrUpdateBasketAsync(basket);
			return Ok(Basket);
		}

		[HttpDelete]
		// DELETE: api/Basket
		public async Task<ActionResult<bool>> DeleteBasketAsync(string id)
		{
			var Result = await _basketService.DeleteBasketAsync(id);
			return Ok(Result);
		}

	}
}
