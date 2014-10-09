using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Domain.Persistence.Configuration;

namespace SharpFu.Domain.Persistence
{

	/// <summary>
	///		Default factory for entity stores
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TIdentity">Identity type</typeparam>
	/// <typeparam name="TStore">Store type</typeparam>
	public class DefaultEntityStoreFactory<TEntity, TIdentity, TStore> : EntityStoreFactoryBase<TEntity, TIdentity, TStore>
		where TStore : IEntityStore<TEntity, TIdentity>, new()
		where TEntity : class 
	{

		/// <summary>
		///		Creates a new instance of a <see cref="DefaultEntityStoreFactory{TEntity,TIdentity,TStore}"/>
		///		using <see cref="DefaultEntityStoreConfiguration{TEntity,TIdentity}"/>
		/// </summary>
		public DefaultEntityStoreFactory()
			: base(new DefaultEntityStoreConfiguration<TEntity, TIdentity>())
		{
			
		} 

	}
}
