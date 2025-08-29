using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data.Contexts;

namespace Talabat.Repository.Repositories
{
	public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
	{
		private readonly StoreDbContext _context;

		public GenericRepository(StoreDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
		
			return await _context.Set<TEntity>().ToListAsync();
		}

		public async Task<TEntity> GetAsync(TKey id)
		{
	
			
			return await _context.Set<TEntity>().FindAsync(id);
		}
		public async Task AddAsync(TEntity entity)
		{
			await _context.AddAsync(entity);
		}

		public void Update(TEntity entity)
		{
			_context.Update(entity);
		}
		public void Delete(TEntity entity)
		{
			_context.Remove(entity);
		}

		#region With Specifications
		public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
			=> await SpecificationsEvaluator.CreateQuery(_context.Set<TEntity>(), specifications).ToListAsync();
		

		public async Task<TEntity> GetAsync(ISpecifications<TEntity, TKey> specifications)
			=> await SpecificationsEvaluator.CreateQuery(_context.Set<TEntity>(), specifications).FirstOrDefaultAsync();

		public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
			=> await SpecificationsEvaluator.CreateQuery(_context.Set<TEntity>(), specifications).CountAsync();
		#endregion
	}

}