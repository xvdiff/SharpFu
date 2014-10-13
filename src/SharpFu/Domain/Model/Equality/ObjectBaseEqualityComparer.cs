using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core;

namespace SharpFu.Domain.Model.Equality
{
	/// <summary>
	///     Provides a comparer for supporting LINQ methods such as Intersect, Union and Distinct.
	///     This may be used for comparing objects of type <see cref="ObjectBase" /> and anything
	///     that derives from it, such as <see cref="EntityBase{TIdentity}" /> and <see cref="ValueObjectBase" />.
	///     NOTE:  Microsoft decided that set operators such as Intersect, Union and Distinct should
	///     not use the IEqualityComparer's Equals() method when comparing objects, but should instead
	///     use IEqualityComparer's GetHashCode() method.
	///     http://blogs.msmvps.com/jon_skeet/2010/12/30/reimplementing-linq-to-objects-part-14-distinct/
	/// </summary>
	public class ObjectBaseEqualityComparer<T> : IEqualityComparer<T>
		where T : ObjectBase
	{
		public bool Equals(T first, T second)
		{
			if (first == null && second == null)
				return true;

			if (first == null ^ second == null)
				return false;

			return first.Equals(second);
		}

		public int GetHashCode(T obj)
		{
			return obj.GetHashCode();
		}
	}
}
