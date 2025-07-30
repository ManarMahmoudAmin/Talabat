using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositories.Contract
{
    public interface ISpecifications<TEntity,TKey> where TEntity : BaseEntity<TKey>
	{
		public Expression<Func<TEntity,bool>>? Criteria { get; }
		public List<Expression<Func<TEntity, object>>> Includes { get; }
		public Expression<Func<TEntity, object>> Order { get; }
		public Expression<Func<TEntity, object>> OrderDesc { get; }

	}
}
