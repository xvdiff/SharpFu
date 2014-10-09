using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db4objects.Db4o;
using SharpFu.Core.Guarding;
using SharpFu.Domain.Persistence.UnitOfWork;

namespace SharpFu.Persistence.Db4OEmbedded
{
	public class Db4oEmbeddedUnitOfWork : IUnitOfWork
	{

		private bool _commited;
		private readonly IObjectContainer _objectContainer;

		public Db4oEmbeddedUnitOfWork(IObjectContainer objectContainer)
		{
			Guard.AgainstNullArgument(objectContainer);
			_objectContainer = objectContainer;
		}

		~Db4oEmbeddedUnitOfWork()
		{
			Dispose(false);
		}
		
		public bool Commited
		{
			get { return _commited; }
		}

		public void Commit()
		{
			_objectContainer.Commit();
			_commited = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if(_objectContainer == null)
				return;

			if (disposing)
			{
				_objectContainer.Dispose();
			}
		}
	}
}
