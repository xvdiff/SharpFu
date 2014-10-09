#region

using System.Linq;

#endregion

namespace SharpFu.Domain.Persistence.Repositories.Queries
{

	/// <summary>
	///		Denotes query options for entities
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public interface IQueryOptions<TEntity>
	{

		/// <summary>
		///		Applies the query options
		///		on a <see cref="IQueryable{T}"/>
		/// </summary>
		IQueryable<TEntity> Apply(IQueryable<TEntity> query);
	}
}