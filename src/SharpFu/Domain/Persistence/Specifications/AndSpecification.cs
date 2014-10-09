#region

using System;
using System.Linq.Expressions;
using SharpFu.Linq.Expressions;

#endregion

namespace SharpFu.Domain.Persistence.Specifications
{
	/// <summary>
	///     Denotes a And specification based on
	///     <see cref="Expression.And(System.Linq.Expressions.Expression, System.Linq.Expressions.Expression)" />
	/// </summary>
	public class AndSpecification<T> : CompositeSpecificationBase<T>
	{
		public AndSpecification(ISpecification<T> left, ISpecification<T> right)
			: base(left, right)
		{
		}

		public override Expression<Func<T, bool>> Predicate
		{
			get { return Left.Predicate.And(Right.Predicate); }
		}
	}
}