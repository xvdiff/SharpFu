#region

using System;
using System.Reflection;

#endregion

namespace SharpFu.Extensions
{

	/// <summary>
	///		Extension methods for <see cref="ICustomAttributeProvider"/>
	/// </summary>
	public static class CustomAttributeProviderExtensions
	{

		/// <summary>
		///		Returns all attributes from a member implementing
		///		<see cref="ICustomAttributeProvider"/>
		/// </summary>
		public static T[] GetAttributes<T>(this ICustomAttributeProvider member)
			where T : Attribute
		{
			return member.GetAttributes<T>(false);
		}

		/// <summary>
		///		Returns all attributes from a member implementing
		///		<see cref="ICustomAttributeProvider"/>
		/// </summary>
		public static T[] GetAttributes<T>(this ICustomAttributeProvider member, bool inherit)
			where T : Attribute
		{
			return member.GetCustomAttributes(typeof (T), inherit) as T[];
		}

		/// <summary>
		///		Denotes if a member implementing <see cref="ICustomAttributeProvider"/>
		///		is annotated with a certain attribute
		/// </summary>
		public static bool HasAttribute<T>(this ICustomAttributeProvider member)
			where T : Attribute
		{
			return member.HasAttribute<T>(false);
		}

		/// <summary>
		///		Denotes if a member implementing <see cref="ICustomAttributeProvider"/>
		///		is annotated with a certain attribute
		/// </summary>
		public static bool HasAttribute<T>(this ICustomAttributeProvider member, bool inherit)
			where T : Attribute
		{
			return member.IsDefined(typeof (T), inherit);
		}
	}
}