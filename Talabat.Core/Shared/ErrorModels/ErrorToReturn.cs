using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Shared.ErrorModels
{
    public class ErrorToReturn
    {
		public int StatusCode { get; set; }
		public string ErrorMessage { get; set; } = default!;
	}
}
