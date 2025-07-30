using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Repository.Data.Dtos;
using Talabat.Service;

namespace Talabat.Core.Services.Contract
{
    public interface IProductService 
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProductQueryParameters queryParams);
        Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
	}

}
