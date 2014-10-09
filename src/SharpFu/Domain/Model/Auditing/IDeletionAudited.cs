using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Model.Auditing
{
	/// <summary>
	///     Denotes that the deletion of
	///     the entity can be audited
	/// </summary>
	public interface IDeletionAudited : IArchivable
	{
		/// <summary>
		///     Denotes the time the entity
		///     has been deleted
		/// </summary>
		DateTime DeletedOn { get; }
	}
}
