#region

using System;

#endregion

namespace SharpFu.Domain.Model.Conventions
{
	/// <summary>
	///     Allows to declare signature properties
	///     on domain models that are being used
	///     to test equality
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class DomainSignatureAttribute : Attribute
	{
	}
}