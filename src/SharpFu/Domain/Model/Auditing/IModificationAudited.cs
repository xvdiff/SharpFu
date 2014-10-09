using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Model.Auditing
{
	/// <summary>
	///     Denotes that the modification of the
	///     entity can be audited
	/// </summary>
	public interface IModificationAudited
	{
		/// <summary>
		///     Denotes the time the entity was
		///     last modified
		/// </summary>
		DateTime LastModifiedOn { get; }
	}
}
