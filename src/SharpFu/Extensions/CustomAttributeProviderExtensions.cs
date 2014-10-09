#region

using System;
using System.Reflection;

#endregion

namespace SharpFu.Extensions
{
	public static class CustomAttributeProviderExtensions
	{

		public static T[] GetAttributes<T>(this ICustomAttributeProvider member)
			where T : Attribute
		{
			return member.GetAttributes<T>(false);
		}

		public static T[] GetAttributes<T>(this ICustomAttributeProvider member, bool inherit)
			where T : Attribute
		{
			return member.GetCustomAttributes(typeof (T), inherit) as T[];
		}

		public static bool HasAttribute<T>(this ICustomAttributeProvider member)
			where T : Attribute
		{
			return member.HasAttribute<T>(false);
		}

		public static bool HasAttribute<T>(this ICustomAttributeProvider member, bool inherit)
			where T : Attribute
		{
			return member.IsDefined(typeof (T), inherit);
		}
	}
}