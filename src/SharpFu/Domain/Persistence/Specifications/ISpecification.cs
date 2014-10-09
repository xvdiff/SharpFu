#region

using System;
using System.Linq;
using System.Linq.Expressions;
using SharpFu.Core.Internal;

#endregion

namespace SharpFu.Domain.Persistence.Specifications
{
	/// <summary>
	///     Specification interface
	/// </summary>
	public interface ISpecification<T> : IFluentInterface
	{
		Expression<Func<T, bool>> Predicate { get; }
		bool IsSatisfiedBy(T obj);
		ISpecification<T> Any();
		T SatisfyingEntityFrom(IQueryable<T> query);
		IQueryable<T> SatisfyingEntitiesFrom(IQueryable<T> query);
	}
}