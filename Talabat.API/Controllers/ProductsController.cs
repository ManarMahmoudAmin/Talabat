using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.Core.Services.Contract;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProducts(int? brandId, int? typeId, SortingOptions sortingOption)
		{
			var result = await _productService.GetAllProductsAsync(brandId, typeId, sortingOption);
			return Ok(result);
		}

		[HttpGet("brands")]
		public async Task<IActionResult> GetAllBrands()
		{
			var result = await _productService.GetAllBrandsAsync();
			return Ok(result);
		}

		[HttpGet("types")]
		public async Task<IActionResult> GetAllTypes()
		{
			var result = await _productService.GetAllTypesAsync();
			return Ok(result);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> getProduct(int? id) {
			if(id is null)
				return BadRequest("Invalid Product Id");

			var result = await _productService.GetProductByIdAsync(id.Value);

			if (result is null)
				return NotFound($"Product with Id {id} not found");

			return Ok(result);
		}
	}
}

