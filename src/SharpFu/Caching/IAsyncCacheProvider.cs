#region

using System;
using System.Threading.Tasks;

#endregion

namespace SharpFu.Caching
{
	/// <summary>
	///     Denotes several asynchronous
	///     calls for cache providers
	/// </summary>
	public interface IAsyncCacheProvider : ICacheProvider
	{
		/// <summary>
		///     Asynchronously retrieves an cached item from
		///     the underlying store
		/// </summary>
		/// <typeparam name="T">Type of the cached item</typeparam>
		/// <param name="key">Cache item key</param>
		Task<T> GetAsync<T>(string key);

		/// <summary>
		///     Asynchronously pushes an object
		///     to the underlying store
		/// </summary>
		/// <typeparam name="T">Type of the object to cache</typeparam>
		/// <param name="key">Cache item key</param>
		/// <param name="value">Object to cache</param>
		/// <param name="lifespan">Desired lifespan of the item</param>
		Task PushAsync<T>(string key, T value, TimeSpan? lifespan = null);

		/// <summary>
		///     Asynchronously removes an item
		///     from the underlying store
		/// </summary>
		/// <param name="key">Cache item key</param>
		Task ClearAsync(string key);
	}
}