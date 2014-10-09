#region

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#endregion

namespace SharpFu.Domain.Persistence.Traits.Crud
{
	public interface ICanGet<out TEntity>
		where TEntity : class
	{
		IEnumerable<TEntity> GetAll();
	}

	public interface ICanGetByIdentity<TEntity, in TIdentity>
		where TEntity : class
	{
		TEntity Get(TIdentity identity);
		TResult Get<TResult>(TIdentity identity, Expression<Func<TEntity, TResult>> selector);
		bool TryGet(TIdentity identity, out TEntity entity);
		bool TryGet<TResult>(TIdentity identity, Expression<Func<TEntity, TResult>> selector, out TResult result);
	}
}