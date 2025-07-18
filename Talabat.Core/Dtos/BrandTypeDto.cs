using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Repository.Data.Dtos
{
    public class BrandTypeDto
    {
		public int Id { get; set; }
		public DateTime CreateAt { get; set; } = DateTime.UtcNow;
	}
}
