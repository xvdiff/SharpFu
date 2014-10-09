#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SharpFu.Core.Guarding;
using SharpFu.Domain.Persistence.Caching;

#endregion

namespace SharpFu.Domain.Persistence.Repositories
{
	public abstract class CrudRepositoryBase<TEntity, TIdentity> :
		EntityStoreBase<TEntity, TIdentity>, 
		ICrudRepository<TEntity, TIdentity>
		where TEntity : class
	{

		protected CrudRepositoryBase()
			: this(null)
		{
			
		}

		protected CrudRepositoryBase(Expression<Func<TEntity, TIdentity>> identitySelector,
			ICachingStrategy<TEntity, TIdentity> cachingStrategy = null)
			: base(identitySelector, cachingStrategy)
		{
			
		}

		public virtual void Delete(TIdentity identity)
		{
			DeleteEntity(identity);
			CacheManager.OnEntityDeleted(identity);
		}

		public void Delete(IEnumerable<TIdentity> identities)
		{
			Delete(identities.ToArray());
		}

		public virtual void Delete(params TIdentity[] identities)
		{
			foreach(var identity in identities)
				Delete(identity);
		}

		public bool TryDelete(TIdentity key)
		{
			try
			{
				Delete(key);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool TryDelete(IEnumerable<TIdentity> identities)
		{
			return TryDelete(identities.ToArray());
		}

		public bool TryDelete(params TIdentity[] identities)
		{
			try
			{
				Delete(identities);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public virtual void Delete(TEntity entity)
		{
			Guard.AgainstNullArgument(entity);
			DeleteEntity(entity);

			TIdentity identity;
			if(TryGetIdentity(entity, out identity))
				CacheManager.OnEntityDeleted(identity);
		}

		public virtual void Delete(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
				Delete(entity);
		}

		public bool TryDelete(TEntity entity)
		{
			try
			{
				Delete(entity);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool TryDelete(IEnumerable<TEntity> entities)
		{
			var success = false;
			foreach (var entity in entities)
			{
				success = TryDelete(entity);
				if (success)
					continue;
				break;
			}

			return success;
		}

		public void AddOrUpdate(TEntity entity)
		{
			Guard.AgainstNullArgument(entity);
			AddOrUpdateEntity(entity);

			TIdentity identity;
			if(TryGetIdentity(entity, out identity))
				CacheManager.OnEntityAddedOrUpdated(identity, entity);
		}

		public virtual void AddOrUpdate(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
				AddOrUpdate(entity);
		}

		public bool TryAddOrUpdate(TEntity entity)
		{
			try
			{
				AddOrUpdate(entity);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool TryAddOrUpdate(IEnumerable<TEntity> entities)
		{
			try
			{
				AddOrUpdate(entities);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public IEnumerable<TEntity> GetAll()
		{
			return CacheManager.ExecuteGetAll(GetAllEntities, null, null);
		}

		public virtual void Update(TEntity entity)
		{
			Guard.AgainstNullArgument(entity);
			UpdateEntity(entity);

			TIdentity identity;
			if(TryGetIdentity(entity, out identity))
				CacheManager.OnEntityUpdated(identity, entity);
		}

		public virtual void Update(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
				Update(entity);
		}

		public bool TryUpdate(TEntity entity)
		{
			try
			{
				Update(entity);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool TryUpdate(IEnumerable<TEntity> entities)
		{
			try
			{
				Update(entities);
				return true;
			}
			catch
			{
				return false;
			}
		}

		protected abstract void UpdateEntity(TEntity entity);
		protected abstract void AddOrUpdateEntity(TEntity entity);
		protected abstract void DeleteEntity(TEntity entity);
		protected abstract void DeleteEntity(TIdentity key);
		protected abstract IEnumerable<TEntity> GetAllEntities();
	}
}