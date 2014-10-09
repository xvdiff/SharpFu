#region

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SharpFu.Domain.Persistence.Repositories.Queries;
using SharpFu.Domain.Persistence.Specifications;

#endregion

namespace SharpFu.Domain.Persistence.Traits
{
	public interface ICanFindByQuery<TEntity>
		where TEntity : class
	{
		#region exists by criteria/predicate

		bool Exists(Expression<Func<TEntity, bool>> predicate);
		bool Exists(ISpecification<TEntity> criteria);

		#endregion

		#region find by criteria/predicate using query options

		TEntity Find(Expression<Func<TEntity, bool>> predicate, IQueryOptions<TEntity> queryOptions = null);
		TEntity Find(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions = null);

		bool TryFind(Expression<Func<TEntity, bool>> predicate, out TEntity entity);
		bool TryFind(Expression<Func<TEntity, bool>> predicate, IQueryOptions<TEntity> queryOptions, out TEntity entity);
		bool TryFind(ISpecification<TEntity> criteria, out TEntity entity);
		bool TryFind(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions, out TEntity entity);

		#endregion

		#region find by criteria/predicate w/selector using query options

		TResult Find<TResult>(ISpecification<TEntity> criteria, Expression<Func<TEntity, TResult>> selector,
			IQueryOptions<TEntity> queryOptions = null);

		TResult Find<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector,
			IQueryOptions<TEntity> queryOptions = null);

		bool TryFind<TResult>(ISpecification<TEntity> criteria, Expression<Func<TEntity, TResult>> selector,
			out TResult result);

		bool TryFind<TResult>(ISpecification<TEntity> criteria, Expression<Func<TEntity, TResult>> selector,
			IQueryOptions<TEntity> queryOptions, out TResult result);

		bool TryFind<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector,
			out TResult result);

		bool TryFind<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector,
			IQueryOptions<TEntity> queryOptions, out TResult result);

		#endregion

		#region find all by criteria/predicate w/selector using query options

		IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, IQueryOptions<TEntity> queryOptions = null);

		IEnumerable<TResult> FindAll<TResult>(Expression<Func<TEntity, bool>> predicate,
			Expression<Func<TEntity, TResult>> selector, IQueryOptions<TEntity> queryOptions = null);

		IEnumerable<TEntity> FindAll(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions = null);

		IEnumerable<TResult> FindAll<TResult>(ISpecification<TEntity> criteria, Expression<Func<TEntity, TResult>> selector,
			IQueryOptions<TEntity> queryOptions = null);

		#endregion
	}
}