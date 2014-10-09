#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SharpFu.Domain.Persistence.Specifications;

#endregion

namespace SharpFu.Domain.Persistence.Traits.Async
{
	public interface ICanAggregateAsyncByQuery<TEntity>
		where TEntity : class
	{
		#region Count

		Task<int> CountAsync(ISpecification<TEntity> criteria);
		Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

		#endregion

		#region Long count

		Task<long> LongCountAsync(ISpecification<TEntity> criteria);
		Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

		#endregion

		#region Group by

		Task<IEnumerable<TResult>> GroupByAsync<TGroupKey, TResult>(Expression<Func<TEntity, TGroupKey>> keySelector,
			Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>> resultSelector);

		Task<IEnumerable<TResult>> GroupByAsync<TGroupKey, TResult>(ISpecification<TEntity> criteria,
			Expression<Func<TEntity, TGroupKey>> keySelector,
			Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>> resultSelector);

		Task<IEnumerable<TResult>> GroupByAsync<TGroupKey, TResult>(Expression<Func<TEntity, bool>> predicate,
			Expression<Func<TEntity, TGroupKey>> keySelector,
			Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>> resultSelector);

		#endregion

		#region Sum

		Task<int> SumAsync(Expression<Func<TEntity, int>> selector);
		Task<int> SumAsync(ISpecification<TEntity> criteria, Expression<Func<TEntity, int>> selector);
		Task<int> SumAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> selector);
		Task<long> SumAsync(Expression<Func<TEntity, long>> selector);
		Task<long> SumAsync(ISpecification<TEntity> criteria, Expression<Func<TEntity, long>> selector);
		Task<long> SumAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, long>> selector);
		Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector);
		Task<decimal> SumAsync(ISpecification<TEntity> criteria, Expression<Func<TEntity, decimal>> selector);
		Task<decimal> SumAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> selector);
		Task<double> SumAsync(Expression<Func<TEntity, double>> selector);
		Task<double> SumAsync(ISpecification<TEntity> criteria, Expression<Func<TEntity, double>> selector);
		Task<double> SumAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, double>> selector);
		Task<float> SumAsync(Expression<Func<TEntity, float>> selector);
		Task<float> SumAsync(ISpecification<TEntity> criteria, Expression<Func<TEntity, float>> selector);
		Task<float> SumAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, float>> selector);

		#endregion

		#region Average

		Task<double> AverageAsync(Expression<Func<TEntity, int>> selector);
		Task<double> AverageAsync(ISpecification<TEntity> criteria, Expression<Func<TEntity, int>> selector);
		Task<double> AverageAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> selector);
		Task<double> AverageAsync(Expression<Func<TEntity, long>> selector);
		Task<double> AverageAsync(ISpecification<TEntity> criteria, Expression<Func<TEntity, long>> selector);
		Task<double> AverageAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, long>> selector);
		Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector);
		Task<decimal> AverageAsync(ISpecification<TEntity> criteria, Expression<Func<TEntity, decimal>> selector);
		Task<decimal> AverageAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> selector);
		Task<double> AverageAsync(Expression<Func<TEntity, double>> selector);
		Task<double> AverageAsync(ISpecification<TEntity> criteria, Expression<Func<TEntity, double>> selector);
		Task<double> AverageAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, double>> selector);
		Task<float> AverageAsync(Expression<Func<TEntity, float>> selector);
		Task<float> AverageAsync(ISpecification<TEntity> criteria, Expression<Func<TEntity, float>> selector);
		Task<float> AverageAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, float>> selector);

		#endregion

		#region Min

		Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> selector);
		Task<TResult> MinAsync<TResult>(ISpecification<TEntity> criteria, Expression<Func<TEntity, TResult>> selector);
		Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);

		#endregion

		#region Max

		Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector);
		Task<TResult> MaxAsync<TResult>(ISpecification<TEntity> criteria, Expression<Func<TEntity, TResult>> selector);
		Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);

		#endregion
	}
}