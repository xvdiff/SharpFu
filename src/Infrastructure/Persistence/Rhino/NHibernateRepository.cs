using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using SharpFu.Core.Guarding;
using SharpFu.Domain.Persistence.Repositories;

namespace SharpFu.Persistence.Rhino
{
	public class NHibernateRepository<TEntity, TIdentity> :
		RepositoryBase<TEntity, TIdentity>
		where TEntity : class
	{
		private readonly ISession _session;

		public NHibernateRepository(ISession session)
			: this(session, null)
		{

		}

		public NHibernateRepository(ISession session, Expression<Func<TEntity, TIdentity>> identitySelector)
			: base(identitySelector)
		{
			Guard.AgainstNullArgument(session, "session");
			_session = session;
		}

		protected ISession Session
		{
			get { return _session; }
		}

		protected override IQueryable<TEntity> ExecuteBaseQuery()
		{
			return _session.Query<TEntity>();
		}

		protected override void AddEntity(TEntity entity)
		{
			_session.Save(entity);
		}

		protected override void DeleteEntity(TEntity entity)
		{
			_session.Delete(entity);
		}

		protected override void DeleteEntity(TIdentity identity)
		{
			_session.DeleteByIdentity<TEntity, TIdentity>(identity);
		}

		protected override void UpdateEntity(TEntity entity)
		{
			_session.Update(entity);
		}

		protected override void AddOrUpdateEntity(TEntity entity)
		{
			_session.SaveOrUpdate(entity);
		}
	}
}
