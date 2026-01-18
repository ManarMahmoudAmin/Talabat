using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos.AuthDto;

namespace Talabat.Core.Dtos.OrderDtos
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }
		[Required]
		public int DeliveryMethodId { get; set; }
		[Required]
		public AddressDto ShippingAddress { get; set; }
    }
}
