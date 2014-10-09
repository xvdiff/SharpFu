#region

using System;
using System.Linq.Expressions;
using SharpFu.Linq.Expressions;

#endregion

namespace SharpFu.Domain.Persistence.Specifications
{
	/// <summary>
	///     Denotes a XOR specification based on
	///     <see cref="Expression.ExclusiveOr(System.Linq.Expressions.Expression, System.Linq.Expressions.Expression)" />
	/// </summary>
	public class ExclusiveOrSpecification<T> : CompositeSpecificationBase<T>
	{
		public ExclusiveOrSpecification(ISpecification<T> left, ISpecification<T> right)
			: base(left, right)
		{
		}

		public override Expression<Func<T, bool>> Predicate
		{
			get { return Left.Predicate.ExclusiveOr(Right.Predicate); }
		}
	}
}