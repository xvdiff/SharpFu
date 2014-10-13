#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SharpFu.Core;
using SharpFu.Domain.Model.Conventions;
using SharpFu.Extensions;

#endregion

namespace SharpFu.Domain.Model
{
	[Serializable]
	public abstract class ValueObjectBase : ObjectBase
	{

		/// <summary>
		///		Returns the type properties of the current instance
		/// </summary>
		protected override IEnumerable<PropertyInfo> GetTypeProperties()
		{
			return UnproxiedType.GetProperties()
				.Where(x => x != null && x.HasAttribute<DomainSignatureAttribute>() 
				&& !x.GetIndexParameters().Any());
		}
	}
}