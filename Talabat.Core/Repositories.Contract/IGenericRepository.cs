using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(TKey id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
		void Delete(TEntity entity);

        #region With Specifications
        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications);
        Task<TEntity> GetAsync(ISpecifications<TEntity, TKey> specifications);
        Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications);
		#endregion
	}
}
