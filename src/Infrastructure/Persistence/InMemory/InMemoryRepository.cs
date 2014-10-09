using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;
using SharpFu.Domain.Persistence.Repositories;
using SharpFu.Domain.Persistence.Repositories.Exceptions;
using SharpFu.Domain.Services.Identity;
using SharpFu.Extensions;

namespace SharpFu.Persistence.InMemory
{
	public class InMemoryRepository<TEntity, TIdentity> : RepositoryBase<TEntity, TIdentity>
		where TEntity : class, new()
	{
		private readonly IIdentifierGenerator<TIdentity> _identifierGenerator;
		private readonly ConcurrentDictionary<TIdentity, TEntity> _items = new ConcurrentDictionary<TIdentity, TEntity>();

		public InMemoryRepository(IIdentifierGenerator<TIdentity> identifierGenerator)
			: this(identifierGenerator, null)
		{

		}

		public InMemoryRepository(IIdentifierGenerator<TIdentity> identifierGenerator,
			Expression<Func<TEntity, TIdentity>> keySelector)
			: base(keySelector)
		{
			_identifierGenerator = identifierGenerator;
		}


		private static IEnumerable<TEntity> CloneDictionary(ICollection<KeyValuePair<TIdentity, TEntity>> list)
		{
			// when you Google deep copy of generic list every answer uses either the IClonable interface on the TEntity or having the TEntity be Serializable
			//		since we can't really put those constraints on TEntity I'm going to do it via reflection
			var type = typeof(TEntity);
			var properties = type.GetProperties();

			var clonedList = new List<TEntity>(list.Count);

			foreach (var keyValuePair in list)
			{
				var newItem = new TEntity();
				foreach (var propInfo in properties.Where(propInfo => propInfo.CanWrite))
				{
					propInfo.SetValue(newItem, propInfo.GetValue(keyValuePair.Value, null), null);
				}

				clonedList.Add(newItem);
			}

			return clonedList;
		}

		protected override void AddEntity(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException("entity");

			var identity = SafeGetIdentity(entity);
			if (identity.Equals(default(TIdentity)))
			{
				var id = _identifierGenerator.Generate();
				entity.SetPropertyValue(IdentitySelector, id);
			}
			else
			{
				if (_items.ContainsKey(identity))
					throw new ConcurrencyException(EntityType, IdentityType, identity);
			}

			_items[SafeGetIdentity(entity)] = entity;
		}

		protected override void UpdateEntity(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException("entity");
			if (!_items.ContainsKey(SafeGetIdentity(entity)))
				throw new EntityNotFoundException(EntityType, IdentityType, entity.ToString());

			_items[SafeGetIdentity(entity)] = entity;
		}

		protected override void DeleteEntity(TEntity entity)
		{
			Guard.AgainstNullArgument(entity, "entity");

			DeleteEntity(SafeGetIdentity(entity));
		}

		protected override void DeleteEntity(TIdentity identity)
		{
			TEntity entity;
			if (!_items.TryRemove(identity, out entity))
				throw new EntityNotFoundException(EntityType, IdentityType, identity.ToString());
		}

		protected override void AddOrUpdateEntity(TEntity entity)
		{
			Guard.AgainstNullArgument(entity, "entity");

			_items[SafeGetIdentity(entity)] = entity;
		}

		protected override IQueryable<TEntity> ExecuteBaseQuery()
		{
			return CloneDictionary(_items).AsQueryable();
		}
	}
}
