using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Caching;
using SharpFu.Domain.Persistence.Repositories.Queries;

namespace SharpFu.Domain.Persistence.Caching
{
	public sealed class NullCachingStrategy<TEntity, TIdentity> : ICachingStrategy<TEntity, TIdentity>
		where TEntity : class 
	{
		public int? MaximumResults { get; set; }

		private readonly ICacheProvider _cacheProvider;

		public string CachePrefix
		{
			get { return string.Empty; }
		}

		public ICacheProvider CacheProvider
		{
			get { return _cacheProvider; }
		}

		public bool TryGetResult(TIdentity identity, out TEntity result)
		{
			result = default(TEntity);
			return false;
		}

		public void SaveGetResult(TIdentity key, TEntity result)
		{
			
		}

		public bool TryGetAllResult<TResult>(IQueryOptions<TEntity> queryOptions, Expression<Func<TEntity, TResult>> selector, out IEnumerable<TResult> result)
		{
			result = default(IEnumerable<TResult>);
			return false;
		}

		public void SaveGetAllResult<TResult>(Repositories.Queries.IQueryOptions<TEntity> queryOptions, System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector, IEnumerable<TResult> result)
		{
			
		}

		public bool TryFindAllResult<TResult>(Specifications.ISpecification<TEntity> criteria, Repositories.Queries.IQueryOptions<TEntity> queryOptions, System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector, out IEnumerable<TResult> result)
		{
			result = default(IEnumerable<TResult>);
			return false;
		}

		public void SaveFindAllResult<TResult>(Specifications.ISpecification<TEntity> criteria, Repositories.Queries.IQueryOptions<TEntity> queryOptions, System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector, IEnumerable<TResult> result)
		{
			
		}

		public bool TryFindResult<TResult>(Specifications.ISpecification<TEntity> criteria, Repositories.Queries.IQueryOptions<TEntity> queryOptions, System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector, out TResult result)
		{
			result = default (TResult);
			return false;
		}

		public void SaveFindResult<TResult>(Specifications.ISpecification<TEntity> criteria, Repositories.Queries.IQueryOptions<TEntity> queryOptions, System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector, TResult result)
		{
			
		}

		public bool TryCountResult(Specifications.ISpecification<TEntity> criteria, out int count)
		{
			count = default(int);
			return false;
		}

		public void SaveCountResult(Specifications.ISpecification<TEntity> criteria, int count)
		{
			
		}

		public bool TryLongCountResult(Specifications.ISpecification<TEntity> criteria, out long count)
		{
			count = default (long);
			return false;
		}

		public void SaveLongCountResult(Specifications.ISpecification<TEntity> criteria, long count)
		{
		
		}

		public bool TrySumResult<TResult>(System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector, Specifications.ISpecification<TEntity> criteria, out TResult sum)
		{
			sum = default(TResult);
			return false;
		}

		public void SaveSumResult<TResult>(System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector, Specifications.ISpecification<TEntity> criteria, TResult sum)
		{
			
		}

		public bool TryAverageResult<TSelector, TResult>(System.Linq.Expressions.Expression<Func<TEntity, TSelector>> selector, Specifications.ISpecification<TEntity> criteria, out TResult sum)
		{
			sum = default (TResult);
			return false;
		}

		public void SaveAverageResult<TSelector, TResult>(System.Linq.Expressions.Expression<Func<TEntity, TSelector>> selector, Specifications.ISpecification<TEntity> criteria, TResult sum)
		{
			
		}

		public bool TryMinResult<TResult>(System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector, Specifications.ISpecification<TEntity> criteria, out TResult sum)
		{
			sum = default (TResult);
			return false;
		}

		public void SaveMinResult<TResult>(System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector, Specifications.ISpecification<TEntity> criteria, TResult sum)
		{
			
		}

		public bool TryMaxResult<TResult>(System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector, Specifications.ISpecification<TEntity> criteria, out TResult sum)
		{
			sum = default (TResult);
			return false;
		}

		public void SaveMaxResult<TResult>(System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector, Specifications.ISpecification<TEntity> criteria, TResult sum)
		{
			
		}

		public void Add(TIdentity identity, TEntity result)
		{
		
		}

		public void AddOrUpdate(TIdentity identity, TEntity result)
		{
		
		}

		public void Update(TIdentity identity, TEntity result)
		{
		}

		public void Delete(TIdentity identity)
		{
		}

		public void ClearAll()
		{
		
		}
	}
}
