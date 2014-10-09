using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace SharpFu.Persistence.Rhino
{
	public static class NHibernateSessionExtensions
	{
		public static void DeleteByIdentity<TEntity, TIdentity>(this ISession session, TIdentity identity)
			where TEntity : class
		{
			var metadata = session.SessionFactory.GetClassMetadata(typeof(TEntity));
			var hql = String.Format("delete {0} where id = :id", metadata.EntityName);
			var results = session.CreateQuery(hql).SetParameter("id", identity).ExecuteUpdate();

			if (results != 1)
				throw new BadImageFormatException();
		}
	}
}
