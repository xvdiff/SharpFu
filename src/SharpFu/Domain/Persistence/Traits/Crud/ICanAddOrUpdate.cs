#region

using System.Collections.Generic;

#endregion

namespace SharpFu.Domain.Persistence.Traits.Crud
{
	
	public interface ICanAddOrUpdate<in TEntity>
		where TEntity : class
	{
		void AddOrUpdate(TEntity entity);
		void AddOrUpdate(IEnumerable<TEntity> entities);
		bool TryAddOrUpdate(TEntity entity);
		bool TryAddOrUpdate(IEnumerable<TEntity> entities);
	}
}