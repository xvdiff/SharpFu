#region

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace SharpFu.Domain.Persistence.Traits.Async
{

	/// <summary>
	///		Denotes that the store can
	///		update asynchronously
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public interface ICanUpdateAsync<in TEntity>
		where TEntity : class
	{
		/// <summary>
		///		Updates an entity asynchronously
		/// </summary>
		Task Update(TEntity entity);

		/// <summary>
		///		Updates a collection of entities
		///		asynchronously
		/// </summary>
		Task Update(IEnumerable<TEntity> entity);
	}
}