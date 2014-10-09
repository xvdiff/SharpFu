#region

using System;
using System.Linq.Expressions;

#endregion

namespace SharpFu.Domain.Persistence.Specifications
{
	/// <summary>
	///     Denotes a None specification based on
	///     a none predicate
	/// </summary>
	public sealed class NoneSpecification<T> : SpecificationBase<T>
	{
		public override Expression<Func<T, bool>> Predicate
		{
			get { return o => false; }
		}
	}
}