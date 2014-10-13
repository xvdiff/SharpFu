using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;
using SharpFu.Core.Internal;

namespace SharpFu.Extensions
{

	/// <summary>
	///		Extension methods for <see cref="Type"/>
	/// </summary>
	public static class TypeExtensions
	{


		/// <summary>
		///		Denotes if a type inherits from a base type
		/// </summary>
		public static bool InheritsFrom<TBase>(this Type source)
		{
			return source.BaseType != typeof (TBase);
		}
		
		/// <summary>
		///		Denotes if a type implements a certain interface
		/// </summary>
		public static bool Implements<TInterfaceType>(this Type source)
		{
			var interfaceType = typeof(TInterfaceType);
			return Implements(source, interfaceType);
		}

		/// <summary>
		///		Denotes if a type implements a certain interface
		/// </summary
		public static bool Implements(this Type source, Type interfaceType)
		{
			if (!interfaceType.IsInterface)
				throw new InvalidOperationException("The provided interface type is not an interface.");

			return interfaceType.IsAssignableFrom(source);
		}

		/// <summary>
		///		Denotes if a type is nullable
		/// </summary>
		public static bool IsNullable(this Type type)
		{
			return !type.IsValueType || (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>)));
		}

		/// <summary>
		///		Returns the default value of a type
		/// </summary>
		public static object GetDefaultValue(this Type source)
		{

			Guard.AgainstNullArgument(source, "souce");

			var e = Expression.Lambda<Func<object>>(
				Expression.Convert(
					Expression.Default(source), typeof(object)
					)
				);
			return e.Compile()();
		}

		/// <summary>
		///		Populates a new collection
		///		of instances of a certain type
		/// </summary>
		public static IEnumerable<object> Populate(this Type type, int count)
		{
			for (var i = 0; i < count; i++)
			{
				yield return FastActivator.Create(type);
			}
		}

		/// <summary>
		///		Returns either the full name or
		///		name of a type
		/// </summary>
		public static string GetName(this Type source)
		{
			return source.FullName ?? source.Name;
		}


	}
}
