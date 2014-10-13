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

	/// <summary>
	///		Configuration for a custom entity store
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TIdentity">Identity type</typeparam>
	public interface IEntityStoreConfiguration<TEntity, TIdentity>
		where TEntity : class
	{

		/// <summary>
		///		Returns a list of identity selector conventions
		/// </summary>
		List<IIdentityConvention> IdentityConventions { get; }

		/// <summary>
		///		Gets or sets the caching strategy for the store
		/// </summary>
		ICachingStrategy<TEntity, TIdentity> CachingStrategy { get; set; }


	}
}
