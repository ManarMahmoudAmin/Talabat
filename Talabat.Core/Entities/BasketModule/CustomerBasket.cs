using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.BasketModule
{
    public class CustomerBasket
    {
		public string Id { get; set; } = default!; // GUID : Created by Client
		public ICollection<BasketItem> Items { get; set; } = [];
	}
}
