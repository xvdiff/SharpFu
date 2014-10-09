#region

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SharpFu.Domain.Persistence.Repositories.Queries;

#endregion

namespace SharpFu.Domain.Persistence.Traits
{
	public interface ICanGetByQuery<TEntity>
		where TEntity : class
	{
		IEnumerable<TEntity> GetAll(IQueryOptions<TEntity> queryOptions);

		IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
			IQueryOptions<TEntity> queryOptions = null);
	}
}