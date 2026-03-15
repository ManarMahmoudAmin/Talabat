using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderModule;
using Talabat.Service.Specifications;

namespace Talabat.Repository.Specifications
{
    public class OrderWithPaymentIntentIdSpecifications : BaseSpecifications<Order, int>
    {
		public OrderWithPaymentIntentIdSpecifications(string paymentIntentId):
			base( O => O.PaymentIntentId == paymentIntentId)
		{
			
		}
	}
}
