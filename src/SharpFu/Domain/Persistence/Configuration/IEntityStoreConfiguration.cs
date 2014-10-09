using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Domain.Persistence.Caching;
using SharpFu.Domain.Persistence.Configuration.Conventions;

namespace SharpFu.Domain.Persistence.Configuration
{
	public interface IEntityStoreConfiguration<TEntity, TIdentity>
		where TEntity : class
	{

		List<IIdentityConvention> IdentityConventions { get; }
		ICachingStrategy<TEntity, TIdentity> CachingStrategy { get; set; }


	}
}
