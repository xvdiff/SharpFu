using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;
using SharpFu.Domain.Persistence.Repositories;
using SharpFu.Extensions;

namespace SharpFu.Persistence.EntityFramework
{
	public class EfRepository<TEntity, TIdentity> : RepositoryBase<TEntity, TIdentity>
		where TEntity : class, new()
	{
		private readonly DbContext _dbContext;
		private readonly DbSet<TEntity> _set;


		public EfRepository(ObjectContext objectContext, bool contextOwnsConnection)
			: this(new DbContext(objectContext, contextOwnsConnection))
		{

		}

		public EfRepository(DbConnection existingConnection, bool contextOwnsConnection)
			: this(new DbContext(existingConnection, contextOwnsConnection))
		{

		}

		public EfRepository(DbContext dbContext, Expression<Func<TEntity, TIdentity>> identitySelector = null)
			: base(identitySelector)
		{
			Guard.AgainstNullArgument(dbContext);

			_dbContext	= dbContext;
			_set		= dbContext.Set<TEntity>();
		}

		protected DbSet<TEntity> Set
		{
			get { return _set; }
		}

		public override void Add(IEnumerable<TEntity> entities)
		{
			_set.AddRange(entities);
		}

		protected override void AddEntity(TEntity entity)
		{
			_set.Add(entity);
		}

		protected override void AddOrUpdateEntity(TEntity entity)
		{
			_set.AddOrUpdate(entity);
		}

		protected override void DeleteEntity(TEntity entity)
		{
			_set.Attach(entity);
			_set.Remove(entity);
		}

		public override void Delete(IEnumerable<TEntity> entities)
		{
			_set.RemoveRange(entities);
		}

		protected override void DeleteEntity(TIdentity key)
		{
			var stub = new TEntity();
			stub.SetPropertyValue(IdentitySelector, key);

			DeleteEntity(stub);
		}

		protected override void UpdateEntity(TEntity entity)
		{
			_set.Attach(entity);
			var entry = _dbContext.Entry(entity);
			entry.State = EntityState.Modified;
		}

		protected override IQueryable<TEntity> ExecuteBaseQuery()
		{
			return _set.AsQueryable();
		}
	}
}
