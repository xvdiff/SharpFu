#region

using System;
using SharpFu.Core.Guarding;

#endregion

namespace SharpFu.Caching
{
	/// <summary>
	///     Base class for cache providers
	/// </summary>
	public abstract class CacheProviderBase : ICacheProvider
	{
		protected static readonly object SyncRoot = new object();

		~CacheProviderBase()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public bool Get<T>(string key, out T value)
		{
			Guard.AgainstNullOrEmpty(key);

			value = default(T);
			try {
				if (!Exists(key))
					return false;

				value = GetValue<T>(key);
				return true;
			} catch {
				return false;
			}
		}

		public virtual bool Exists(string key)
		{
			Guard.AgainstNullArgument(key);
			object result;
			return Get(key, out result);
		}

		public abstract void ClearAll();

		public void Clear(string key)
		{
			Guard.AgainstNullOrEmpty(key, "key");
			RemoveValue(key);
		}

		public void Push<T>(string key, T value, TimeSpan? lifespan = null)
		{
			Guard.AgainstNullOrEmpty(key, "key");
			InsertValue(key, value, lifespan);
		}

		public virtual long Increment(string key, long defaultValue, long incrementValue)
		{
			Guard.AgainstNullOrEmpty(key, "key");

			lock (SyncRoot)
			{
				long current;
				if (!Get(key, out current))
					current = defaultValue;

				var @new = current + incrementValue;
				Push(key, @new);

				return @new;
			}
		}

		public virtual long Decrement(string key, long defaultValue, long decrementValue)
		{
			Guard.AgainstNullOrEmpty(key, "key");

			lock (SyncRoot)
			{
				long current;
				if (!Get(key, out current))
					current = defaultValue;

				var @new = current - decrementValue;
				Push(key, @new);

				return @new;
			}
		}

		protected abstract T GetValue<T>(string key);
		protected abstract void RemoveValue(string key);
		protected abstract void InsertValue<T>(string key, T value, TimeSpan? lifespan = null);
		protected abstract void Dispose(bool disposing);
	}
}