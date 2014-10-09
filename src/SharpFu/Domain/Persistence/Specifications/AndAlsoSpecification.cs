#region

using System;
using System.Linq.Expressions;
using SharpFu.Linq.Expressions;

#endregion

namespace SharpFu.Domain.Persistence.Specifications
{
	/// <summary>
	///     Denotes a AndAlso specification based on
	///     <see cref="Expression.AndAlso(System.Linq.Expressions.Expression, System.Linq.Expressions.Expression)" />
	/// </summary>
	public class AndAlsoSpecification<T> : CompositeSpecificationBase<T>
	{
		public AndAlsoSpecification(ISpecification<T> left, ISpecification<T> right)
			: base(left, right)
		{
		}

		public override Expression<Func<T, bool>> Predicate
		{
			get { return Left.Predicate.AndAlso(Right.Predicate); }
		}
	}
}