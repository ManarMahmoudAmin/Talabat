using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Dtos.BasketDtos
{
    public class BasketDto
    {
		[Required]
		public string Id { get; set; } = default!;
		public ICollection<BasketItemDto> Items { get; set; } = [];
		public string? PaymentIntentId { get; set; }
		public string? ClientSecret { get; set; }
		public int? DeliveryMethodId { get; set; }
		public decimal ShippingPrice { get; set; }
	}
}
