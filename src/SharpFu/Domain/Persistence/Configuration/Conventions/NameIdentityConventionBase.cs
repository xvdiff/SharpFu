using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Persistence.Configuration.Conventions
{

	/// <summary>
	///		Identity convention by property name
	/// </summary>
	public abstract class NameIdentityConventionBase : PropertyIdentityConventionBase
	{

		/// <summary>
		///		Creates a new instance of <see cref="NameIdentityConventionBase"/>
		/// </summary>
		protected NameIdentityConventionBase(Func<string, bool> condition)
			: base(info => condition(info.Name))
		{
			
		}

	}
}
