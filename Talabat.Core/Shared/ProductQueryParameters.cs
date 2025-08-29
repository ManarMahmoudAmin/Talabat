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
		private const int DefaultSizeValue = 5;
		private const int MaxSizeValue = 10;
		public int? BrandId { get; set; }
		public int? TypeId { get; set; }
		public SortingOptions SortingOption { get; set; }
		public string? SearchValue { get; set; }
		public int PageIndex { get; set; } = 1;
		private int pageSize = DefaultSizeValue;
		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value > MaxSizeValue ? MaxSizeValue : value; }
		}
	}
}
