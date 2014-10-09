#region

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace SharpFu.Domain.Persistence.Traits.Async
{
	public interface ICanAddOrUpdateAsync<in TEntity>
		where TEntity : class
	{
		Task AddOrUpdateAsync(TEntity entity);
		Task AddOrUpdateAsync(IEnumerable<TEntity> entities);
	}
}