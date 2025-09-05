using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Talabat.Core.Entities.ProductModule;

namespace Talabat.Repository.Data.Contexts
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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
