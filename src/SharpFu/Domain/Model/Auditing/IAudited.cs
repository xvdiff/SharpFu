using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Model.Auditing
{
	/// <summary>
	///     Denotes an audited entity
	/// </summary>
	public interface IAudited :
		ICreationAudited,
		IModificationAudited,
		IDeletionAudited
	{
	}
}
