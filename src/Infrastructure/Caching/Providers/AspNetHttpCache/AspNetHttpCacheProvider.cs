using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using SharpFu.Core.Guarding;

namespace SharpFu.Caching.Providers.AspNetHttpCache
{
	public class AspNetHttpCacheProvider : CacheProviderBase
	{

		private static Cache InternalCache
		{
			get { return HttpRuntime.Cache; }
		}

		public override void ClearAll()
		{
			var enumerator = InternalCache.GetEnumerator();
			while (enumerator.MoveNext())
				RemoveValue((string)enumerator.Key);
		}

		protected override T GetValue<T>(string key)
		{
			Guard.AgainstNullArgument(key);
			return (T)InternalCache.Get(key);
		}

		protected override void RemoveValue(string key)
		{
			Guard.AgainstNullOrEmpty(key, "key");
			InternalCache.Remove(key);
		}

		protected override void InsertValue<T>(string key, T value, TimeSpan? lifespan = null)
		{
			Guard.AgainstNullOrEmpty(key);

			InternalCache.Add(key, value, null, !lifespan.HasValue ? DateTime.MaxValue : DateTime.UtcNow.Add(lifespan.Value),
				TimeSpan.Zero, CacheItemPriority.Default,
				null);
		}

		protected override void Dispose(bool disposing)
		{
			
		}
	}
}
