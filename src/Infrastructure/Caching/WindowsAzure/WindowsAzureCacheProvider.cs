using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationServer.Caching;
using SharpFu.Core.Guarding;

namespace SharpFu.Caching.Providers.WindowsAzure
{
	public class WindowsAzureCacheProvider : CacheProviderBase
	{

		private readonly DataCache _dataChache;

		protected DataCache DataCache
		{
			get { return _dataChache; }
		}

		public WindowsAzureCacheProvider(string cacheName = null)
			: this(new DataCacheFactory(), cacheName)
		{
			
		}

		public WindowsAzureCacheProvider(DataCacheFactoryConfiguration configuration, string cacheName = null)
			: this(new DataCacheFactory(configuration), cacheName)
		{
			
		}

		public WindowsAzureCacheProvider(DataCacheFactory cacheFactory, string cacheName = null)
		{
			Guard.AgainstNullArgument(cacheFactory);

			_dataChache = String.IsNullOrEmpty(cacheName)
				? cacheFactory.GetDefaultCache()
				: cacheFactory.GetCache(cacheName);
		}

		public WindowsAzureCacheProvider(DataCache dataCache)
		{
			Guard.AgainstNullArgument(dataCache);
			_dataChache = dataCache;
		}

		public override void ClearAll()
		{
			_dataChache.Clear();
		}

		public override long Increment(string key, long defaultValue, long incrementValue)
		{
			Guard.AgainstNullOrEmpty(key, "key");
			return _dataChache.Increment(key, incrementValue, defaultValue);
		}

		public override long Decrement(string key, long defaultValue, long decrementValue)
		{
			Guard.AgainstNullOrEmpty(key, "key");
			return _dataChache.Decrement(key, decrementValue, defaultValue);
		}

		protected override T GetValue<T>(string key)
		{
			Guard.AgainstNullOrEmpty(key, "key");
			return (T) _dataChache.Get(key);
		}

		protected override void RemoveValue(string key)
		{
			Guard.AgainstNullOrEmpty(key, "key");
			_dataChache.Remove(key);
		}

		protected override void InsertValue<T>(string key, T value, TimeSpan? lifespan = null)
		{
			Guard.AgainstNullOrEmpty(key, "key");

			if (!lifespan.HasValue)
				_dataChache.Put(key, value);
			else
				_dataChache.Put(key, value, lifespan.Value);
		}

		protected override void Dispose(bool disposing)
		{
			
		}
	}
}
