#region

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace SharpFu.Domain.Persistence.Traits.Async
{
	public interface ICanAddAsync<in TEntity>
		where TEntity : class
	{
		Task Add(TEntity entity);
		Task Add(IEnumerable<TEntity> entities);
	}
}