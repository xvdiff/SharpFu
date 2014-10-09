#region

using System;
using System.Linq.Expressions;
using SharpFu.Linq.Expressions;

#endregion

namespace SharpFu.Domain.Persistence.Specifications
{
	/// <summary>
	///     Denotes a Or specification based on
	///     <see cref="Expression.Or(System.Linq.Expressions.Expression, System.Linq.Expressions.Expression)" />
	/// </summary>
	public class OrSpecification<T> : CompositeSpecificationBase<T>
	{
		public OrSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right)
		{
		}

		public override Expression<Func<T, bool>> Predicate
		{
			get { return Left.Predicate.Or(Right.Predicate); }
		}
	}
}