using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Service.Specifications
{
	public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{

		protected BaseSpecifications(Expression<Func<TEntity, bool>> criteriaExp)
		{
			Criteria = criteriaExp;
		}
		public Expression<Func<TEntity, bool>> Criteria { get; private set; }
		public List<Expression<Func<TEntity, object>>> Includes { get; } = [];
		public Expression<Func<TEntity, object>> Order { get; private set; }
		public Expression<Func<TEntity, object>> OrderDesc { get; private set; }

		#region Methods
		protected void AddIncludes(Expression<Func<TEntity, object>> include)
		{
			if (include is not null)
				Includes.Add(include);
		}

		protected void AddOrderBy(Expression<Func<TEntity, object>> order)
		{
			if (order is not null)
				Order = order;
		}
		protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderDesc)
		{
			if (orderDesc is not null)
				OrderDesc = orderDesc;
		}
		#endregion
	}
}
