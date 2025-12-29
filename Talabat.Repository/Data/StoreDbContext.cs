using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Talabat.Core.Entities.OrderModule;
using Talabat.Core.Entities.ProductModule;

namespace Talabat.Repository.Data
{
    public class StoreDbContext : DbContext
    {
		public StoreDbContext(DbContextOptions<StoreDbContext> options) 
			: base(options)
		{
			
		}
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductType> Types { get; set; }
		public DbSet<ProductBrand> Brands { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<DeliveryMethod> DeliveryMethods { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
