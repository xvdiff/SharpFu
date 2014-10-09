#region

using System;
using System.Linq.Expressions;

#endregion

namespace SharpFu.Domain.Persistence.Specifications
{
	/// <summary>
	///     Extension methods for specifications
	/// </summary>
	public static class SpecificationExtensions
	{
		/// <summary>
		///     Returns a new And specification based
		///     on the current as left
		///     and a new one as right
		/// </summary>
		/// <seealso cref="AndSpecification{T}" />
		public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> right)
		{
			return new AndSpecification<T>(left, right);
		}

		/// <summary>
		///     Returns a new Or specification based
		///     on the current as left
		///     and a new one as right
		/// </summary>
		/// <seealso cref="OrSpecification{T}" />
		public static ISpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right)
		{
			return new OrSpecification<T>(left, right);
		}

		/// <summary>
		///     Returns a new AndNot specification based
		///     on the current as left
		///     and a new one as right
		/// </summary>
		/// <seealso cref="AndNotSpecification{T}" />
		public static ISpecification<T> AndNot<T>(this ISpecification<T> left, ISpecification<T> right)
		{
			return new AndNotSpecification<T>(left, right);
		}

		/// <summary>
		///     Returns a new Not specification based
		///     on the current one
		/// </summary>
		/// <seealso cref="NotSpecification{T}" />
		public static ISpecification<T> Not<T>(this ISpecification<T> specification)
		{
			return new NotSpecification<T>(specification);
		}

		/// <summary>
		///     Returns a new And specification based
		///     on the current as left
		///     and a predicate as right
		/// </summary>
		/// <seealso cref="AndSpecification{T}" />
		public static ISpecification<T> And<T>(this ISpecification<T> specification, Expression<Func<T, bool>> predicate)
		{
			return new AndSpecification<T>(specification, new ExpressionSpecification<T>(predicate));
		}

		/// <summary>
		///     Returns a new AndAlso specification based
		///     on the current as left
		///     and a new one as right
		/// </summary>
		/// <seealso cref="AndAlsoSpecification{T}" />
		public static ISpecification<T> AndAlso<T>(this ISpecification<T> left, ISpecification<T> right)
		{
			return new AndAlsoSpecification<T>(left, right);
		}

		/// <summary>
		///     Returns a new AndAlso specification based
		///     on the current as left
		///     and a predicate as right
		/// </summary>
		/// <seealso cref="AndAlsoSpecification{T}" />
		public static ISpecification<T> AndAlso<T>(this ISpecification<T> specification, Expression<Func<T, bool>> predicate)
		{
			return new AndAlsoSpecification<T>(specification, new ExpressionSpecification<T>(predicate));
		}

		/// <summary>
		///     Returns a new AndNot specification based
		///     on the current as left
		///     and a predicate as right
		/// </summary>
		/// <seealso cref="AndNotSpecification{T}" />
		public static ISpecification<T> AndNot<T>(this ISpecification<T> specification, Expression<Func<T, bool>> predicate)
		{
			return new AndNotSpecification<T>(specification, new ExpressionSpecification<T>(predicate));
		}

		/// <summary>
		///     Returns a new Or specification based
		///     on the current as left
		///     and a predicate as right
		/// </summary>
		/// <seealso cref="OrSpecification{T}" />
		public static ISpecification<T> Or<T>(this ISpecification<T> specification, Expression<Func<T, bool>> predicate)
		{
			return new OrSpecification<T>(specification, new ExpressionSpecification<T>(predicate));
		}

		/// <summary>
		///     Returns a new OrElse specification based
		///     on the current as left
		///     and a new one as right
		/// </summary>
		/// <seealso cref="OrElseSpecification{T}" />
		public static ISpecification<T> OrElse<T>(this ISpecification<T> left, ISpecification<T> right)
		{
			return new OrElseSpecification<T>(left, right);
		}

		public static ISpecification<T> OrElse<T>(this ISpecification<T> specification, Expression<Func<T, bool>> predicate)
		{
			return new OrElseSpecification<T>(specification, new ExpressionSpecification<T>(predicate));
		}

		public static ISpecification<T> OrNot<T>(this ISpecification<T> left, ISpecification<T> right)
		{
			return new OrNotSpecification<T>(left, right);
		}

		public static ISpecification<T> OrNot<T>(this ISpecification<T> specification, Expression<Func<T, bool>> predicate)
		{
			return new OrNotSpecification<T>(specification, new ExpressionSpecification<T>(predicate));
		}
	}
}