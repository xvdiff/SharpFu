using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Domain.Model.Conventions;
using SharpFu.Extensions;

namespace SharpFu.Domain.Persistence.Configuration.Conventions
{

	/// <summary>
	///		Identity selector convention
	///		which is being applied if a entity implements
	///		a defined interface
	/// </summary>
	public class InterfaceIdentityConvention<TInterface, TEntity, TIdentity> 
		: TypeIdentityConventionBase<TEntity, TIdentity>
		where TEntity : class 
	{

		/// <summary>
		///		Creates a new instance of a <see cref="InterfaceIdentityConvention{TInterface,TEntity,TIdentity}"/>
		/// </summary>
		public InterfaceIdentityConvention(Expression<Func<TEntity, TIdentity>> selector)
			: base(type => type.Implements<TInterface>(), selector)
		{
			
		} 

	}
}
