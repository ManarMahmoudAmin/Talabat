using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderModule;
using Talabat.Service.Specifications;

namespace Talabat.Repository.Specifications
{
	public class OrderSpecifications : BaseSpecifications<Order, int>
	{
		public OrderSpecifications(string email) : base(O => O.BuyerEmail == email)
		{
			Includes.Add(O => O.DeliveryMethod);
			Includes.Add(O => O.Items);
			AddOrderByDesc(O => O.OrderDate);
		}
	}
}
