using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Threading.Tasks;
using Talabat.Core.Dtos.BasketDtos;
using Talabat.Core.Entities.BasketModule;
using Talabat.Core.Entities.OrderModule;
using Talabat.Core.Exceptions;
using Talabat.Core.Services.Contract;
using Stripe;

namespace Talabat.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly IPaymentService _paymentService;
		private readonly IConfiguration _configuration;
		private readonly ILogger<PaymentController> _logger;

		public PaymentController(IPaymentService paymentService,
			IConfiguration configuration,
			ILogger<PaymentController> logger)
		{
			_paymentService = paymentService;
			_configuration = configuration;
			_logger = logger;
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
		[HttpPost("webhook")]
		public async Task<IActionResult> StripeWebHook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
			try
			{
				var stripeEvent = EventUtility.ConstructEvent(json,
					Request.Headers["Stripe-Signature"], _configuration["StripeSettings:WhSecret"]);

				var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
				Order? order;
				// Handle the event
				switch (stripeEvent.Type)
				{
					case "payment_intent.succeeded":
						order = await _paymentService.UpdateOrderStatus(paymentIntent.Id, true);
						_logger.LogInformation("Order is succeeded {0}", order?.PaymentIntentId);
						_logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);
						break;
					case "payment_intent.payment_failed":
						order = await _paymentService.UpdateOrderStatus(paymentIntent.Id, false);
						_logger.LogInformation("Order is failed {0}", order?.PaymentIntentId);
						_logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);
						break;
				}

				return Ok();
			}
			catch (StripeException e)
			{
				_logger.LogError(e, "Webhook Error");
				return BadRequest();
			}
		}
	}
}
