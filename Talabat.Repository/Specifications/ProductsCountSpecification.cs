using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Service;
using Talabat.Service.Specifications;

namespace Talabat.Repository.Specifications
{
    public class ProductsCountSpecification : BaseSpecifications<Product, int>
    {
		public ProductsCountSpecification(ProductQueryParameters queryParams) :
			base(P =>
			(!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId) &&
			(!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId) &&
			(string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
		{

		}
	}
}
