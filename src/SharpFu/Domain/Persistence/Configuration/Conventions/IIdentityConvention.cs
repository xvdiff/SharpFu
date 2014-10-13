using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Persistence.Configuration.Conventions
{

	/// <summary>
	///		Interface for identity selector
	///		conventions
	/// </summary>
	public interface IIdentityConvention
	{
		
		/// <summary>
		///		Applies the convention for specific
		///		entity / identity types
		/// </summary>
		/// <typeparam name="TEntity">Arbitary entity type</typeparam>
		/// <typeparam name="TIdentity">Arbitary identity type</typeparam>
		Expression<Func<TEntity, TIdentity>> Apply<TEntity, TIdentity>()
			where TEntity : class;

	}
}
