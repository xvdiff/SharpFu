#region

using System;
using System.Threading.Tasks;

#endregion

namespace SharpFu.Caching
{
	/// <summary>
	///     Asynchronous interface methods
	///		for cache providers
	/// 
	///		<seealso cref="ICacheProvider"/>
	/// </summary>
	public interface IAsyncCacheProvider : ICacheProvider
	{
		/// <summary>
		///		Asynchronously retrieves a stored
		///		item from the cache provider
		/// </summary>
		/// <typeparam name="T">Arbitary item type</typeparam>
		/// <param name="key">Cache item key</param>
		/// <returns>Task result containing the retrieved item</returns>
		Task<T> GetAsync<T>(string key);

		/// <summary>
		///     Asynchronously pushes an item
		///     to the cache provider
		/// </summary>
		/// <typeparam name="T">Arbitary item type</typeparam>
		/// <param name="key">Cache item key</param>
		/// <param name="value">Item to cache</param>
		/// <param name="lifespan">Desired lifespan of the item</param>
		Task PushAsync<T>(string key, T value, TimeSpan? lifespan = null);

		/// <summary>
		///     Asynchronously removes an item
		///     from the cache provider
		/// </summary>
		/// <param name="key">Cache item key</param>
		Task ClearAsync(string key);
	}
}