using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.API;
using Talabat.Core.Entities;
using Talabat.Service.Specifications;

namespace Talabat.Repository.Specifications
{
    public class ProductsWithFiltersSpecifications : BaseSpecifications<Product, int>
	{
		// This constructor is used to get all products with their brands and types
		public ProductsWithFiltersSpecifications(int? brandId, int? typeId, SortingOptions sortingOption) :
			base(P => (!brandId.HasValue || P.BrandId == brandId) && 
			           (!typeId.HasValue || P.TypeId == typeId))
		{
			AddIncludes(P => P.Brand);	
			AddIncludes(P => P.Type);	

			switch(sortingOption)
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
