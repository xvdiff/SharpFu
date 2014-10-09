using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Caching;
using SharpFu.Domain.Persistence.Repositories.Queries;

namespace SharpFu.Domain.Persistence.Caching
{
	public class TimeoutCachingStrategy<TEntity, TIdentity>
		: CachingStrategyBase<TEntity, TIdentity>
		where TEntity : class 
	{

		public TimeSpan Lifespan { get; set; }
		
		public TimeoutCachingStrategy(ICacheProvider cacheProvider, TimeSpan lifespan, int? maximumResults)
			: base(cacheProvider, maximumResults)
		{
			
		}

		public override void Add(TIdentity identity, TEntity result)
		{
			InternalTryPush(GetWriteThroughCacheKey(identity), result);
		}

		public override void AddOrUpdate(TIdentity identity, TEntity result)
		{
			InternalTryPush(GetWriteThroughCacheKey(identity), result);
		}

		public override void Delete(TIdentity identity)
		{
			CacheProvider.Clear(GetWriteThroughCacheKey(identity));
		}

		public override void Update(TIdentity identity, TEntity result)
		{
			InternalTryPush(GetWriteThroughCacheKey(identity), result);
		}

		protected override void InternalTryPush<TCacheItem>(string cacheKey, TCacheItem result, IQueryOptions<TEntity> queryOptions = null)
		{
			try
			{
				CacheProvider.Push(cacheKey, result, Lifespan);

				var options = queryOptions as IPagingOptions;
				if (options != null)
				{
					CacheProvider.Push(cacheKey + "=>pagingTotal", options.TotalItems, Lifespan);
				}
			}
			catch
			{
				
			}
		}
	}
}
