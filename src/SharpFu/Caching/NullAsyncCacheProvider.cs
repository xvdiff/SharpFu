#region

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;

#endregion

namespace SharpFu.Caching
{
	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly",
		Justification = "Implemented in base class")]
	public class NullAsyncCacheProvider : NullCacheProvider, IAsyncCacheProvider
	{
		public Task<T> GetAsync<T>(string key)
		{
			Guard.AgainstNullOrEmpty(key, "key");
			return default(Task<T>);
		}

		public Task PushAsync<T>(string key, T value, TimeSpan? lifespan = null)
		{
			Guard.AgainstNullOrEmpty(key, "key");
			return default(Task);
		}

		public Task ClearAsync(string key)
		{
			Guard.AgainstNullOrEmpty(key, "key");
			return default(Task);
		}
	}
}