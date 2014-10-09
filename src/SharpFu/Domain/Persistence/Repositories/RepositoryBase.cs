#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SharpFu.Core.Guarding;
using SharpFu.Domain.Model;
using SharpFu.Domain.Model.Conventions;
using SharpFu.Domain.Persistence.Caching;
using SharpFu.Domain.Persistence.Repositories.Exceptions;
using SharpFu.Domain.Persistence.Repositories.Queries;
using SharpFu.Domain.Persistence.Specifications;
using SharpFu.Extensions;

#endregion

namespace SharpFu.Domain.Persistence.Repositories
{
	public abstract partial class RepositoryBase<TEntity, TIdentity> :
		CrudRepositoryBase<TEntity, TIdentity>,
		IRepository<TEntity, TIdentity>
		where TEntity : class
	{

		protected RepositoryBase()
			: this(null)
		{
		}

		protected RepositoryBase(Expression<Func<TEntity, TIdentity>> identitySelector, 
			ICachingStrategy<TEntity, TIdentity> cachingStrategy = null)
			: base(identitySelector, cachingStrategy)
		{
			
		}

		public bool Exists(TIdentity key)
		{
			TEntity entity;
			return TryGet(key, out entity);
		}

		public bool Exists(Expression<Func<TEntity, bool>> predicate)
		{
			Guard.AgainstNullArgument(predicate);

			TEntity entity;
			return TryFind(predicate, out entity);
		}

		public bool Exists(ISpecification<TEntity> criteria)
		{
			Guard.AgainstNullArgument(criteria);

			TEntity entity;
			return TryFind(criteria, out entity);
		}

		public void Delete(Expression<Func<TEntity, bool>> predicate)
		{
			Guard.AgainstNullArgument(predicate);

			Delete(new ExpressionSpecification<TEntity>(predicate));
		}

		public void Delete(ISpecification<TEntity> criteria)
		{
			Guard.AgainstNullArgument(criteria);

			Delete(FindAll(criteria));
		}

		public virtual TEntity Find(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions = null)
		{
			return FindQuery(criteria, queryOptions);
		}

		public virtual TResult Find<TResult>(ISpecification<TEntity> criteria, Expression<Func<TEntity, TResult>> selector,
			IQueryOptions<TEntity> queryOptions = null)
		{
			return FindQuery(criteria, selector, queryOptions);
		}

		public TEntity Find(Expression<Func<TEntity, bool>> predicate, IQueryOptions<TEntity> queryOptions = null)
		{
			return Find(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), queryOptions);
		}

		public bool TryFind(Expression<Func<TEntity, bool>> predicate, out TEntity entity)
		{
			return TryFind(predicate, (IQueryOptions<TEntity>) null, out entity);
		}

