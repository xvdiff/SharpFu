#region

using System;

#endregion

namespace SharpFu.Caching
{
	/// <summary>
	///     Interface for cache providers
	/// </summary>
	public interface ICacheProvider : IDisposable
	{
		/// <summary>
		///     Tries to retrieve a cached item from
		///     the cache provider and informs
		///     about a potential miss
		/// </summary>
		/// <typeparam name="T">Arbitary item type</typeparam>
		/// <param name="key">Cache item key</param>
		/// <param name="value">Retrieved value</param>
		/// <returns>
		///     True if the item was succesfully retrieved,
		///     false if it was missed
		/// </returns>
		bool Get<T>(string key, out T value);

		/// <summary>
		///     Denotes if a certain item is available
		///     inside the cache
		/// </summary>
		/// <param name="key">Cache item key</param>
		bool Exists(string key);

		/// <summary>
		///		Clears all cached items inside the cache
		///		provider
		/// </summary>
		void ClearAll();

		/// <summary>
		///		Clears an item from the cache provider
		/// </summary>
		/// <param name="key">Cache item key</param>
		void Clear(string key);

		/// <summary>
		///     Pushes an item to the cache provider
		/// </summary>
		/// <typeparam name="T">Arbitary item type</typeparam>
		/// <param name="key">Cache item key</param>
		/// <param name="value">Item to cache</param>
		/// <param name="lifespan">Desired lifespan of the item</param>
		void Push<T>(string key, T value, TimeSpan? lifespan = null);

		/// <summary>
		///		Increments a long value stored in the cache
		/// </summary>
		/// <param name="key">Cache item key</param>
		/// <param name="defaultValue">Default value</param>
		/// <param name="incrementValue">Value to increment</param>
		/// <returns>Incremented value</returns>
		long Increment(string key, long defaultValue, long incrementValue);

		/// <summary>
		///		Decrements a long value stored in the cache
		/// </summary>
		/// <param name="key">Cache item key</param>
		/// <param name="defaultValue">Default value</param>
		/// <param name="decrementValue">Value to decrement</param>
		/// <returns>Decremented value</returns>
		long Decrement(string key, long defaultValue, long decrementValue);
	}
}