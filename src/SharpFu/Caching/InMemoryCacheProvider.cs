#region

using System;
using System.Collections;
using System.Linq;
#if NET35 || NET_35_OR_GREATER
using System.Web.Caching;
#else

using System.Runtime.Caching;
#endif
using SharpFu.Core.Guarding;

#endregion

namespace SharpFu.Caching
{

	/// <summary>
	///		Chache provider which persists items
	///		in a memory store
	/// </summary>
	public class InMemoryCacheProvider : CacheProviderBase
	{
		private readonly string _regionName;

		/// <summary>
		///		Creates a new instance of a <see cref="InMemoryCacheProvider"/>
		/// </summary>
		/// <param name="regionName">Optional cache region name</param>
		public InMemoryCacheProvider(string regionName = null)
		{
			_regionName = regionName;
		}

		private static ObjectCache InternalCache
		{
			get { return MemoryCache.Default; }
		}

		public override bool Exists(string key)
		{
			Guard.AgainstNullOrEmpty(key, "key");
			return InternalCache.Any(x => x.Key == key);
		}

		public override void ClearAll()
		{
			foreach (var key in InternalCache.Select(x => x.Key))
			RemoveValue(key);
		}

		protected override T GetValue<T>(string key)
		{
			Guard.AgainstNullOrEmpty(key, "key");
			return (T) InternalCache.Get(key, _regionName);
		}

		protected override void RemoveValue(string key)
		{
			Guard.AgainstNullOrEmpty(key, "key");
			InternalCache.Remove(key, _regionName);
		}

		protected override void InsertValue<T>(string key, T value, TimeSpan? lifespan = null)
		{
			Guard.AgainstNullOrEmpty(key, "key");
			var policy = new CacheItemPolicy();
			if (lifespan.HasValue)
				policy.AbsoluteExpiration = DateTime.UtcNow.Add(lifespan.Value);

			InternalCache.Set(key, value, policy);
		}

		/// <summary>
		///		Disposes the cache provider
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			// do nothing
		}
	}
}