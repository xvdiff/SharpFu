using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Core.Internal
{
	public static class FastActivator
	{
		private static readonly Dictionary<Type, Func<object>> factoryCache = new Dictionary<Type, Func<object>>();

		public static T Create<T>()
		{
			return TypeFactory<T>.Create();
		}

		public static object Create(Type type)
		{
			Func<object> f;

			if (factoryCache.TryGetValue(type, out f)) 
				return f();

			lock (factoryCache)
				if (!factoryCache.TryGetValue(type, out f))
				{
					factoryCache[type] = f = Expression.Lambda<Func<object>>(Expression.New(type)).Compile();
				}

			return f();
		}

		private static class TypeFactory<T>
		{
			// ReSharper disable once MemberHidesStaticFromOuterClass
			public static readonly Func<T> Create = Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
		}
	}
}
