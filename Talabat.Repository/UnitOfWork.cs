using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data.Contexts;
using Talabat.Repository.Repositories;

namespace Talabat.Repository
{
	class UnitOfWork : IUnitOfWork
	{
		private readonly StoreDbContext _context;

		private readonly Hashtable _repositories;

		public UnitOfWork(StoreDbContext context)
		{
			_context = context;
			_repositories = new Hashtable();
		}
		public Task<int> CompleteAsync()
			=> _context.SaveChangesAsync();


		public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
		{
			var repoType = typeof(TEntity).Name;

			//Check if the repository already exists in the hashtable
			if (!_repositories.ContainsKey((repoType)))
			{
				var repository = new GenericRepository<TEntity, TKey>(_context);

				_repositories.Add(repoType, repository);
			}

			return _repositories[repoType] as IGenericRepository<TEntity, TKey>;
		}
	}
}
