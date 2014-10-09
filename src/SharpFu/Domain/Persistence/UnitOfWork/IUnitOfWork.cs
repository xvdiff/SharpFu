using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Persistence.UnitOfWork
{

	/// <summary>
	///		Interface for units of work
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		
		/// <summary>
		///		Denotes if the unit of work has 
		///		been commited
		/// </summary>
		bool Commited { get; }

		/// <summary>
		///		Commits the changes inside the
		///		unit of work to the underlying store
		/// </summary>
		void Commit();
	}
}
