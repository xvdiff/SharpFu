using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;

namespace SharpFu.Extensions
{
	public static class EnumerableExtensions
	{
		public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
		{
			return source.Any();
		}

		public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
		{
			foreach (var item in source)
				action(item);
		}

		public static void ForEach<TSource>(this IEnumerator<TSource> collection, Action<TSource> action)
		{
			while (collection.MoveNext())
				action(collection.Current);
		}

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
