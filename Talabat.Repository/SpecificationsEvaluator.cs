using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository
{
    static class SpecificationsEvaluator
    {
		public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity,TKey> specifications)
			where TEntity : BaseEntity<TKey>
		{
			var query = inputQuery;

			if(specifications.Criteria is not null)
				query = query.Where(specifications.Criteria);

			if (specifications.Order is not null)
				query = query.OrderBy(specifications.Order);

			if (specifications.OrderDesc is not null)
				query = query.OrderByDescending(specifications.OrderDesc);

			if(specifications.Includes is not null && specifications.Includes.Count > 0)
			{
				///foreach (var include in specifications.Includes)
				///{
				///	query = query.Include(include);
				///}

				query = specifications.Includes.Aggregate(query, (currQuery, include) =>  currQuery.Include(include));
			}

			if(specifications.IsPaginated)
				query = query.Skip(specifications.Skip).Take(specifications.Take);

			return query;
		}
	}
}
