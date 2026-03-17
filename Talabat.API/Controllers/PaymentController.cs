using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.Core.Dtos.BasketDtos;
using Talabat.Core.Entities.BasketModule;
using Talabat.Core.Exceptions;
using Talabat.Core.Services.Contract;

namespace Talabat.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly IPaymentService _paymentService;

		public PaymentController(IPaymentService paymentService)
		{
			_paymentService = paymentService;
		}

		[Authorize]
		[HttpPost("{basketId}")]
		public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymen(string basketId)
		{
			var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

			if (basket is null)
				throw new BadRequestException("There is a problem with your basket");
			return Ok(basket);

		}
	}
}
