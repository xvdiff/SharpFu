using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Domain.Persistence.Caching;
using SharpFu.Domain.Persistence.Configuration.Conventions;

namespace SharpFu.Domain.Persistence.Configuration
{

	/// <summary>
	///		Default entity store configuration
	/// </summary>
	public class DefaultEntityStoreConfiguration<TEntity, TIdentity> : IEntityStoreConfiguration<TEntity, TIdentity>
		where TEntity : class
	{

		private readonly List<IIdentityConvention> _identityConventions
			= GlobalEntityStoreConfiguration.DefaultConventions;

		/// <summary>
		///		Returns the default identity conventions
		/// </summary>
		public List<IIdentityConvention> IdentityConventions
		{
			get { return _identityConventions; }
		}

		/// <summary>
		///		Returns the default caching strategy
		/// </summary>
		public ICachingStrategy<TEntity, TIdentity> CachingStrategy { get; set; }
	}
}
