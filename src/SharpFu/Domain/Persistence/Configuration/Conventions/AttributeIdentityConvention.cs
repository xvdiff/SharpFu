using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Extensions;

namespace SharpFu.Domain.Persistence.Configuration.Conventions
{

	/// <summary>
	///		Denotes that the identity property
	///		must be annoted with a certain attribute
	/// </summary>
	/// <typeparam name="TAttribute"></typeparam>
	public class AttributeIdentityConvention<TAttribute> : PropertyIdentityConventionBase
		where TAttribute : Attribute
	{
		
		/// <summary>
		///		Creates a new instance of <see cref="AttributeIdentityConvention{TAttribute}"/>
		/// </summary>
		public AttributeIdentityConvention()
			: base(p => p.HasAttribute<TAttribute>())
		{
			
		}

	}
}
