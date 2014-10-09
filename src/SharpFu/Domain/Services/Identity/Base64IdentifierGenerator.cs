#region

using System;

#endregion

namespace SharpFu.Domain.Services.Identity
{
	/// <summary>
	///     Generates a new url friendly base64 string
	///     based on a <see cref="System.Guid" />
	/// </summary>
	public class Base64GuidGenerator : IIdentifierGenerator<string>
	{
		public string Generate()
		{
			var guid = new GuidGenerator().Generate();
			return Convert.ToBase64String(guid.ToByteArray())
				// replace special chars to guarantee 
				// uri friendliness
				.Replace("==", "")
				.Replace("/", "_")
				.Replace("+", "-");
		}
	}
}