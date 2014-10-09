#region

using SharpFu.Domain.Persistence.Traits.Crud;

#endregion



namespace SharpFu.Domain.Persistence.Repositories
{
	public interface ICrudRepository<TEntity, TIdentity> :
		IEntityStore<TEntity, TIdentity>,
		ICanDeleteByIdentity<TIdentity>,
		ICanUpdate<TEntity>,
		ICanAddOrUpdate<TEntity>
		where TEntity : class
	{
		
	}
}