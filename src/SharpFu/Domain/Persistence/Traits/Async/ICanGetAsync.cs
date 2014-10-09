#region

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace SharpFu.Domain.Persistence.Traits.Async
{

	/// <summary>
	///		Denotes that the store
	///		can asynchronously get entities
	/// </summary>
	public interface ICanGetAsync<TEntity>
		where TEntity : class
	{

		/// <summary>
		///		Returns all available entities
		///		asynchronously
		/// </summary>
		Task<IEnumerable<TEntity>> GetAll();
	}

	/// <summary>
	///		Denotes that the store can asynchronously
	///		get entities by their identity
	/// </summary>
	public interface ICanGetAsyncByIdentity<TEntity, in TIdentity>
		where TEntity : class
	{

		/// <summary>
		///		Asynchronously returns an
		///		entity by its identity
		/// </summary>
		Task<TEntity> Get(TIdentity identity);
	}
}