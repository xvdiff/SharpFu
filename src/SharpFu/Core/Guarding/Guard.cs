#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using SharpFu.Extensions;

#endregion

namespace SharpFu.Core.Guarding
{
	/// <summary>
	///     Provides guarding operations
	///     for arguments
	/// </summary>
	public static class Guard
	{

		public static void AgainstNullArgument<TArgument>([ValidatedNotNull] TArgument argument, string argumentName = null)
			where TArgument : class
		{
			if (argument == null)
				throw new ArgumentNullException(argumentName);
		}

		public static void AgainstDefaultArgument<TArgument>([ValidatedNotNull] TArgument argument, string argumentName = null)
		{
			if(EqualityComparer<TArgument>.Default.Equals(argument, default(TArgument)))
				throw new ArgumentException("Argument must not be default", argumentName);
		}

		public static void AgainstPropertyIsNull<TArgument, TProperty>([ValidatedNotNull] TArgument argument, Func<TArgument, TProperty> selector,
			string propertyName = null)
			where TProperty : class
		{
			AgainstNullArgument(selector);
			AgainstNullArgument(selector(argument));
		}

		public static void AgainstPropertyIsDefault<TArgument, TProperty>([ValidatedNotNull] TArgument argument,
			Func<TArgument, TProperty> selector,
			string propertyName = null)
		{
			AgainstNullArgument(selector);
			AgainstDefaultArgument(selector(argument));
		}

		public static void AgainstOutOfRange<TArgument>(TArgument argument, TArgument minInclusive, TArgument maxInclusive,
			string argumentName = null)
			where TArgument : struct, IComparable<TArgument>, IConvertible
		{
			if(argument.CompareTo(minInclusive) < 0 || argument.CompareTo(maxInclusive) > 0)
				throw new ArgumentOutOfRangeException(argumentName, "Value should be in range {" + minInclusive + "-" + maxInclusive + "}");
		}

		public static void AgainstNullOrEmpty([ValidatedNotNull] string argument, string argumentName = null)
		{
			if (String.IsNullOrEmpty(argument))
				throw new ArgumentException("Argument cannot be null or empty", argumentName);
		}

		public static void AgainstCondition(bool condition, string argumentName = null, string message = null)
		{
			AgainstCondition(condition, () => new ArgumentException(message, argumentName));
		}

		public static void AgainstCondition<TException>(bool condition, Func<TException> exception)
			where TException : Exception
		{
			if (condition)
				throw exception();
		}

		public static void EnsureImplements<TInterface>(object argument)
		{
			var argumentType		= argument.GetType();
			var interfaceTypeName	= typeof (TInterface).GetName();
			
			AgainstCondition(!argumentType.Implements<TInterface>(),
				() => new InvalidOperationException("Argument of type {0} does not implement {1}"
				.Args(argumentType.GetName(), interfaceTypeName)));
		}

		public static void EnsureIsBasedOn<TType>(object argument)
		{
			var argumentType	= argument.GetType();
			var baseType		= typeof(TType);

			AgainstCondition(!(argumentType.BaseType == baseType),
				() => new InvalidOperationException("Argument of type {0} does not derive from {1}"
				.Args(argumentType.GetName(), baseType.Name)));
		}

		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}