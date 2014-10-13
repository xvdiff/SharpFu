#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SharpFu.Domain.Persistence.Caching;
using SharpFu.Domain.Persistence.Repositories.Queries;
using SharpFu.Domain.Persistence.Specifications;

#endregion

namespace SharpFu.Domain.Persistence
{

	/// <summary>
	///		Middleware between the caching strategy and
	///		a persistence layer query
	/// </summary>
	public class EntityCacheManager<TEntity, TIdentity>
		where TEntity : class
	{
		private readonly ICachingStrategy<TEntity, TIdentity> _cachingStrategy;

		/// <summary>
		///		Creates a new instance of a <see cref="EntityCacheManager{TEntity,TIdentity}"/>
		/// </summary>
		public EntityCacheManager(ICachingStrategy<TEntity, TIdentity> cachingStrategy)
		{
			CacheUsed			= false;
			_cachingStrategy	= cachingStrategy;
		}

		/// <summary>
		///		Denotes if the cache has been used
		/// </summary>
		public bool CacheUsed { get; private set; }

		/// <summary>
		///		Denotes if the cache is currently enabled
		/// </summary>
		public bool CacheEnabled
		{
			get { return _cachingStrategy != null; }
		}

		/// <summary>
		///		Returns the current caching strategy
		/// </summary>
		public ICachingStrategy<TEntity, TIdentity> CachingStrategy
		{
			get { return _cachingStrategy; }
		} 

		/// <summary>
		///		Executes a get query
		/// </summary>
		public TEntity ExecuteGet(Func<TEntity> query, TIdentity key)
		{
			TEntity result;

			if (CacheEnabled && _cachingStrategy.TryGetResult(key, out result))
			{
				CacheUsed = true;
				return result;
			}

			CacheUsed = false;
			result = query.Invoke();

			_cachingStrategy.SaveGetResult(key, result);

			return result;
		}

		/// <summary>
		///		Executes a get all query
		/// </summary>
		public IEnumerable<TResult> ExecuteGetAll<TResult>(Func<IEnumerable<TResult>> query,
			Expression<Func<TEntity, TResult>> selector, IQueryOptions<TEntity> queryOptions)
		{
			IEnumerable<TResult> result;
			if (CacheEnabled && _cachingStrategy.TryGetAllResult(queryOptions, selector, out result))
			{
				CacheUsed = true;
				return result;
			}

			CacheUsed = false;
			result = query.Invoke();

			var executeGetAll = result as TResult[] ?? result.ToArray();
			_cachingStrategy.SaveGetAllResult(queryOptions, selector, executeGetAll);

			return executeGetAll;
		}

		/// <summary>
		///		Executes a find all query
		/// </summary>
		public IEnumerable<TResult> ExecuteFindAll<TResult>(Func<IEnumerable<TResult>> query, ISpecification<TEntity> criteria,
			Expression<Func<TEntity, TResult>> selector, IQueryOptions<TEntity> queryOptions)
		{
			IEnumerable<TResult> result;
			if (CacheEnabled && _cachingStrategy.TryFindAllResult(criteria, queryOptions, selector, out result))
			{
				CacheUsed = true;
				return result;
			}

			CacheUsed = false;
			result = query.Invoke();

			var executeFindAll = result as TResult[] ?? result.ToArray();
			_cachingStrategy.SaveFindAllResult(criteria, queryOptions, selector, executeFindAll);

			return executeFindAll;
		}

		/// <summary>
		///		Executes a find query
		/// </summary>
		public TResult ExecuteFind<TResult>(Func<TResult> query, ISpecification<TEntity> criteria,
			Expression<Func<TEntity, TResult>> selector, IQueryOptions<TEntity> queryOptions)
		{
			TResult result;
			if (CacheEnabled && _cachingStrategy.TryFindResult(criteria, queryOptions, selector, out result))
			{
				CacheUsed = true;
				return result;
			}

			CacheUsed = false;
			result = query.Invoke();

			_cachingStrategy.SaveFindResult(criteria, queryOptions, selector, result);

			return result;
		}

		/// <summary>
		///		Called when an entity has been added
		/// </summary>
		public void OnEntityAdded(TIdentity identity, TEntity entity)
		{
			if(CacheEnabled)
				_cachingStrategy.Add(identity, entity);
		}

		/// <summary>
		///		Called when an entity has been added or updated
		/// </summary>
		public void OnEntityAddedOrUpdated(TIdentity identity, TEntity entity)
		{
			if (CacheEnabled)
				_cachingStrategy.AddOrUpdate(identity, entity);
		}

		/// <summary>
		///		Called when an entity has been updated
		/// </summary>
		public void OnEntityUpdated(TIdentity identity, TEntity entity)
		{
			if (CacheEnabled)
				_cachingStrategy.Update(identity, entity);
		}

		/// <summary>
		///		Called when an entity has been deleted
		/// </summary>
		public void OnEntityDeleted(TIdentity identity)
		{
			if (CacheEnabled)
				_cachingStrategy.Delete(identity);
		}
	}
}