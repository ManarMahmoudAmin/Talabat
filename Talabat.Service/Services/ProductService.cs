using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Data.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Shared;
using Talabat.Repository.Specifications;

namespace Talabat.Service.Services
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductService(IUnitOfWork unitOfWork, IMapper mapper )
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParameters queryParams)
		{
			var repo = _unitOfWork.Repository<Product, int>();
			var spec = new ProductsWithFiltersSpecifications(queryParams);
			var products = await repo.GetAllAsync(spec);
			var mappedProducts = _mapper.Map<IEnumerable<ProductDto>>( products);
			var productCount = mappedProducts.Count();
			var totalCount = await repo.CountAsync(new ProductsCountSpecification(queryParams));
			return new PaginatedResult<ProductDto>(queryParams.PageIndex, productCount, totalCount, mappedProducts);
		}

		public async Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync()
		{
			var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
			var mappedBrands = _mapper.Map<IEnumerable<BrandTypeDto>>(brands);
			
			return mappedBrands;
		}
			
		public async Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync()
		{
			var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
			var mappedTypes = _mapper.Map<IEnumerable<BrandTypeDto>>(types);
			
			return mappedTypes;
		}

		public async Task<ProductDto> GetProductByIdAsync(int id)
		{
			var spec = new ProductsWithFiltersSpecifications(id);
			var product = await _unitOfWork.Repository<Product, int>().GetAsync(spec);
			var mappedProduct = _mapper.Map<ProductDto>(product);
	
			return mappedProduct;
		}
	}
}
