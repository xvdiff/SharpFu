#region

using SharpFu.Domain.Persistence.Traits;
using SharpFu.Domain.Persistence.Traits.Crud;

#endregion



namespace SharpFu.Domain.Persistence.Repositories
{
	public interface IRepository<TEntity, TIdentity> :
		ICrudRepository<TEntity, TIdentity>,
		ICanGet<TEntity>,
		ICanDelete<TEntity>,
		IQueryableRepository<TEntity>,
		ICanFindByIdentity<TIdentity>
		where TEntity : class
	{

		

	}
}