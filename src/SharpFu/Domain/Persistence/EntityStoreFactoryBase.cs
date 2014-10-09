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
	public abstract class EntityStoreFactoryBase<TEntity, TIdentity, TStore> : IEntityStoreFactory<TEntity, TIdentity, TStore>
		where TStore : IEntityStore<TEntity, TIdentity>, new()
		where TEntity : class
	{

		public IEntityStoreConfiguration<TEntity, TIdentity> Configuration { get; set; }

		protected EntityStoreFactoryBase(IEntityStoreConfiguration<TEntity, TIdentity> configuration)
		{
			Configuration = configuration ?? new DefaultEntityStoreConfiguration<TEntity, TIdentity>();
		} 
		
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
