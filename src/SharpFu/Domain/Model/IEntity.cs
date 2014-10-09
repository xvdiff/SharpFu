using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Model
{
	/// <summary>
	///     Interface for entities (domain models) that
	///     have a strongly typed identity
	/// </summary>
	/// <typeparam name="TKey">
	///     Arbitary entity type
	/// </typeparam>
	public interface IEntity<TKey> : IPersistable,
		IEquatable<IEntity<TKey>>
	{
		/// <summary>
		///     Denotes the identity of the domain
		///     model
		/// </summary>
		TKey Id { get; }
	}

	public interface IEntity : IEntity<int>
	{
	}
}
