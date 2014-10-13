using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;
using SharpFu.Core.Internal;
using SharpFu.Linq.Expressions;

namespace SharpFu.Extensions
{

	/// <summary>
	///		Extension methods for generic types
	/// </summary>
	public static class GenericExtensions
	{

		/// <summary>
		///		Sets the value of a property using
		///		reflection defined by a property selector expression
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="TargetException"></exception>
		/// <exception cref="TargetParameterCountException"></exception>
		/// <exception cref="MethodAccessException"></exception>
		/// <exception cref="TargetInvocationException"></exception>
		public static void SetPropertyValue<T, TProperty>(this T source, Expression<Func<T, TProperty>> selector,
			TProperty newValue)
		{
			var propertyInfo = source.GetProperty(selector);
			propertyInfo.SetValue(source, newValue, null);
		}

		/// <summary>
		///		Returns a <see cref="PropertyInfo"/> using
		///		a property selector expression
		/// </summary>
		/// <exception cref="AmbiguousMatchException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		public static PropertyInfo GetProperty<T, TProperty>(this T source, Expression<Func<T, TProperty>> selector)
		{
			Guard.AgainstNullArgument(selector, "selector");

			var propertyName = selector.GetMemberInfo().Name;
			var type = source.GetType();

			return type.GetProperty(propertyName, typeof(TProperty));
		}

		/// <summary>
		///		Returns a collection of instances
		///		of a certain type
		/// </summary>
		public static IEnumerable<T> Populate<T>(int count)
		{
			for (var i = 0; i < count; i++)
			{
				yield return FastActivator.Create<T>();
			}
		}
	}
}
