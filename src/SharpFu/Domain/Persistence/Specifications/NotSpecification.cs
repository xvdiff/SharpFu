#region

using System;
using System.Linq.Expressions;
using SharpFu.Linq.Expressions;

#endregion

namespace SharpFu.Domain.Persistence.Specifications
{
	/// <summary>
	///     Denotes a Not specification based on
	///     <see cref="Expression.Not(System.Linq.Expressions.Expression)" />
	/// </summary>
	public class NotSpecification<T> : SpecificationBase<T>
	{
		private readonly ISpecification<T> _specification;

		public NotSpecification(ISpecification<T> specification)
		{
			_specification = specification;
		}

		public override Expression<Func<T, bool>> Predicate
		{
			get { return _specification.Predicate.Not(); }
		}
	}
}