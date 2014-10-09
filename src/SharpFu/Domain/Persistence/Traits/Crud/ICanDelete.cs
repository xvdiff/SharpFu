#region

using System.Collections.Generic;

#endregion

namespace SharpFu.Domain.Persistence.Traits.Crud
{

	/// <summary>
	///		Denotes that the store can delete entities
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public interface ICanDelete<in TEntity>
		where TEntity : class
	{

		/// <summary>
		///		Deletes an entity from the underlying store
		/// </summary>
		/// <param name="entity">Entity to delete</param>
		void Delete(TEntity entity);

		/// <summary>
		///		Deletes a collection of entities
		///		from the underlying store
		/// </summary>
		/// <param name="entities">Collection of entities</param>
		void Delete(IEnumerable<TEntity> entities);

		/// <summary>
		///		Attempts to delete an entity from
		///		the underlying store
		/// </summary>
		/// <param name="entity">Entity to delete</param>
		bool TryDelete(TEntity entity);

		/// <summary>
		///		Attempts to delete a collection
		///		of entities from the underlying store
		/// </summary>
		bool TryDelete(IEnumerable<TEntity> entities);
	}

	/// <summary>
	///		Denotes that the store can delete entities
	///		by their identities
	/// </summary>
	/// <typeparam name="TIdentity"></typeparam>
	public interface ICanDeleteByIdentity<in TIdentity>
	{
		
		/// <summary>
		///		Deletes an entity by its identity
		/// </summary>
		/// <param name="identity">Identity</param>
		void Delete(TIdentity identity);

		/// <summary>
		///		Deletes a collection of entities
		///		by their identities
		/// </summary>
		void Delete(IEnumerable<TIdentity> identities);

		/// <summary>
		///		Deletes a collection of entities
		///		by their identities
		/// </summary>
		void Delete(params TIdentity[] identities);

		/// <summary>
		///		Attempts to delete an entity
		///		by its identity
		/// </summary>
		bool TryDelete(TIdentity identity);

		/// <summary>
		///		Attempts to delete a collection
		///		of entities by their identities
		/// </summary>
		bool TryDelete(IEnumerable<TIdentity> identities);

		/// <summary>
		///		Attempts to delete a collection
		///		of entities by their identities
		/// </summary>
		bool TryDelete(params TIdentity[] identities);
	}
}