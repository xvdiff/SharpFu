#region

using System;
using System.Linq.Expressions;

#endregion

namespace SharpFu.Domain.Persistence.Specifications
{
	/// <summary>
	///     Denotes a Any specification based on
	///     a any predicate
	/// </summary>
	public sealed class AnySpecification<T> : SpecificationBase<T>
	{
		public override Expression<Func<T, bool>> Predicate
		{
			get { return o => true; }
		}
	}
}