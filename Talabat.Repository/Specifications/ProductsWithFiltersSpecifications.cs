using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Service.Specifications;

namespace Talabat.Repository.Specifications
{
    public class ProductsWithFiltersSpecifications : BaseSpecifications<Product, int>
	{
		// This constructor is used to get all products with their brands and types
		public ProductsWithFiltersSpecifications()
		{
			AddIncludes(P => P.Brand);	
			AddIncludes(P => P.Type);	
		}

		// This constructor is used to get product by its id with its brand and its type
		public ProductsWithFiltersSpecifications(int id) : base(P => P.Id == id)
		{
			AddIncludes(P => P.Brand);
			AddIncludes(P => P.Type);
		}
	}
}
