using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Persistence.UnitOfWork
{

	/// <summary>
	///		Attribute denoting an unit of work
	/// </summary>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public class UnitOfWorkAttribute : Attribute
	{
	}
}
