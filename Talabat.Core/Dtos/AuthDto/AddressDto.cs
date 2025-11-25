using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Dtos.AuthDto
{
    public class AddressDto
    {
		public int Id { get; set; }
		public string FistName { get; set; }
		public string LastName { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
	}
}
