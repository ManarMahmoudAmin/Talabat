using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.ProductModule;
using Talabat.Core.Shared;
using Talabat.Service;
using Talabat.Service.Specifications;

namespace Talabat.Repository.Specifications
{
    public class ProductsWithFiltersSpecifications : BaseSpecifications<Product, int>
	{
		// This constructor is used to get all products with their brands and types
		public ProductsWithFiltersSpecifications(ProductQueryParameters queryParams) :
			base(P => 
			(!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId) && 
			(!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId ) &&
			(string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
		{
			AddIncludes(P => P.Brand);	
			AddIncludes(P => P.Type);	

			switch(queryParams.SortingOption)
			{
				case SortingOptions.NameAsc:
					AddOrderBy(P => P.Name);
					break;
				case SortingOptions.NameDesc:
					AddOrderByDesc(P => P.Name);
					break;
				case SortingOptions.PriceAsc:
					AddOrderBy(P => P.Price);
					break;
				case SortingOptions.PriceDesc:
					AddOrderByDesc(P => P.Price);
					break;
				default:
					break;
			}

			ApplyPagination(queryParams.PageIndex, queryParams.PageSize);
		}

		// This constructor is used to get product by its id with its brand and its type
		public ProductsWithFiltersSpecifications(int id) : base(P => P.Id == id)
		{
			AddIncludes(P => P.Brand);
			AddIncludes(P => P.Type);
		}
	}
}
