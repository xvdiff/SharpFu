using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Core.Internal
{

	/// <summary>
	///		A faster alternative to <see cref="Activator.CreateInstance{T}"/>
	///		which caches the activation results in order to speed up the
	///		activation process when it is required to be called many times
	/// </summary>
	public static class FastActivator
	{

		private static readonly Dictionary<Type, Func<object>> FactoryCache = new Dictionary<Type, Func<object>>();

		/// <summary>
		///		Creates a new instance of a desired type
		/// </summary>
		/// <typeparam name="T">Arbitary type</typeparam>
		public static T Create<T>()
		{
			return TypeFactory<T>.CreateFunc.Invoke();
		}

		/// <summary>
		///		Creates a new instance of a desired type
		/// </summary>
		/// <param name="type">Arbitary type</param>
		public static object Create(Type type)
		{
			Func<object> f;

			if (FactoryCache.TryGetValue(type, out f)) 
				return f();

			lock (FactoryCache)
				if (!FactoryCache.TryGetValue(type, out f))
				{
					FactoryCache[type] = f = Expression.Lambda<Func<object>>(Expression.New(type)).Compile();
				}

			return f();
		}

		private static class TypeFactory<T>
		{
			public static readonly Func<T> CreateFunc 
				= Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
		}
	}
}
