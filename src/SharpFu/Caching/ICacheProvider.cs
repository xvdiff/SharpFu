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
		///     the underlying store and informs
		///     about a potential miss
		/// </summary>
		/// <typeparam name="T">Cache item type</typeparam>
		/// <param name="key">Cache item key</param>
		/// <param name="value">Retrieved value</param>
		/// <returns>
		///     True if the item was succesfully retrieved,
		///     false if it was missed
		/// </returns>
		bool Get<T>(string key, out T value);

		/// <summary>
		///     Denotes if a certain item is available
		///     inside the cahce
		/// </summary>
		/// <param name="key">Cache item key</param>
		bool Exists(string key);

		void ClearAll();
		void Clear(string key);
		void Push<T>(string key, T value, TimeSpan? lifespan = null);

		long Increment(string key, long defaultValue, long incrementValue);
		long Decrement(string key, long defaultValue, long decrementValue);
	}
}