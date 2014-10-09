using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Domain.Persistence.Caching;
using SharpFu.Domain.Persistence.Configuration.Conventions;

namespace SharpFu.Domain.Persistence.Configuration
{
	public class DefaultEntityStoreConfiguration<TEntity, TIdentity> : IEntityStoreConfiguration<TEntity, TIdentity>
		where TEntity : class
	{

		private readonly List<IIdentityConvention> _identityConventions
			= GlobalEntityStoreConfiguration.DefaultConventions;

		public List<IIdentityConvention> IdentityConventions
		{
			get { return _identityConventions; }
		}

		public ICachingStrategy<TEntity, TIdentity> CachingStrategy { get; set; }
	}
}
