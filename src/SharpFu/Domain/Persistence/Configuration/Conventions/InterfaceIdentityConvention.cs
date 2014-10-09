using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Extensions;

namespace SharpFu.Domain.Persistence.Configuration.Conventions
{
	public class InterfaceIdentityConvention<TInterface, TEntity, TIdentity> 
		: TypeIdentityConvention<TEntity, TIdentity>
		where TEntity : class 
	{

		public InterfaceIdentityConvention(Expression<Func<TEntity, TIdentity>> selector)
			: base(type => type.Implements<TInterface>(), selector)
		{
			
		} 

	}
}
