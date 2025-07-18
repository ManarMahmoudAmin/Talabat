using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository.Data.Dtos;

namespace Talabat.Service.Services
{
	class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductService(IUnitOfWork unitOfWork, IMapper mapper )
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
		{
			var products = await _unitOfWork.Repository<Product, int>().GetAllAsync();
			var mappedProducts = _mapper.Map<IEnumerable<ProductDto>>( products);
			
			return mappedProducts;
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
			var product = await _unitOfWork.Repository<Product, int>().GetAsync(id);
			var mappedProduct = _mapper.Map<ProductDto>(product);
	
			return mappedProduct;
		}
	}
}
