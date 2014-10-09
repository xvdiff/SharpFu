#region

using System.Collections.Generic;

#endregion

namespace SharpFu.Domain.Persistence.Traits.Crud
{

	/// <summary>
	///		Persistence trait for
	///		adding entities
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public interface ICanAdd<in TEntity>
		where TEntity : class
	{
		/// <summary>
		///		Adds an entity to the underlying storage
		/// </summary>
		/// <param name="entity">Entity to persist</param>
		void Add(TEntity entity);

		/// <summary>
		///		Adds a collection of entities
		///		to the underlying storage
		/// </summary>
		/// <param name="entities">Entities to persist</param>
		void Add(IEnumerable<TEntity> entities);
		
		/// <summary>
		///		Attempts to add an entity
		///		to the underlying storage
		/// </summary>
		/// <param name="entity">Entity to persist</param>
		/// <returns>True if successful, otherwise false</returns>
		bool TryAdd(TEntity entity);
		/// <summary>
		///		Attempts to add a collection
		///		of entities to the underlying storage
		/// </summary>
		/// <param name="entities">Entities to persist</param>
		/// <returns>True if successful, otherwise false</returns>
		bool TryAdd(IEnumerable<TEntity> entities);
	}
}