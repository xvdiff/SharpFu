using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;
using SharpFu.Linq.Expressions;

namespace SharpFu.Extensions
{
	public static class GenericExtensions
	{
		public static void SetPropertyValue<T, TProperty>(this T source, Expression<Func<T, TProperty>> selector,
			TProperty newValue)
		{
			var propertyInfo = source.GetProperty(selector);
			propertyInfo.SetValue(source, newValue, null);
		}

		public static PropertyInfo GetProperty<T, TProperty>(this T source, Expression<Func<T, TProperty>> selector)
		{
			Guard.AgainstNullArgument(selector, "selector");

			var propertyName = selector.GetMemberInfo().Name;
			var type = source.GetType();

			return type.GetProperty(propertyName, typeof(TProperty));
		}

		public static IEnumerable<T> Populate<T>(int count)
		{
			for (var i = 0; i < count; i++)
			{
				yield return Activator.CreateInstance<T>();
			}
		}
	}
}
