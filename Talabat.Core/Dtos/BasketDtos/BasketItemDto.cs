using System.ComponentModel.DataAnnotations;

namespace Talabat.Core.Dtos.BasketDtos
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }
		[Required]

		public string ProductName { get; set; } = default!;
		[Required]

		public string PictureUrl { get; set; } = default!;

		[Required]
		[Range(0.1, double.MaxValue, ErrorMessage ="Price Can not be Zero")]
		public decimal Price { get; set; }

		[Required]
		[Range(1, int.MaxValue, ErrorMessage ="Quantity Must be At Least One Item")]
		public int Quantity { get; set; }
	}
}