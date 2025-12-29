using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderModule
{
    public class OrderItem : BaseEntity<int>
    {
		public OrderItem()
		{
			
		}
		public OrderItem(ProductItemOrdered product, int qauntity, decimal price)
		{
			Product = product;
			Qauntity = qauntity;
			Price = price;
		}

		public ProductItemOrdered Product { get; set; }
		public int Qauntity { get; set; }
		public decimal Price { get; set; }

	}
}
