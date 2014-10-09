using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Persistence.Configuration.Conventions
{

	/// <summary>
	///		Identity convention for entity types
	/// </summary>
	public class TypeEqualsIdentityConvention<TType, TEntity, TIdentity> : TypeIdentityConvention<TEntity, TIdentity>
		where TEntity : class
	{

		/// <summary>
		///		Creates a new <see cref="TypeEqualsIdentityConvention{TType,TEntity,TIdentity}"/>
		/// </summary>
		public TypeEqualsIdentityConvention(Expression<Func<TEntity, TIdentity>> selector)
			: base(type => type == typeof(TType), selector)
		{
			
		} 

	}
}
