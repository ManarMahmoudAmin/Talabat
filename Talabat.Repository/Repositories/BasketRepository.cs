using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities.BasketModule;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository.Repositories
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _database;
		public BasketRepository(IConnectionMultiplexer connection) 
		{
			_database = connection.GetDatabase();
		}
		public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? time = null)
		{
			var BasketJson = JsonSerializer.Serialize(basket);
			var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, BasketJson, time ?? TimeSpan.FromDays(30));
			
			if (IsCreatedOrUpdated)
				return await GetBasketAsync(basket.Id);
			else 
				return null;
		}

		public async Task<bool> DeleteBasketAsync(string key)
          => await _database.KeyDeleteAsync(key);

		public async Task<CustomerBasket?> GetBasketAsync(string key)
		{
			var Basket = await _database.StringGetAsync(key);
			if(Basket.IsNullOrEmpty)
				return null;
			else
				return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
		}
	}
}
