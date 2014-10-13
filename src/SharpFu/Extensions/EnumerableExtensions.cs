using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;

namespace SharpFu.Extensions
{

	/// <summary>
	///		Extension methods for <see cref="IEnumerable{T}"/>
	/// </summary>
	public static class EnumerableExtensions
	{

		/// <summary>
		///		Denotes if the current <see cref="IEnumerable{T}"/> is empty
		/// </summary>
		public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
		{
			return source.Any();
		}

		/// <summary>
		///		Executes a action for each item in a
		///		<see cref="IEnumerable{T}"/>
		/// </summary>
		public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
		{
			foreach (var item in source)
				action(item);
		}

		/// <summary>
		///		Executes a action for each item using a 
		///		<see cref="IEnumerator{T}"/>
		/// </summary>
		public static void ForEach<TSource>(this IEnumerator<TSource> collection, Action<TSource> action)
		{
			while (collection.MoveNext())
				action(collection.Current);
		}

		/// <summary>
		///		Attempts to execute a action for each item in a
		///		<see cref="IEnumerable{T}"/>
		/// </summary>
		public static void TryForEach<TSource>(this IEnumerable<TSource> collection, Action<TSource> action)
		{
			foreach (var item in collection)
			{
				try
				{
					action(item);
				}
				// ReSharper disable once EmptyGeneralCatchClause
				catch { }
			}
		}

		/// <summary>
		///		Attempts to execute a action for each item using a 
		///		<see cref="IEnumerator{T}"/>
		/// </summary>
		public static void TryForEach<TSource>(this IEnumerator<TSource> enumerator, Action<TSource> action)
		{
			while (enumerator.MoveNext())
			{
				try
				{
					action(enumerator.Current);
				}
				// ReSharper disable once EmptyGeneralCatchClause
				catch { }
			}
		}
	}
}
