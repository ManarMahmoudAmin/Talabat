using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.BasketModule;
using Talabat.Core.Entities.OrderModule;

namespace Talabat.Core.Services.Contract
{
	public interface IPaymentService
	{
		public Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BaskedId);
		public Task<Order?> UpdateOrderStatus(string paymentIntentId, bool isPaid);
	}
}
