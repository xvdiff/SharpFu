using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Model.Auditing
{
	/// <summary>
	///     Denotes that the creation of the
	///     entity can be audited
	/// </summary>
	public interface ICreationAudited : IHasCreationTime
	{
	}
}
