#region

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

#endregion

namespace SharpFu.Caching
{
	/// <summary>
	///     Asynchronous in memory cache provider
	/// </summary>
	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly",
		Justification = "Implemented in base class")]
	public class InMemoryAsyncCacheProvider : InMemoryCacheProvider, IAsyncCacheProvider
	{
		public Task<T> GetAsync<T>(string key)
		{
#if NET_45_OR_GREATER
			return Task.Run(() => GetValue<T>(key));
#elif NET_40_OR_GREATER
			return Task.Factory.StartNew(() => GetValue<T>(key));
#endif
		}

		public Task PushAsync<T>(string key, T value, TimeSpan? lifespan = null)
		{
#if NET_45_OR_GREATER
			return Task.Run(() => InsertValue(key, value, lifespan));
#elif NET_40_OR_GREATER
			return Task.Factory.StartNew(() => InsertValue(key, value, lifespan));
#endif
		}

		public Task ClearAsync(string key)
		{
#if NET_45_OR_GREATER
			return Task.Run(() => ClearAsync(key));
#elif NET_40_OR_GREATER
			return Task.Factory.StartNew(() => ClearAsync(key));
#endif
		}
	}
}