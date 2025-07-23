using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Repository.Data.Contexts;

namespace Talabat.Repository.Data
{
    public static class StoreDbContextSeed
    {
		public static async Task SeedAsync(StoreDbContext context)
		{
			await seedDataAsync<ProductBrand, int>(context, context.Brands, "../Talabat.Repository/Data/DataSeed/brands.json");
			await seedDataAsync<ProductType, int>(context, context.Types, "../Talabat.Repository/Data/DataSeed/types.json");
			await seedDataAsync<Product, int>(context, context.Products, "../Talabat.Repository/Data/DataSeed/products.json");

		}
		private static async Task seedDataAsync<TEntity, TKey>(StoreDbContext context, DbSet<TEntity> dbSet, string filePath)
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