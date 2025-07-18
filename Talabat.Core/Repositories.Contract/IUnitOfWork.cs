using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositories.Contract
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();

		//Create Repository and return it
		IGenericRepository<TEntity, TKey> Repository<TEntity,TKey>()
            where TEntity : BaseEntity<TKey>;
    }
}
