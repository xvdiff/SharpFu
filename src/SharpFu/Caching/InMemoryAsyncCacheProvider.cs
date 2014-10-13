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
	public class InMemoryAsyncCacheProvider : InMemoryCacheProvider, IAsyncCacheProvider
	{
		public Task<T> GetAsync<T>(string key)
		{
			return Task.Run(() => GetValue<T>(key));
		}

		public Task PushAsync<T>(string key, T value, TimeSpan? lifespan = null)
		{
			return Task.Run(() => InsertValue(key, value, lifespan));
		}

		public Task ClearAsync(string key)
		{
			return Task.Run(() => ClearAsync(key));
		}

		protected override void Dispose(bool disposing)
		{
			// do nothing
		}
	}
}