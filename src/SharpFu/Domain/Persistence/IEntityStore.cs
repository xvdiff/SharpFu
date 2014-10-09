using System;
using System.Linq.Expressions;
using SharpFu.Domain.Persistence.Caching;
using SharpFu.Domain.Persistence.Configuration;
using SharpFu.Domain.Persistence.Traits.Crud;

namespace SharpFu.Domain.Persistence
{

	/// <summary>
	///		Basic store which allows to insert
	///		and retrieve entities
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TIdentity">Identity type</typeparam>
	public interface IEntityStore<TEntity, TIdentity>
		: ICanAdd<TEntity>, ICanGetByIdentity<TEntity, TIdentity>
		where TEntity : class
	{

		/// <summary>
		///		Returns the type of the entity
		/// </summary>
		Type EntityType		{ get; }

		/// <summary>
		///		Returns the type of the identity
		/// </summary>
		Type IdentityType	{ get; }

		/// <summary>
		///		Gets or sets the caching strategy of the store
		/// </summary>
		ICachingStrategy<TEntity, TIdentity>	CachingStrategy		{ get; set; }

		/// <summary>
		///		Gets or sets the identity selector of the store
		/// </summary>
		Expression<Func<TEntity, TIdentity>>	IdentitySelector	{ get; set; }

	}
}
