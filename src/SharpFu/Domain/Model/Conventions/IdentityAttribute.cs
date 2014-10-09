#region

using System;

#endregion

namespace SharpFu.Domain.Model.Conventions
{

	/// <summary>
	///		Attribute denoting an identity
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class IdentityAttribute : Attribute
	{
		/*public int Order { get; set; }*/
	}
}