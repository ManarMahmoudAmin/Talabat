using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.API;
using Talabat.Core.Entities;
using Talabat.Repository.Data.Dtos;

namespace Talabat.Core.Services.Contract
{
    public interface IProductService 
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? brandId, int? typeId, SortingOptions sortingOption);
        Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
	}

}
