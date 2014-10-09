#region

using System;

#endregion

namespace SharpFu.Domain.Services.Identity
{

	/// <summary>
	///		Generator for default <see cref="System.Guid"/>
	/// </summary>
	public class GuidGenerator : IIdentifierGenerator<Guid>
	{
		public Guid Generate()
		{
			var guid = Guid.Empty;
			while (Guid.Empty == guid)
			{
				guid = Guid.NewGuid();
			}

			return guid;
		}
	}
}