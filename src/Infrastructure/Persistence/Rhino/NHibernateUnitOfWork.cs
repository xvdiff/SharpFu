using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using SharpFu.Core.Guarding;
using SharpFu.Domain.Persistence.UnitOfWork;

namespace SharpFu.Persistence.Rhino
{
	public class NHibernateUnitOfWork : IUnitOfWork
	{
		private readonly ISession _session;
		private bool _diposed;

		public NHibernateUnitOfWork(ISession session)
		{
			Guard.AgainstNullArgument(session);
			_session = session;
		}

		protected ISession Session
		{
			get { return _session; }
		}

		public virtual bool Commited { get; private set; }

		public virtual void Commit()
		{
			if (Commited)
				throw new InvalidOperationException("Unit of work already commited.");
			_session.Flush();
			Commited = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool dispose)
		{
			if (_diposed)
				return;

			_diposed = true;
		}
	}
}
