#region

using System;
using System.Linq.Expressions;
using SharpFu.Domain.Persistence.Specifications;

#endregion

namespace SharpFu.Domain.Persistence.Traits
{
	public interface ICanDeleteByQuery<TEntity>
		where TEntity : class
	{
		void Delete(Expression<Func<TEntity, bool>> predicate);
		void Delete(ISpecification<TEntity> criteria);
	}
}