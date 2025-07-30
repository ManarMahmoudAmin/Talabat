using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Shared;

namespace Talabat.Service
{
    public class ProductQueryParameters
    {
		public int? brandId { get; set; }
		public int? typeId { get; set; }
		public SortingOptions sortingOption { get; set; }
		public string? searchValue { get; set; }
	}
}
