using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Service.Specifications
{
	public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		protected BaseSpecifications() { }
		protected BaseSpecifications(Expression<Func<TEntity, bool>> criteriaExp)
		{
			Criteria = criteriaExp;
		}
		public Expression<Func<TEntity, bool>> Criteria { get; private set; }

		public List<Expression<Func<TEntity, object>>> Includes { get; } = [];

		protected void AddIncludes(Expression<Func<TEntity, object>> include)
		{
			if (include != null)
			{
				Includes.Add(include);
			}
		}
	}
}
