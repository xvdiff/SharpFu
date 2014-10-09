using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Persistence.Configuration.Conventions
{

	/// <summary>
	///		Convention denoting that the name of the
	///		property must equal a certain pattern
	/// </summary>
	public class EqualsNameIdentityConvention : NameIdentityConventionBase
	{

		/// <summary>
		///		Creates a new instance of a <see cref="EqualsNameIdentityConvention"/>
		/// </summary>
		/// <param name="pattern">Pattern to check for equality</param>
		public EqualsNameIdentityConvention(string pattern)
			: base(name => name.Equals(pattern, StringComparison.CurrentCultureIgnoreCase))
		{
			
		}

	}
}
