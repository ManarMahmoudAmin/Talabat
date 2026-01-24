using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderModule;

namespace Talabat.Core.Services.Contract
{
    public interface IOrderService
    {
		Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress);
		Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail);

	}
}
