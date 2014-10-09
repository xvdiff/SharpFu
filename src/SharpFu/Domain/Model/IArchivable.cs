using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Model
{
	/// <summary>
	///     Interface that allowes entities
	///     to be put in a transient deletion
	///     state
	/// </summary>
	public interface IArchivable
	{
		/// <summary>
		///     Denotes if the entity
		///     has been deleted
		/// </summary>
		bool IsDeleted { get; }
	}
}
