#region

using System;

#endregion

namespace SharpFu.Caching
{

	/// <summary>
	///		An empty cache provider which does
	///		absolutely nothing
	/// </summary>
	public class NullCacheProvider : CacheProviderBase
	{

		/// <summary>
		///		
		/// </summary>
		public override bool Exists(string key)
		{
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		public override void ClearAll()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		protected override T GetValue<T>(string key)
		{
			return default(T);
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void RemoveValue(string key)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void InsertValue<T>(string key, T value, TimeSpan? lifespan = null)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void Dispose(bool disposing)
		{
		}
	}
}