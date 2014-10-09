using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SharpFu.Core.Guarding;
using SharpFu.Domain.Persistence.Caching;
using SharpFu.Domain.Persistence.Configuration;
using SharpFu.Domain.Persistence.Repositories;
using SharpFu.Domain.Persistence.Repositories.Exceptions;

namespace SharpFu.Domain.Persistence
{

	/// <summary>
	///		Base implementation of an entity store
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TIdentity">Identity type</typeparam>
	public abstract class EntityStoreBase<TEntity, TIdentity> : IEntityStore<TEntity, TIdentity>
		where TEntity: class
	{

		private EntityCacheManager<TEntity, TIdentity> _cacheManager;
		private Expression<Func<TEntity, TIdentity>> _identitySelector;

		public Type EntityType
		{
			get { return typeof (TEntity); }
		}

		public Type IdentityType
		{
			get { return typeof (TIdentity); }
		}

		public ICachingStrategy<TEntity, TIdentity> CachingStrategy
		{
			get { return _cacheManager.CachingStrategy; }
			set {
				_cacheManager = new EntityCacheManager<TEntity, TIdentity>(value);
			}
		}

		public Expression<Func<TEntity, TIdentity>> IdentitySelector
		{
			get { return _identitySelector; }
			set { _identitySelector = value ?? 
				GlobalEntityStoreConfiguration.GetDefaultIdentitySelector<TEntity, TIdentity>(); 
			}
		}

		protected EntityCacheManager<TEntity, TIdentity> CacheManager
		{
			get { return _cacheManager; }
		}

		protected EntityStoreBase()
			: this(null)
		{
			
		}

		protected EntityStoreBase(Expression<Func<TEntity, TIdentity>> identitySelector, 
			ICachingStrategy<TEntity, TIdentity> cachingStrategy = null)
		{
			IdentitySelector	= identitySelector;
			CachingStrategy		= cachingStrategy;
		}

		/// <summary>
		///		Adds an entity to the store
		/// </summary>
		/// <param name="entity">Entity to add</param>
		/// <exception cref="ArgumentNullException">
		///		Thrown when the entity is null
		/// </exception>
		/// <exception cref="ConcurrencyException">
		///		Thrown when an entity with the same
		///		identity is already persisted
		/// </exception>
		public virtual void Add(TEntity entity)
		{
			Guard.AgainstNullArgument(entity, "entity");

			AddEntity(entity);

			TIdentity identity;
			if(TryGetIdentity(entity, out identity))
				_cacheManager.OnEntityAdded(identity, entity);
		}

		/// <summary>
		///		Adds a collection of entities
		///		to the store
		/// </summary>
		/// <param name="entities">Collection of entities</param>
		/// <exception cref="ArgumentNullException">
		///		Thrown when an entity is null
		/// </exception>
		/// <exception cref="ConcurrencyException">
		///		Thrown when an entity with the same
		///		identity is already persisted
		/// </exception>
		public virtual void Add(IEnumerable<TEntity> entities)
		{
			foreach(var entity in entities)
				Add(entity);
		}

		/// <summary>
		///		Attempts to add an entity to the store
		/// </summary>
		/// <param name="entity">Entity to add</param>
		/// <returns>
		///		True if the entity has been successfully added, 
		///		otherwise false
		/// </returns>
		public bool TryAdd(TEntity entity)
		{
			try {
				Add(entity);
				return true;
			} catch {
				return false;
			}
		}

		/// <summary>
		///		Attempts to add a collection of entities
		///		to the store
		/// </summary>
		/// <param name="entities">Collection of entities</param>
		/// <returns>
		///		True if all entities have been successfully added,
		///		otherwise false
		/// </returns>
		public bool TryAdd(IEnumerable<TEntity> entities)
		{
			try
			{
				Add(entities);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		///		Returns an entity from the store
		///		by its identity
		/// </summary>
		/// <param name="identity">Identity to lookup</param>
		/// <exception cref="EntityNotFoundException">
		///		Thrown when no entity which the desired
		///		identity is available
		/// </exception>
		public virtual TEntity Get(TIdentity identity)
		{
			return _cacheManager.ExecuteGet(() => GetEntity(identity), identity);
		}

		/// <summary>
		///		Returns a selected value from a entity
		///		from the store by its identity
		/// </summary>
		/// <typeparam name="TResult">Type of the selected value</typeparam>
		/// <param name="identity">Identity to lookup</param>
		/// <param name="selector">Value selector expression</param>
		/// <exception cref="EntityNotFoundException">
		///		Thrown when no entity which the desired
		///		identity is available
		/// </exception>
		public virtual TResult Get<TResult>(TIdentity identity, Expression<Func<TEntity, TResult>> selector)
		{
			var result = _cacheManager.ExecuteGet(() => GetEntity(identity), identity);
			var selectedResult = result == null ? default(TResult) : new[] 
				{result}.AsQueryable().Select(selector).First();
			return selectedResult;
		}

		/// <summary>
		///		Tries to retrieve an entity from
		///		the store by its identity
		/// </summary>
		/// <param name="identity">Identity to lookup</param>
		/// <param name="entity">Entity result</param>
		/// <returns>
		///		True if the identity could have been retrieved,
		///		otherwise false
		/// </returns>
		public bool TryGet(TIdentity identity, out TEntity entity)
		{
			entity = default(TEntity);
			try
			{
				entity = Get(identity);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		///		Tries to retrieve a selected value
		///		from an entity from the store by
		///		its identity
		/// </summary>
		/// <typeparam name="TResult">Type of the selected value</typeparam>
		/// <param name="identity">Identity to lookup</param>
		/// <param name="selector">Value selector expression</param>
		/// <param name="result">Value result</param>
		/// <returns>
		///		True if the value could have been retrieved,
		///		otherwise false
		/// </returns>
		public bool TryGet<TResult>(TIdentity identity, Expression<Func<TEntity, TResult>> selector, out TResult result)
		{
			result = default(TResult);
			try
			{
				result = Get(identity, selector);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		///		Applies the identity selector on
		///		an entity in order to retrieve its
		///		identity
		/// </summary>
		/// <param name="entity">The entity the identity selector gets applied on</param>
		/// <param name="identity">Identity result</param>
		/// <returns>True if the identity could have been retrieved, otherwise false</returns>
		protected bool TryGetIdentity(TEntity entity, out TIdentity identity)
		{
			identity = default(TIdentity);
			try
			{
				var func = _identitySelector.Compile();
				identity = func(entity);

				return true;
			} catch {
				return false;
			}
		}

		protected TIdentity SafeGetIdentity(TEntity entity)
		{
			TIdentity identity;
			TryGetIdentity(entity, out identity);
			return identity;
		}

		/// <summary>
		///		Adds an entity to the store
		/// </summary>
		protected abstract void AddEntity(TEntity entity);

		/// <summary>
		///		Returns an entity from the store
		/// </summary>
		protected abstract TEntity GetEntity(TIdentity identity);

		/// <summary>
		///		Returns a value from an entity from
		///		the store
		/// </summary>
		protected abstract TResult GetEntity<TResult>(TIdentity identity, Expression<Func<TEntity, TResult>> selector);

	}
}
