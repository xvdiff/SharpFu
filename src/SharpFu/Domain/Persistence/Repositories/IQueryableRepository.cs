#region

using SharpFu.Domain.Persistence.Traits;

#endregion



namespace SharpFu.Domain.Persistence.Repositories
{
	public interface IQueryableRepository<TEntity> :
		ICanAggregate, ICanAggregateByQuery<TEntity>, ICanDeleteByQuery<TEntity>,
		ICanFindByQuery<TEntity>, ICanGetByQuery<TEntity>
		where TEntity : class
	{
	}
}