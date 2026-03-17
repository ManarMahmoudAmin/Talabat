using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.BasketModule;
using Talabat.Core.Entities.OrderModule;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository.Specifications;
using Product = Talabat.Core.Entities.ProductModule.Product;


namespace Talabat.Service.Services
{
	class PaymentService : IPaymentService
	{
		private readonly IConfiguration _configuration;
		private readonly IBasketRepository _basketRepo;
		private readonly IUnitOfWork _unitOfWork;

		public PaymentService( IConfiguration configuration,
			IBasketRepository basketRepo,
			IUnitOfWork unitOfWork)
		{
			_configuration = configuration;
			_basketRepo = basketRepo;
			_unitOfWork = unitOfWork;
		}
		public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BaskedId)
		{
			StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

			var basket = await _basketRepo.GetBasketAsync(BaskedId);
			if (basket is null)
				return null;

			var shippingPrice = 0m;

			if(basket.Items?.Count > 0)
			{
				foreach (var item in basket.Items)
				{
					var product = await _unitOfWork.Repository<Product, int>().GetAsync(item.Id);
					if (item.Price != product.Price)
						item.Price = product.Price;
				}
			}

			if (basket.DeliveryMethodId.HasValue)
			{
				var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);
				shippingPrice = deliveryMethod.Cost;
				basket.ShippingPrice = shippingPrice;
			}

			var totalAmount = (long)((basket.Items.Sum(item => item.Quantity * item.Price) + shippingPrice) * 100);

			PaymentIntent paymentIntent;
			PaymentIntentService paymentIntentService = new PaymentIntentService();

			if (string.IsNullOrEmpty(basket.PaymentIntentId)){
				var options = new PaymentIntentCreateOptions()
				{
					Amount = totalAmount,
					Currency = "usd",
					PaymentMethodTypes = new List<string>() { "card" }
				};
				paymentIntent = await paymentIntentService.CreateAsync(options);
				basket.PaymentIntentId = paymentIntent.Id;
				basket.ClientSecret = paymentIntent.ClientSecret;
			}
			else
			{
				var options = new PaymentIntentUpdateOptions()
				{
					Amount = totalAmount
				};
				await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
			}

			await _basketRepo.CreateOrUpdateBasketAsync(basket);
			return basket;

		}
		 public async Task<Order?> UpdateOrderStatus(string paymentIntentId, bool isPaid)
		{
			var orderRepo = _unitOfWork.Repository<Order, int>();
			var spec = new OrderWithPaymentIntentIdSpecifications(paymentIntentId);
			var order = await orderRepo.GetAsync(spec);
			if (order is null)
			{
				return null;
			}
			if (isPaid)
				order.Status = OrderStatus.PaymentReceived;
			else
				order.Status = OrderStatus.PaymentFailed;
			orderRepo.Update(order);
			await _unitOfWork.CompleteAsync();
			return order;
		}
	}
}
