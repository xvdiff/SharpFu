using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Persistence.Configuration.Conventions
{
	public interface IIdentityConvention
	{

		Expression<Func<TEntity, TIdentity>> Apply<TEntity, TIdentity>()
			where TEntity : class;

	}
}
