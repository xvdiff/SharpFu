using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Model
{
	/// <summary>
	///     Denotes special entities
	///     that act as access roots for
	///     clusters of domain models
	/// </summary>
	/// <typeparam name="TKey">
	///     Arbitary identity type
	/// </typeparam>
	public interface IAggregateRoot<TKey> : IEntity<TKey>
	{
	}

	/// <summary>
	///		Denotes an aggregate root with
	///		integer identity
	/// </summary>
	public interface IAggregateRoot : IAggregateRoot<int>
	{
	}

	/// <summary>
	///     Denotes an aggregate root
	///     that has a globally unique identifier
	/// </summary>
	public interface IGloballyUniqueAggregateRoot : IAggregateRoot<Guid>
	{
	}

	/// <summary>
	///     Denotes an aggregate root
	///     that has a keyed identifier
	/// </summary>
	public interface INaturalKeyedAggregateRoot : IAggregateRoot<string>
	{
	}
}