		public bool TryFind(Expression<Func<TEntity, bool>> predicate, IQueryOptions<TEntity> queryOptions, out TEntity entity)
		{
			return TryFind(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate),
				(IQueryOptions<TEntity>) null, out entity);
		}

		public bool TryFind(ISpecification<TEntity> criteria, out TEntity entity)
		{
			return TryFind(criteria, (IQueryOptions<TEntity>) null, out entity);
		}

		public bool TryFind(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions, out TEntity entity)
		{
			entity = null;
			try
			{
				entity = Find(criteria, queryOptions);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public TResult Find<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector,
			IQueryOptions<TEntity> queryOptions = null)
		{
			return Find(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector, queryOptions);
		}

		public bool TryFind<TResult>(ISpecification<TEntity> criteria, Expression<Func<TEntity, TResult>> selector,
			out TResult result)
		{
			return TryFind(criteria, selector, null, out result);
		}

		public bool TryFind<TResult>(ISpecification<TEntity> criteria, Expression<Func<TEntity, TResult>> selector,
			IQueryOptions<TEntity> queryOptions, out TResult result)
		{
			result = default(TResult);
			try
			{
				result = Find(criteria, selector, queryOptions);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool TryFind<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector,
			out TResult result)
		{
			return TryFind(predicate, selector, null, out result);
		}

		public bool TryFind<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector,
			IQueryOptions<TEntity> queryOptions, out TResult result)
		{
			return TryFind(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector, queryOptions,
				out result);
		}

		public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate,
			IQueryOptions<TEntity> queryOptions = null)
		{
			return FindAll(predicate == null
				? null
				: new ExpressionSpecification<TEntity>(predicate), queryOptions);
		}

		public IEnumerable<TResult> FindAll<TResult>(Expression<Func<TEntity, bool>> predicate,
			Expression<Func<TEntity, TResult>> selector, IQueryOptions<TEntity> queryOptions = null)
		{
			return FindAll(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector, queryOptions);
		}

		public virtual IEnumerable<TEntity> FindAll(ISpecification<TEntity> criteria,
			IQueryOptions<TEntity> queryOptions = null)
		{
			return FindAllQuery(criteria, queryOptions);
		}

		public virtual IEnumerable<TResult> FindAll<TResult>(ISpecification<TEntity> criteria,
			Expression<Func<TEntity, TResult>> selector, IQueryOptions<TEntity> queryOptions = null)
		{
			return FindAllQuery(criteria, selector, queryOptions);
		}

		public int Count()
		{
			return FindAllQuery(null).Count();
		}

		public long LongCount()
		{
			return FindAllQuery(null).LongCount();
		}

		public int Count(ISpecification<TEntity> criteria)
		{
			return FindAllQuery(criteria).Count();
		}

		public int Count(Expression<Func<TEntity, bool>> predicate)
		{
			return FindAllQuery(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate)).Count();
		}

		public long LongCount(ISpecification<TEntity> criteria)
		{
			return FindAllQuery(criteria).LongCount();
		}

		public long LongCount(Expression<Func<TEntity, bool>> predicate)
		{
			return FindAllQuery(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate)).Count();
		}

		public IEnumerable<TResult> GroupBy<TGroupKey, TResult>(Expression<Func<TEntity, TGroupKey>> keySelector,
			Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>> resultSelector)
		{
			return GroupBy((ISpecification<TEntity>) null, keySelector, resultSelector);
		}

		public IEnumerable<TResult> GroupBy<TGroupKey, TResult>(ISpecification<TEntity> criteria,
			Expression<Func<TEntity, TGroupKey>> keySelector,
			Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>> resultSelector)
		{
			return FindAllQuery(criteria).GroupBy(keySelector).Select(resultSelector);
		}

		public IEnumerable<TResult> GroupBy<TGroupKey, TResult>(Expression<Func<TEntity, bool>> predicate,
			Expression<Func<TEntity, TGroupKey>> keySelector,
			Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>> resultSelector)
		{
			return GroupBy(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), keySelector,
				resultSelector);
		}

		public int Sum(Expression<Func<TEntity, int>> selector)
		{
			return Sum((ISpecification<TEntity>) null, selector);
		}

		public int Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, int>> selector)
		{
			return FindAllQuery(criteria).Sum(selector);
		}

		public int Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> selector)
		{
			return Sum(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public int? Sum(Expression<Func<TEntity, int?>> selector)
		{
			return Sum((ISpecification<TEntity>) null, selector);
		}

		public int? Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, int?>> selector)
		{
			return FindAllQuery(criteria).Sum(selector);
		}

		public int? Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int?>> selector)
		{
			return Sum(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public long Sum(Expression<Func<TEntity, long>> selector)
		{
			return Sum((ISpecification<TEntity>) null, selector);
		}

		public long Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, long>> selector)
		{
			return FindAllQuery(criteria).Sum(selector);
		}

		public long Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, long>> selector)
		{
			return Sum(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public long? Sum(Expression<Func<TEntity, long?>> selector)
		{
			return Sum((ISpecification<TEntity>) null, selector);
		}

		public long? Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, long?>> selector)
		{
			return FindAllQuery(criteria).Sum(selector);
		}

		public long? Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, long?>> selector)
		{
			return Sum(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public decimal Sum(Expression<Func<TEntity, decimal>> selector)
		{
			return Sum((ISpecification<TEntity>) null, selector);
		}

		public decimal Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, decimal>> selector)
		{
			return FindAllQuery(criteria).Sum(selector);
		}

		public decimal Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> selector)
		{
			return Sum(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public decimal? Sum(Expression<Func<TEntity, decimal?>> selector)
		{
			return Sum((ISpecification<TEntity>) null, selector);
		}

		public decimal? Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, decimal?>> selector)
		{
			return FindAllQuery(criteria).Sum(selector);
		}

		public decimal? Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal?>> selector)
		{
			return Sum(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public double Sum(Expression<Func<TEntity, double>> selector)
		{
			return Sum((ISpecification<TEntity>) null, selector);
		}

		public double Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, double>> selector)
		{
			return FindAllQuery(criteria).Sum(selector);
		}

		public double Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, double>> selector)
		{
			return Sum(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public double? Sum(Expression<Func<TEntity, double?>> selector)
		{
			return Sum((ISpecification<TEntity>) null, selector);
		}

		public double? Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, double?>> selector)
		{
			return FindAllQuery(criteria).Sum(selector);
		}

		public double? Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, double?>> selector)
		{
			return Sum(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public float Sum(Expression<Func<TEntity, float>> selector)
		{
			return Sum((ISpecification<TEntity>) null, selector);
		}

		public float Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, float>> selector)
		{
			return FindAllQuery(criteria).Sum(selector);
		}

		public float Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, float>> selector)
		{
			return Sum(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public float? Sum(Expression<Func<TEntity, float?>> selector)
		{
			return Sum((ISpecification<TEntity>) null, selector);
		}

		public float? Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, float?>> selector)
		{
			return FindAllQuery(criteria).Sum(selector);
		}

		public float? Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, float?>> selector)
		{
			return Sum(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public double Average(Expression<Func<TEntity, int>> selector)
		{
			return Average((ISpecification<TEntity>) null, selector);
		}

		public double Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, int>> selector)
		{
			return FindAllQuery(criteria).Average(selector);
		}

		public double Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> selector)
		{
			return Average(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public double? Average(Expression<Func<TEntity, int?>> selector)
		{
			return Average((ISpecification<TEntity>) null, selector);
		}

		public double? Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, int?>> selector)
		{
			return FindAllQuery(criteria).Average(selector);
		}

		public double? Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int?>> selector)
		{
			return Average(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public double Average(Expression<Func<TEntity, long>> selector)
		{
			return Average((ISpecification<TEntity>) null, selector);
		}

		public double Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, long>> selector)
		{
			return FindAllQuery(criteria).Average(selector);
		}

		public double Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, long>> selector)
		{
			return Average(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public double? Average(Expression<Func<TEntity, long?>> selector)
		{
			return Average((ISpecification<TEntity>) null, selector);
		}

		public double? Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, long?>> selector)
		{
			return FindAllQuery(criteria).Average(selector);
		}

		public double? Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, long?>> selector)
		{
			return Average(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public decimal Average(Expression<Func<TEntity, decimal>> selector)
		{
			return Average((ISpecification<TEntity>) null, selector);
		}

		public decimal Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, decimal>> selector)
		{
			return FindAllQuery(criteria).Average(selector);
		}

		public decimal Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> selector)
		{
			return Average(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public decimal? Average(Expression<Func<TEntity, decimal?>> selector)
		{
			return Average((ISpecification<TEntity>) null, selector);
		}

		public decimal? Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, decimal?>> selector)
		{
			return FindAllQuery(criteria).Average(selector);
		}

		public decimal? Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal?>> selector)
		{
			return Average(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public double Average(Expression<Func<TEntity, double>> selector)
		{
			return Average((ISpecification<TEntity>) null, selector);
		}

		public double Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, double>> selector)
		{
			return FindAllQuery(criteria).Average(selector);
		}

		public double Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, double>> selector)
		{
			return Average(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public double? Average(Expression<Func<TEntity, double?>> selector)
		{
			return Average((ISpecification<TEntity>) null, selector);
		}

		public double? Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, double?>> selector)
		{
			return FindAllQuery(criteria).Average(selector);
		}

		public double? Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, double?>> selector)
		{
			return Average(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public float Average(Expression<Func<TEntity, float>> selector)
		{
			return Average((ISpecification<TEntity>) null, selector);
		}

		public float Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, float>> selector)
		{
			return FindAllQuery(criteria).Average(selector);
		}

		public float Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, float>> selector)
		{
			return Average(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public float? Average(Expression<Func<TEntity, float?>> selector)
		{
			return Average((ISpecification<TEntity>) null, selector);
		}

		public float? Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, float?>> selector)
		{
			return FindAllQuery(criteria).Average(selector);
		}

		public float? Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, float?>> selector)
		{
			return Average(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public TResult Min<TResult>(Expression<Func<TEntity, TResult>> selector)
		{
			return Min((ISpecification<TEntity>) null, selector);
		}

		public TResult Min<TResult>(ISpecification<TEntity> criteria, Expression<Func<TEntity, TResult>> selector)
		{
			return FindAllQuery(criteria).Min(selector);
		}

		public TResult Min<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector)
		{
			return Min(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public TResult Max<TResult>(Expression<Func<TEntity, TResult>> selector)
		{
			return Max((ISpecification<TEntity>) null, selector);
		}

		public TResult Max<TResult>(ISpecification<TEntity> criteria, Expression<Func<TEntity, TResult>> selector)
		{
			return FindAllQuery(criteria).Max(selector);
		}

		public TResult Max<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector)
		{
			return Max(predicate == null ? null : new ExpressionSpecification<TEntity>(predicate), selector);
		}

		public virtual IEnumerable<TEntity> GetAll(IQueryOptions<TEntity> queryOptions)
		{
			return GetAllQuery(queryOptions);
		}

		public virtual IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
			IQueryOptions<TEntity> queryOptions = null)
		{
			return GetAllQuery(queryOptions).Select(selector);
		}

		private TResult FindQuery<TResult>(ISpecification<TEntity> criteria, Expression<Func<TEntity, TResult>> selector,
			IQueryOptions<TEntity> queryOptions = null)
		{
			return FindAllQuery(criteria, selector, queryOptions).FirstOrDefault();
		}

		private TEntity FindQuery(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions = null)
		{
			var entities = GetAllQuery(queryOptions);
			var result = criteria.SatisfyingEntityFrom(entities);
			if (result == null)
				throw new EntityNotFoundException(EntityType, IdentityType, criteria.ToString());

			return result;
		}

		private IQueryable<TEntity> GetAllQuery(IQueryOptions<TEntity> queryOptions = null)
		{
			var baseQuery = ExecuteBaseQuery();
			if (queryOptions != null)
				baseQuery = queryOptions.Apply(baseQuery);

			return baseQuery;
		}

		private IQueryable<TResult> FindAllQuery<TResult>(ISpecification<TEntity> criteria,
			Expression<Func<TEntity, TResult>> selector,
			IQueryOptions<TEntity> queryOptions = null)
		{
			if (selector == null)
				throw new ArgumentNullException("selector");
			return FindAllQuery(criteria, queryOptions).Select(selector);
		}

		private IQueryable<TEntity> FindAllQuery(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions = null)
		{
			var entities = GetAllQuery(queryOptions);
			return criteria != null ? criteria.SatisfyingEntitiesFrom(entities) : entities;
		}

		protected override IEnumerable<TEntity> GetAllEntities()
		{
			return GetAllQuery();
		}

		protected override TEntity GetEntity(TIdentity identity)
		{
			return Find(ByIdentitySpecification(identity));
		}

		protected override TResult GetEntity<TResult>(TIdentity identity, Expression<Func<TEntity, TResult>> selector)
		{
			return Find(ByIdentitySpecification(identity), selector);
		}

		protected abstract IQueryable<TEntity> ExecuteBaseQuery();

	}
}