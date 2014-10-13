using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Extensions;

namespace SharpFu.Domain.Persistence.Configuration.Conventions
{

	/// <summary>
	///		Convention denoting that the type of the entity
	///		must derive from another type
	/// </summary>
	public class BaseTypeIdentityConvention<TBase, TEntity, TIdentity> : TypeIdentityConventionBase<TEntity, TIdentity>
		where TEntity : class 
	{

		/// <summary>
		///		Creates a new instance of <see cref="BaseTypeIdentityConvention{TBase,TEntity,TIdentity}"/>
		/// </summary>
		public BaseTypeIdentityConvention(Expression<Func<TEntity, TIdentity>> selector)
			: base(type => type.InheritsFrom<TBase>(), selector)
		{
			
		} 

	}

}
