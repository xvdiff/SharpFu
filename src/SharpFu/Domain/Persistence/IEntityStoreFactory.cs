using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Domain.Persistence.Configuration;
using SharpFu.Domain.Services;

namespace SharpFu.Domain.Persistence
{

	/// <summary>
	///		Generic factory to create an arbitary entity store
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TIdentity">Identity type</typeparam>
	/// <typeparam name="TStore">Store type</typeparam>
	public interface IEntityStoreFactory<TEntity, TIdentity, out TStore> : IGenericFactory<TStore>
		where TStore : IEntityStore<TEntity, TIdentity>, new()
		where TEntity : class
	{

		/// <summary>
		///		Gets or sets the configuration for the factory
		/// </summary>
		IEntityStoreConfiguration<TEntity, TIdentity> Configuration { get; set; } 

	}
}
