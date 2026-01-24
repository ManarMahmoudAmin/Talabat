using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.BasketModule;
using Talabat.Core.Entities.OrderModule;
using Talabat.Core.Entities.ProductModule;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository.Specifications;

namespace Talabat.Service.Services
{
	class OrderService : IOrderService
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IUnitOfWork _unitOfWork;

		public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
		{
			_basketRepository = basketRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
		{
			var Basket = await _basketRepository.GetBasketAsync(BasketId);

			var OrderItems = new List<OrderItem>();

			if (Basket?.Items.Count > 0)
			{
				foreach (var item in Basket.Items)
				{
					var product = await _unitOfWork.Repository<Product, int>().GetAsync(item.Id);
					var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
					var orderItem = new OrderItem(productItemOrdered, item.Quantity, product.Price);
					OrderItems.Add(orderItem);
				}
			}

			var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(DeliveryMethodId);

			var SubTotal = OrderItems.Sum(item => item.Quantity * item.Price);

			var Order = new Order(BuyerEmail, ShippingAddress, DeliveryMethod, OrderItems, SubTotal);

			await _unitOfWork.Repository<Order, int>().AddAsync(Order);

			var Result = await _unitOfWork.CompleteAsync();
			if (Result <= 0)
				return null;
			return Order;
		}

		public async Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail)
		{
			var spec = new OrderSpecifications(BuyerEmail);
			var Orders = await _unitOfWork.Repository<Order, int>().GetAllAsync(spec);
			return Orders;
		}

		public async Task<Order> GetOrdersForSpecificUserByIdAsync(string BuyerEmail, int OrderId)
		{
			var spec = new OrderSpecifications(BuyerEmail, OrderId);
			var Order = await _unitOfWork.Repository<Order, int>().GetAsync(spec);
			return Order;
		}
		public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
		{
			var DeliveryMethods = await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();
			return DeliveryMethods;
		}
	}
}
