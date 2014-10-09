#region

using System.Collections.Generic;

#endregion

namespace SharpFu.Domain.Persistence.Traits.Crud
{

	/// <summary>
	///		Persistence update trait
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public interface ICanUpdate<in TEntity>
		where TEntity : class
	{
		/// <summary>
		///		Updates an entity in the underlying
		///		storage
		/// </summary>
		/// <param name="entity">Entity to update</param>
		void Update(TEntity entity);
		void Update(IEnumerable<TEntity> entities);
		bool TryUpdate(TEntity entity);
		bool TryUpdate(IEnumerable<TEntity> entities);
	}
}