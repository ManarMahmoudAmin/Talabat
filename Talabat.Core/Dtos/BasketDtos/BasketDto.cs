using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Dtos.BasketDtos
{
    public class BasketDto
    {
		public string Id { get; set; } = default!;
		public ICollection<BasketItemDto> Items { get; set; } = [];
	}
}
