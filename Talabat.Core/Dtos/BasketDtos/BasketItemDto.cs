namespace Talabat.Core.Dtos.BasketDtos
{
	public class BasketItemDto
	{
		public int Id { get; set; }
		public string PrdouctName { get; set; } = default!;
		public string PictureUtl { get; set; } = default!;
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}
}