using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Model
{
	/// <summary>
	///     Interface that allows entities
	///     to express the time when
	///     their lifecycle began
	/// </summary>
	public interface IHasCreationTime
	{
		/// <summary>
		///     Denotes the time the
		///     entity has been created
		/// </summary>
		DateTime CreatedOn { get; }
	}
}
