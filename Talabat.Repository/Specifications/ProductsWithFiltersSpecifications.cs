using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Shared;
using Talabat.Service;
using Talabat.Service.Specifications;

namespace Talabat.Repository.Specifications
{
    public class ProductsWithFiltersSpecifications : BaseSpecifications<Product, int>
	{
		// This constructor is used to get all products with their brands and types
		public ProductsWithFiltersSpecifications(ProductQueryParameters queryParams) :
			base(P => (!queryParams.brandId.HasValue || P.BrandId == queryParams.brandId) && 
			           (!queryParams.typeId.HasValue || P.TypeId ==	queryParams.typeId ))
		{
			AddIncludes(P => P.Brand);	
			AddIncludes(P => P.Type);	

			switch(queryParams.sortingOption)
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
		}

		// This constructor is used to get product by its id with its brand and its type
		public ProductsWithFiltersSpecifications(int id) : base(P => P.Id == id)
		{
			AddIncludes(P => P.Brand);
			AddIncludes(P => P.Type);
		}
	}
}
