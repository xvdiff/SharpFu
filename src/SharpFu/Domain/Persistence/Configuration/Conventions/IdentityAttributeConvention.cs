using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Domain.Model.Conventions;

namespace SharpFu.Domain.Persistence.Configuration.Conventions
{

	/// <summary>
	///		Identity selector convention
	///		which is being applied if a entity identity
	///		is annotated with the <see cref="IdentityAttribute"/>
	/// </summary>
	public class IdentityAttributeConvention : AttributeIdentityConvention<IdentityAttribute>
	{
	}
}
