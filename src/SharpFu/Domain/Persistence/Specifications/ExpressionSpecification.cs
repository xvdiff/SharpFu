#region

using System;
using System.Linq.Expressions;

#endregion

namespace SharpFu.Domain.Persistence.Specifications
{
	/// <summary>
	///     Denotes a specification built up by
	///     a expression predicate
	/// </summary>
	internal sealed class ExpressionSpecification<T> : SpecificationBase<T>
	{
		private readonly Expression<Func<T, bool>> _expression;

		public ExpressionSpecification(Expression<Func<T, bool>> expression)
		{
			_expression = expression;
		}

		public override Expression<Func<T, bool>> Predicate
		{
			get { return _expression; }
		}
	}
}