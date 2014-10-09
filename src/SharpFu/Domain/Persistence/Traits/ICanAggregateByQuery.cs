#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SharpFu.Domain.Persistence.Specifications;

#endregion

namespace SharpFu.Domain.Persistence.Traits
{
	public interface ICanAggregateByQuery<TEntity>
		where TEntity : class
	{
		#region Count

		int Count(ISpecification<TEntity> criteria);
		int Count(Expression<Func<TEntity, bool>> predicate);

		#endregion

		#region Long count

		long LongCount(ISpecification<TEntity> criteria);
		long LongCount(Expression<Func<TEntity, bool>> predicate);

		#endregion

		#region Group

		IEnumerable<TResult> GroupBy<TGroupKey, TResult>(Expression<Func<TEntity, TGroupKey>> keySelector,
			Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>> resultSelector);

		IEnumerable<TResult> GroupBy<TGroupKey, TResult>(ISpecification<TEntity> criteria,
			Expression<Func<TEntity, TGroupKey>> keySelector,
			Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>> resultSelector);

		IEnumerable<TResult> GroupBy<TGroupKey, TResult>(Expression<Func<TEntity, bool>> predicate,
			Expression<Func<TEntity, TGroupKey>> keySelector,
			Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>> resultSelector);

		#endregion

		#region Sum

		int Sum(Expression<Func<TEntity, int>> selector);
		int Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, int>> selector);
		int Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> selector);
		int? Sum(Expression<Func<TEntity, int?>> selector);
		int? Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, int?>> selector);
		int? Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int?>> selector);
		long Sum(Expression<Func<TEntity, long>> selector);
		long Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, long>> selector);
		long Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, long>> selector);
		long? Sum(Expression<Func<TEntity, long?>> selector);
		long? Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, long?>> selector);
		long? Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, long?>> selector);
		decimal Sum(Expression<Func<TEntity, decimal>> selector);
		decimal Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, decimal>> selector);
		decimal Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> selector);
		decimal? Sum(Expression<Func<TEntity, decimal?>> selector);
		decimal? Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, decimal?>> selector);
		decimal? Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal?>> selector);
		double Sum(Expression<Func<TEntity, double>> selector);
		double Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, double>> selector);
		double Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, double>> selector);
		double? Sum(Expression<Func<TEntity, double?>> selector);
		double? Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, double?>> selector);
		double? Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, double?>> selector);
		float Sum(Expression<Func<TEntity, float>> selector);
		float Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, float>> selector);
		float Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, float>> selector);
		float? Sum(Expression<Func<TEntity, float?>> selector);
		float? Sum(ISpecification<TEntity> criteria, Expression<Func<TEntity, float?>> selector);
		float? Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, float?>> selector);

		#endregion

		#region Average

		double Average(Expression<Func<TEntity, int>> selector);
		double Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, int>> selector);
		double Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> selector);
		double? Average(Expression<Func<TEntity, int?>> selector);
		double? Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, int?>> selector);
		double? Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int?>> selector);
		double Average(Expression<Func<TEntity, long>> selector);
		double Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, long>> selector);
		double Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, long>> selector);
		double? Average(Expression<Func<TEntity, long?>> selector);
		double? Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, long?>> selector);
		double? Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, long?>> selector);
		decimal Average(Expression<Func<TEntity, decimal>> selector);
		decimal Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, decimal>> selector);
		decimal Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> selector);
		decimal? Average(Expression<Func<TEntity, decimal?>> selector);
		decimal? Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, decimal?>> selector);
		decimal? Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal?>> selector);
		double Average(Expression<Func<TEntity, double>> selector);
		double Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, double>> selector);
		double Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, double>> selector);
		double? Average(Expression<Func<TEntity, double?>> selector);
		double? Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, double?>> selector);
		double? Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, double?>> selector);
		float Average(Expression<Func<TEntity, float>> selector);
		float Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, float>> selector);
		float Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, float>> selector);
		float? Average(Expression<Func<TEntity, float?>> selector);
		float? Average(ISpecification<TEntity> criteria, Expression<Func<TEntity, float?>> selector);
		float? Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, float?>> selector);

		#endregion

		#region Min

		TResult Min<TResult>(Expression<Func<TEntity, TResult>> selector);
		TResult Min<TResult>(ISpecification<TEntity> criteria, Expression<Func<TEntity, TResult>> selector);
		TResult Min<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);

		#endregion

		#region Max

		TResult Max<TResult>(Expression<Func<TEntity, TResult>> selector);
		TResult Max<TResult>(ISpecification<TEntity> criteria, Expression<Func<TEntity, TResult>> selector);
		TResult Max<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);

		#endregion
	}
}