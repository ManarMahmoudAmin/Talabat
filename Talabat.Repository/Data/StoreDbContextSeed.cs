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
			if (!context.Brands.Any())
			{
				//1.Read Data from files
				var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");

				//2. Convert Json string to list<T>
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

				//3. Seed Data to Database
				if (brands is not null && brands.Count() > 0)
					await context.Brands.AddRangeAsync(brands);
				await context.SaveChangesAsync();
			}

			if (!context.Types.Any())
			{
				var typesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
				var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

				if (types is not null && types.Count() > 0)
					await context.Types.AddRangeAsync(types);
				await context.SaveChangesAsync();
			}

			if (!context.Products.Any())
			{
				var productsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productsData);

				if(products is not null && products.Count() > 0)
					await context.Products.AddRangeAsync(products);
				await context.SaveChangesAsync();
			}

		}
    }
}
