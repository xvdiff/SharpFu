#region

using System;
using System.Linq.Expressions;
using SharpFu.Linq.Expressions;

#endregion

namespace SharpFu.Domain.Persistence.Specifications
{
	/// <summary>
	///     Denotes a OrElse specification based on
	///     <see cref="Expression.OrElse(System.Linq.Expressions.Expression, System.Linq.Expressions.Expression)" />
	/// </summary>
	public class OrElseSpecification<T> : CompositeSpecificationBase<T>
	{
		public OrElseSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right)
		{
		}

		public override Expression<Func<T, bool>> Predicate
		{
			get { return Left.Predicate.OrElse(Right.Predicate); }
		}
	}
}