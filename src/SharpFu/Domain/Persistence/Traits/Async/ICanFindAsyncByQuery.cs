#region

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SharpFu.Domain.Persistence.Repositories.Queries;
using SharpFu.Domain.Persistence.Specifications;

#endregion

namespace SharpFu.Domain.Persistence.Traits.Async
{
	public interface ICanFindAsyncByQuery<TEntity>
		where TEntity : class
	{
		Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
		Task<bool> Exists(ISpecification<TEntity> criteria);
		Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate, IQueryOptions<TEntity> queryOptions = null);
		Task<TEntity> Find(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions = null);

		Task<TResult> Find<TResult>(ISpecification<TEntity> criteria, Expression<Func<TEntity, TResult>> selector,
			IQueryOptions<TEntity> queryOptions = null);

		Task<TResult> Find<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector,
			IQueryOptions<TEntity> queryOptions = null);

		Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate,
			IQueryOptions<TEntity> queryOptions = null);

		Task<IEnumerable<TResult>> FindAll<TResult>(Expression<Func<TEntity, bool>> predicate,
			Expression<Func<TEntity, TResult>> selector, IQueryOptions<TEntity> queryOptions = null);

		Task<IEnumerable<TEntity>> FindAll(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions = null);

		Task<IEnumerable<TResult>> FindAll<TResult>(ISpecification<TEntity> criteria,
			Expression<Func<TEntity, TResult>> selector, IQueryOptions<TEntity> queryOptions = null);
	}
}