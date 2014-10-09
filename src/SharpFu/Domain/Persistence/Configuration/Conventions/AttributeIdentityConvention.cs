using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Extensions;

namespace SharpFu.Domain.Persistence.Configuration.Conventions
{

	[ComVisible(true)]
	public class AttributeIdentityConvention<TAttribute> : PropertyIdentityConventionBase
		where TAttribute : Attribute
	{

		public AttributeIdentityConvention()
			: base(p => p.HasAttribute<TAttribute>())
		{
			
		}

	}
}
