#region

using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Linq.Expressions;
using SharpFu.Caching;
using SharpFu.Core.Guarding;
using SharpFu.Domain.Persistence.Repositories.Queries;
using SharpFu.Domain.Persistence.Specifications;
using SharpFu.Extensions;

#endregion

namespace SharpFu.Domain.Persistence.Caching
{
	public abstract class CachingStrategyBase<TEntity, TIdentity> : ICachingStrategy<TEntity, TIdentity>
		where TEntity : class
	{
		private const string RepositoryCachePrefix = "#repo";
		private const string CachingPrefixCounterKey = "CachingPrefixCounter";
		private readonly string _cachePrefixCounterKey;

		private readonly ICacheProvider _cacheProvider;
		private readonly string _repositoryCacheIdentifier;


		public string CachePrefix
		{
			get
			{
				return String.Join("/", RepositoryCachePrefix,
					GetCachingPrefixCounter(),
					_repositoryCacheIdentifier);
			}
		}

		public int? MaximumResults { get; set; }

		public ICacheProvider CacheProvider
		{
			get { return _cacheProvider; }
		}


		protected CachingStrategyBase(ICacheProvider cacheProvider, int? maximumResults)
		{
			Guard.AgainstNullArgument(cacheProvider);
			MaximumResults = maximumResults;

			var entityTypeName = typeof(TEntity).GetName();
			var identityTypeName = typeof(TIdentity).GetName();
			_repositoryCacheIdentifier = entityTypeName + "/" + identityTypeName; // [ENTITY_TYPE]/[IDENTITY_TYPE]
			_cachePrefixCounterKey = String.Join("/", RepositoryCachePrefix, _repositoryCacheIdentifier,
				CachingPrefixCounterKey); // #repo/[ENTITY_TYPE]/[IDENTITY_TYPE]/CachingPrefixCounter
		}

		public bool TryGetResult(TIdentity identity, out TEntity result)
		{
			result = default(TEntity);
			try
			{
				var cacheKey = GetWriteThroughCacheKey(identity);
				return CacheProvider.Exists(cacheKey) && (CacheProvider.Get(GetWriteThroughCacheKey(identity), out result));
			}
			catch
			{
				return false;
			}
		}

		public void SaveGetResult(TIdentity identity, TEntity result)
		{
			InternalTryPush(GetWriteThroughCacheKey(identity), result);
		}

		public bool TryGetAllResult<TResult>(IQueryOptions<TEntity> queryOptions, Expression<Func<TEntity, TResult>> selector,
			out IEnumerable<TResult> result)
		{
			result = null;
			try
			{
				var cacheKey = GetAllCacheKey(queryOptions, selector);
				if(!CacheProvider.Get(cacheKey, out result))
					return false;
				
				return queryOptions == null || PushCachedQueryOptions(cacheKey, queryOptions);
			}
			catch
			{
				return false;
			}
		}

		public void SaveGetAllResult<TResult>(IQueryOptions<TEntity> queryOptions, Expression<Func<TEntity, TResult>> selector,
			IEnumerable<TResult> result)
		{
			InternalTryPush(GetAllCacheKey(queryOptions, selector), result, queryOptions);
		}

		public bool TryFindAllResult<TResult>(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions,
			Expression<Func<TEntity, TResult>> selector, out IEnumerable<TResult> result)
		{
			result = null;
			try
			{
				var cacheKey = FindAllCacheKey(criteria, queryOptions, selector);
				if (!CacheProvider.Get(cacheKey, out result))
					return false;

				return queryOptions == null || PushCachedQueryOptions(cacheKey, queryOptions);
			}
			catch
			{
				return false;
			}
		}

		public void SaveFindAllResult<TResult>(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions,
			Expression<Func<TEntity, TResult>> selector, IEnumerable<TResult> result)
		{
			InternalTryPush(FindAllCacheKey(criteria, queryOptions, selector), result, queryOptions);
		}

		public bool TryFindResult<TResult>(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions,
			Expression<Func<TEntity, TResult>> selector, out TResult result)
		{
			result = default(TResult);
			try
			{
				return CacheProvider.Get(FindCacheKey(criteria, queryOptions, selector), out result);
			}
			catch
			{
				return false;
			}
		}

		public void SaveFindResult<TResult>(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions,
			Expression<Func<TEntity, TResult>> selector, TResult result)
		{
			InternalTryPush(FindAllCacheKey(criteria, queryOptions, selector), result, queryOptions);
		}

		public bool TryCountResult(ISpecification<TEntity> criteria, out int count)
		{
			count = 0;
			try
			{
				return CacheProvider.Get(CountCacheKey(criteria), out count);
			}
			catch (Exception)
			{
				return false;
			}
		}

		public void SaveCountResult(ISpecification<TEntity> criteria, int count)
		{
			InternalTryPush(CountCacheKey(criteria), count);
		}

		public bool TryLongCountResult(ISpecification<TEntity> criteria, out long count)
		{
			count = 0;
			try
			{
				return CacheProvider.Get(LongCountCacheKey(criteria), out count);
			}
			catch
			{
				return false;
			}
		}

		public void SaveLongCountResult(ISpecification<TEntity> criteria, long count)
		{
			InternalTryPush(LongCountCacheKey(criteria), count);
		}

		public bool TrySumResult<TResult>(Expression<Func<TEntity, TResult>> selector, ISpecification<TEntity> criteria,
			out TResult sum)
		{
			sum = default(TResult);
			try
			{
				return CacheProvider.Get(SumCacheKey(selector, criteria), out sum);
			}
			catch
			{
				return false;
			}
		}

		public void SaveSumResult<TResult>(Expression<Func<TEntity, TResult>> selector, ISpecification<TEntity> criteria,
			TResult sum)
		{
			InternalTryPush(SumCacheKey(selector, criteria), sum);
		}

		public bool TryAverageResult<TSelector, TResult>(Expression<Func<TEntity, TSelector>> selector,
			ISpecification<TEntity> criteria, out TResult sum)
		{
			sum = default(TResult);
			try
			{
				return CacheProvider.Get(AverageCacheKey(selector, criteria), out sum);
			}
			catch
			{
				return false;
			}
		}

		public void SaveAverageResult<TSelector, TResult>(Expression<Func<TEntity, TSelector>> selector,
			ISpecification<TEntity> criteria, TResult sum)
		{
			InternalTryPush(AverageCacheKey(selector, criteria), sum);
		}

		public bool TryMinResult<TResult>(Expression<Func<TEntity, TResult>> selector, ISpecification<TEntity> criteria,
			out TResult sum)
		{
			sum = default(TResult);
			try
			{
				return CacheProvider.Get(MinCacheKey(selector, criteria), out sum);
			}
			catch
			{
				return false;
			}
		}

		public void SaveMinResult<TResult>(Expression<Func<TEntity, TResult>> selector, ISpecification<TEntity> criteria,
			TResult sum)
		{
			InternalTryPush(MinCacheKey(selector, criteria), sum);
		}

		public bool TryMaxResult<TResult>(Expression<Func<TEntity, TResult>> selector, ISpecification<TEntity> criteria,
			out TResult sum)
		{
			sum = default(TResult);
			try
			{
				return CacheProvider.Get(MaxCacheKey(selector, criteria), out sum);
			}
			catch
			{
				return false;
			}
		}

		public void SaveMaxResult<TResult>(Expression<Func<TEntity, TResult>> selector, ISpecification<TEntity> criteria,
			TResult sum)
		{
			InternalTryPush(MaxCacheKey(selector, criteria), sum);
		}

		public abstract void Add(TIdentity identity, TEntity result);

		public abstract void AddOrUpdate(TIdentity identity, TEntity result);

		public abstract void Update(TIdentity identity, TEntity result);

		public abstract void Delete(TIdentity identity);

		public void ClearAll()
		{
			IncrementCachingPrefixCounter();
		}

		protected bool InternalTryGetPagingTotal(string cacheKey, out int totalItems)
		{
			totalItems = 0;
			try
			{
				if (CacheProvider.Get(cacheKey + "=>pagingTotal", out totalItems))
					return true;
			}
			catch
			{
				return false;
			}

			return false;
		}

		protected virtual void InternalTryPush<TCacheItem>(string cacheKey, TCacheItem result,
			IQueryOptions<TEntity> queryOptions = null)
		{
			try
			{
				CacheProvider.Push(cacheKey, result);

				var options = queryOptions as IPagingOptions;
				if (options != null)
				{
					CacheProvider.Push(cacheKey + "=>pagingTotal", options.TotalItems);
				}
			}
			catch
			{
			}
		}

		private int GetCachingPrefixCounter()
		{
			int counter;
			return !CacheProvider.Get(_cachePrefixCounterKey, out counter) ? 1 : counter;
		}

		private void IncrementCachingPrefixCounter()
		{
			CacheProvider.Increment(_cachePrefixCounterKey, 1, 1);
		}

		protected bool PushCachedQueryOptions(string cacheKey, IQueryOptions<TEntity> queryOptions)
		{
			if (!(queryOptions is IPagingOptions))
				return true;

			int totalItems;
			if (!InternalTryGetPagingTotal(cacheKey, out totalItems)) 
				return false;
			((IPagingOptions)queryOptions).TotalItems = totalItems;
			return true;
		}

		protected string GetWriteThroughCacheKey(TIdentity identity)
		{
			return String.Format("{0}/{1}", CachePrefix, identity);
		}

		protected virtual string GetAllCacheKey<TResult>(IQueryOptions<TEntity> queryOptions,
			Expression<Func<TEntity, TResult>> selector)
		{
			return String.Format("{0}/{1}", CachePrefix, String.Join("::", "All", queryOptions, GetExpressionBody(selector)));
		}

		protected virtual string FindAllCacheKey<TResult>(ISpecification<TEntity> criteria,
			IQueryOptions<TEntity> queryOptions, Expression<Func<TEntity, TResult>> selector)
		{
			return String.Format("{0}/{1}", CachePrefix,
				String.Join("::", "FindAll", criteria, queryOptions, GetExpressionBody(selector)));
		}

		protected virtual string FindCacheKey<TResult>(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions,
			Expression<Func<TEntity, TResult>> selector)
		{
			return String.Format("{0}/{1}", CachePrefix,
				String.Join("::", "Find", criteria, queryOptions, GetExpressionBody(selector)));
		}

		protected virtual string CountCacheKey(ISpecification<TEntity> criteria)
		{
			return String.Format("{0}/{1}", CachePrefix, String.Join("::", "Count", criteria));
		}

		protected virtual string LongCountCacheKey(ISpecification<TEntity> criteria)
		{
			return String.Format("{0}/{1}", CachePrefix, String.Join("::", "LongCount", criteria));
		}

		protected virtual string GroupCountCacheKey<TGroupKey>(Func<TEntity, TGroupKey> keySelector,
			ISpecification<TEntity> criteria)
		{
			return String.Format("{0}/{1}", CachePrefix, String.Join("::", "GroupCount", keySelector, criteria));
		}

		protected virtual string GroupLongCountCacheKey<TGroupKey>(Func<TEntity, TGroupKey> keySelector,
			ISpecification<TEntity> criteria)
		{
			return String.Format("{0}/{1}", CachePrefix, String.Join("::", "GroupLongCount", keySelector, criteria));
		}

		protected virtual string GroupCacheKey<TGroupKey, TResult>(Expression<Func<TEntity, TGroupKey>> keySelector,
			Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>> resultSelector, ISpecification<TEntity> criteria)
		{
			return String.Format("{0}/{1}", CachePrefix,
				String.Join("::", "Group", GetExpressionBody(keySelector), GetExpressionBody(resultSelector), criteria));
		}

		protected virtual string SumCacheKey<TResult>(Expression<Func<TEntity, TResult>> selector,
			ISpecification<TEntity> criteria)
		{
			return String.Format("{0}/{1}", CachePrefix, String.Join("::", "Sum", GetExpressionBody(selector), criteria));
		}

		protected virtual string AverageCacheKey<TSelector>(Expression<Func<TEntity, TSelector>> selector,
			ISpecification<TEntity> criteria)
		{
			return String.Format("{0}/{1}", CachePrefix, String.Join("::", "Average", GetExpressionBody(selector), criteria));
		}

		protected virtual string MinCacheKey<TResult>(Expression<Func<TEntity, TResult>> selector,
			ISpecification<TEntity> criteria)
		{
			return String.Format("{0}/{1}", CachePrefix, String.Join("::", "Min", GetExpressionBody(selector), criteria));
		}

		protected virtual string MaxCacheKey<TResult>(Expression<Func<TEntity, TResult>> selector,
			ISpecification<TEntity> criteria)
		{
			return String.Format("{0}/{1}", CachePrefix, String.Join("::", "Max", GetExpressionBody(selector), criteria));
		}

		private static string GetExpressionBody(LambdaExpression expression)
		{
			return expression == null ? string.Empty : expression.Body.ToString();
		}
	}
}