namespace Talabat.Core.Entities.IdentityModule
{
	public class Address
	{
		public int Id { get; set; }
		public string FistName { get; set; }
		public string LastName { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string AppUserId { get; set; } //FK
		public AppUser User { get; set; }
	}
}