using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Model
{

	/// <summary>
	///		Denotes that an item is persistable
	/// </summary>
	public interface IPersistable
	{

		/// <summary>
		///		Returns true if the item
		///		is currently transient
		/// </summary>
		bool IsTransient { get; }
	}
}
