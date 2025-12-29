using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderModule;
using Talabat.Core.Entities.ProductModule;

namespace Talabat.Repository.Data
{
    public static class StoreDbContextSeed
    {
		public static async Task SeedAsync(StoreDbContext context)
		{
			await SeedDataAsync<ProductBrand, int>(context, context.Brands, "../Talabat.Repository/Data/DataSeed/brands.json");
			await SeedDataAsync<ProductType, int>(context, context.Types, "../Talabat.Repository/Data/DataSeed/types.json");
			await SeedDataAsync<Product, int>(context, context.Products, "../Talabat.Repository/Data/DataSeed/products.json");
			await SeedDataAsync<DeliveryMethod, int>(context, context.DeliveryMethods, "../Talabat.Repository/Data/DataSeed/delivery.json");

		}
		private static async Task SeedDataAsync<TEntity, TKey>(StoreDbContext context, DbSet<TEntity> dbSet, string filePath)
			where TEntity : BaseEntity<TKey>
		{
			if (!dbSet.Any())
			{
				var jsonData = File.ReadAllText(filePath);
				var items = JsonSerializer.Deserialize<List<TEntity>>(jsonData);
				if (items is not null && items.Count > 0)
				{
					await dbSet.AddRangeAsync(items);
				}
				await context.SaveChangesAsync();
			}
		}

	}
}