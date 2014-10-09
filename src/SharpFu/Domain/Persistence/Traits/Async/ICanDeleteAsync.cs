#region

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace SharpFu.Domain.Persistence.Traits.Async
{

	/// <summary>
	///		Denotes that the store can
	///		delete entities asynchronously
	/// </summary>
	public interface ICanDeleteAsync<in TEntity>
		where TEntity : class
	{

		/// <summary>
		///		Deletes an entity asynchronously
		/// </summary>
		Task DeleteAsync(TEntity entity);

		/// <summary>
		///		Deletes a collection of entities
		///		asynchronously
		/// </summary>
		Task DeleteAsync(IEnumerable<TEntity> entities);
	}

	/// <summary>
	///		Denotes that the store can
	///		delete entities asynchronously
	///		by their identities
	/// </summary>
	public interface ICanDeleteAsyncByIdentity<in TIdentity>
	{

		/// <summary>
		///		Deletes an entity asynchronously
		///		by its identity
		/// </summary>
		Task DeleteAsync(TIdentity identity);

		/// <summary>
		///		Deletes a collection of entities
		///		asynchronously by their identities
		/// </summary>
		Task DeleteAsync(IEnumerable<TIdentity> identities);

		/// <summary>
		///		Deletes a collection of entities
		///		asynchronously by their identities
		/// </summary>
		Task DeleteAsync(params TIdentity[] identities);
	}
}