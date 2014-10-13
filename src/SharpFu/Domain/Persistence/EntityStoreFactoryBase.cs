using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;
using SharpFu.Core.Internal;
using SharpFu.Domain.Persistence.Configuration;

namespace SharpFu.Domain.Persistence
{

	/// <summary>
	///		Base class for entity store factories
	/// </summary>
	public abstract class EntityStoreFactoryBase<TEntity, TIdentity, TStore> : IEntityStoreFactory<TEntity, TIdentity, TStore>
		where TStore : IEntityStore<TEntity, TIdentity>, new()
		where TEntity : class
	{

		/// <summary>
		///		Gets or sets the current configuration
		/// </summary>
		public IEntityStoreConfiguration<TEntity, TIdentity> Configuration { get; set; }

		/// <summary>
		///		Creates a new instance of <see cref="EntityStoreFactoryBase{TEntity,TIdentity,TStore}"/>
		/// </summary>
		/// <param name="configuration"></param>
		protected EntityStoreFactoryBase(IEntityStoreConfiguration<TEntity, TIdentity> configuration)
		{
			Configuration = configuration ?? new DefaultEntityStoreConfiguration<TEntity, TIdentity>();
		} 
		
		/// <summary>
		///		Creates a new store based on the factory configuration
		/// </summary>
		public TStore Create()
		{

			var identitySelector =
				Configuration.IdentityConventions.Where(x => x != null)
					.Select(x => x.Apply<TEntity, TIdentity>()).First(x => x != null);
			
			var store = FastActivator.Create<TStore>();
			store.CachingStrategy	= Configuration.CachingStrategy;
			store.IdentitySelector	= identitySelector;

			return store;
		}

		
	}
}
