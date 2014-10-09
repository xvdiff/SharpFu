using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using SharpFu.Domain.Persistence.Repositories;
using SharpFu.Domain.Services.Identity;
using SharpFu.Extensions;

namespace SharpFu.Persistence.Db4oEmbedded
{
	public class Db4OEmbeddedRepository<TEntity, TIdentity> : RepositoryBase<TEntity, TIdentity>
		where TEntity : class 
	{

		private readonly IIdentifierGenerator<TIdentity> _identifierGenerator;
		private readonly IObjectContainer _objectContainer;
		
		public Db4OEmbeddedRepository(IObjectContainer objectContainer,
			IIdentifierGenerator<TIdentity> identifierGenerator)
			: this(objectContainer, identifierGenerator, null)
		{
		}

		public Db4OEmbeddedRepository(IObjectContainer objectContainer,
			IIdentifierGenerator<TIdentity> identifierGenerator, Expression<Func<TEntity, TIdentity>> keySelector)
			: base(keySelector)
		{
			if (objectContainer == null)
				throw new ArgumentNullException("objectContainer");

			_objectContainer = objectContainer;
			_identifierGenerator = identifierGenerator;
		}

		protected override TEntity GetEntity(TIdentity identity)
		{
			return ExecuteBaseQuery().Single(x => MatchOnIdentity(x, identity));
		}

		protected override IQueryable<TEntity> ExecuteBaseQuery()
		{
			return _objectContainer.AsQueryable<TEntity>();
		}

		protected override void UpdateEntity(TEntity entity)
		{
			_objectContainer.Store(entity);
		}

		protected override void AddOrUpdateEntity(TEntity entity)
		{
			_objectContainer.Store(entity);
		}

		protected override void DeleteEntity(TEntity entity)
		{
			_objectContainer.Delete(entity);
		}

		protected override void DeleteEntity(TIdentity identity)
		{
			Delete(Get(identity));
		}

		protected override void AddEntity(TEntity entity)
		{
			if (_identifierGenerator != null)
			{
				TIdentity identity;
				if (TryGetIdentity(entity, out identity))
				{
					if (Equals(identity, default(TIdentity)))
					{
						identity = _identifierGenerator.Generate();
						entity.SetPropertyValue(IdentitySelector, identity);
					}
				}
			}

			_objectContainer.Store(entity);
		}

		private bool MatchOnIdentity(TEntity entity, TIdentity keyValue)
		{
			TIdentity identity;
			var actualKey = TryGetIdentity(entity, out identity);
			return keyValue.Equals(actualKey);
		}
	}
}
