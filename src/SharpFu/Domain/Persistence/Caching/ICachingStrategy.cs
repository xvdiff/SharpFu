using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Caching;
using SharpFu.Domain.Persistence.Repositories.Queries;
using SharpFu.Domain.Persistence.Specifications;

namespace SharpFu.Domain.Persistence.Caching
{
	public interface ICachingStrategy<TEntity, in TIdentity>
		where TEntity : class 
	{

		int? MaximumResults { get; set; }
		string CachePrefix { get; }
		ICacheProvider CacheProvider { get; }

		bool TryGetResult(TIdentity identity, out TEntity result);

		void SaveGetResult(TIdentity key, TEntity result);

		bool TryGetAllResult<TResult>(IQueryOptions<TEntity> queryOptions, Expression<Func<TEntity, TResult>> selector,
			out IEnumerable<TResult> result);

		void SaveGetAllResult<TResult>(IQueryOptions<TEntity> queryOptions, Expression<Func<TEntity, TResult>> selector,
			IEnumerable<TResult> result);

		bool TryFindAllResult<TResult>(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions,
			Expression<Func<TEntity, TResult>> selector, out IEnumerable<TResult> result);

		void SaveFindAllResult<TResult>(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions,
			Expression<Func<TEntity, TResult>> selector, IEnumerable<TResult> result);

		bool TryFindResult<TResult>(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions,
			Expression<Func<TEntity, TResult>> selector, out TResult result);

		void SaveFindResult<TResult>(ISpecification<TEntity> criteria, IQueryOptions<TEntity> queryOptions,
			Expression<Func<TEntity, TResult>> selector, TResult result);

		bool TryCountResult(ISpecification<TEntity> criteria, out int count);
		void SaveCountResult(ISpecification<TEntity> criteria, int count);

		bool TryLongCountResult(ISpecification<TEntity> criteria, out long count);
		void SaveLongCountResult(ISpecification<TEntity> criteria, long count);

		bool TrySumResult<TResult>(Expression<Func<TEntity, TResult>> selector, ISpecification<TEntity> criteria,
			out TResult sum);

		void SaveSumResult<TResult>(Expression<Func<TEntity, TResult>> selector, ISpecification<TEntity> criteria, TResult sum);

		bool TryAverageResult<TSelector, TResult>(Expression<Func<TEntity, TSelector>> selector,
			ISpecification<TEntity> criteria, out TResult sum);

		void SaveAverageResult<TSelector, TResult>(Expression<Func<TEntity, TSelector>> selector,
			ISpecification<TEntity> criteria, TResult sum);

		bool TryMinResult<TResult>(Expression<Func<TEntity, TResult>> selector, ISpecification<TEntity> criteria,
			out TResult sum);

		void SaveMinResult<TResult>(Expression<Func<TEntity, TResult>> selector, ISpecification<TEntity> criteria, TResult sum);

		bool TryMaxResult<TResult>(Expression<Func<TEntity, TResult>> selector, ISpecification<TEntity> criteria,
			out TResult sum);

		void SaveMaxResult<TResult>(Expression<Func<TEntity, TResult>> selector, ISpecification<TEntity> criteria, TResult sum);

		void Add(TIdentity identity, TEntity result);
		void AddOrUpdate(TIdentity identity, TEntity result);
		void Update(TIdentity identity, TEntity result);
		void Delete(TIdentity identity);
		void ClearAll();

	}
}
