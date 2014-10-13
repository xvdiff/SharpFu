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

		/// <summary>
		///		Guards against null arguments
		/// </summary>
		/// <typeparam name="TArgument">Argument type</typeparam>
		/// <param name="argument">Argument to guard</param>
		/// <param name="argumentName">Argument name</param>
		public static void AgainstNullArgument<TArgument>([ValidatedNotNull] TArgument argument, string argumentName = null)
			where TArgument : class
		{
			AgainstCondition(argument == null, 
				() => new ArgumentNullException(argumentName));
		}

		/// <summary>
		///		Guards against out of range
		///		arguments
		/// </summary>
		/// <typeparam name="TArgument">Argument type implementing <see cref="IComparable{T}"/>, <see cref="IComparable"/></typeparam>
		/// <param name="argument">Argument</param>
		/// <param name="minInclusive">Minimum inclusive value</param>
		/// <param name="maxInclusive">Maximum inclusive value</param>
		/// <param name="argumentName">Argument name</param>
		public static void AgainstOutOfRange<TArgument>(TArgument argument, TArgument minInclusive, TArgument maxInclusive,
			string argumentName = null)
			where TArgument : struct, IComparable<TArgument>, IConvertible
		{
			AgainstCondition(argument.CompareTo(minInclusive) < 0 || argument.CompareTo(maxInclusive) > 0,
				() => new ArgumentOutOfRangeException(argumentName,
					"Argument value must be in range [{0};{1}]. Actual value: {3}".Args(minInclusive, minInclusive, argument)));
		}

		/// <summary>
		///		Guards against a string being null or
		///		empty
		/// </summary>
		/// <param name="argument">Argument</param>
		/// <param name="argumentName">Argument name</param>
		public static void AgainstNullOrEmpty([ValidatedNotNull] string argument, string argumentName = null)
		{
			AgainstCondition(String.IsNullOrEmpty(argument),
				() => new ArgumentException("Argument cannot be null or empty", argumentName));
		}

		/// <summary>
		///		Guards against a certain condition
		/// </summary>
		/// <param name="condition">Condition to check</param>
		/// <param name="argumentName">Argument name</param>
		/// <param name="message">Exceptional message</param>
		public static void AgainstCondition(bool condition, string argumentName = null, string message = null)
		{
			AgainstCondition(condition, () => new ArgumentException(message, argumentName));
		}

		/// <summary>
		///		Guards against a certain condition using
		///		a custom exception function
		/// </summary>
		/// <typeparam name="TException">Exception type</typeparam>
		/// <param name="condition">Condition to check</param>
		/// <param name="exception">Exception function</param>
		public static void AgainstCondition<TException>(bool condition, Func<TException> exception)
			where TException : Exception
		{
			if (condition)
				throw exception();
		}

		/// <summary>
		///		Ensures that the passed argument
		///		implements a certain interface
		/// </summary>
		/// <typeparam name="TInterface">Interface type</typeparam>
		/// <param name="argument">Argument</param>
		public static void EnsureImplements<TInterface>(object argument)
		{
			var argumentType		= argument.GetType();
			var interfaceTypeName	= typeof (TInterface).GetName();
			
			AgainstCondition(!argumentType.Implements<TInterface>(),
				() => new InvalidOperationException("Argument of type {0} does not implement {1}"
				.Args(argumentType.GetName(), interfaceTypeName)));
		}

		/// <summary>
		///		Ensures that the passed argument
		///		is based on a certain type
		/// </summary>
		/// <typeparam name="TType">Arbitary type</typeparam>
		/// <param name="argument">Argument</param>
		public static void EnsureIsBasedOn<TType>(object argument)
		{
			var argumentType	= argument.GetType();
			var baseType		= typeof(TType);

			AgainstCondition(argumentType.BaseType != baseType,
				() => new InvalidOperationException("Argument of type {0} does not derive from {1}"
				.Args(argumentType.GetName(), baseType.Name)));
		}

		/// <summary>
		///		Ensures that the passes argument
		///		is an instance of a certain type
		/// </summary>
		/// <typeparam name="TType">Arbitary type</typeparam>
		/// <param name="argument">Argument</param>
		public static void EnsureIsInstanceOf<TType>(object argument)
		{
			var argumentType	= argument.GetType();
			var type			= typeof (TType);

			AgainstCondition(argumentType != type, 
				() => new InvalidOperationException("Argument is not of type " + type.GetName()));
		}

		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}