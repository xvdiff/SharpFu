using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Domain.Services;

namespace SharpFu.Domain.Persistence.UnitOfWork
{
	/// <summary>
	///		Factory for <see cref="IUnitOfWork"/>
	/// </summary>
	public interface IUnitOfWorkFactory : IGenericFactory<IUnitOfWork>
	{
	}
}
