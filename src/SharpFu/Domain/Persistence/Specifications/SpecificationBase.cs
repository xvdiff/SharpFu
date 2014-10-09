#region

using System;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace SharpFu.Domain.Persistence.Specifications
{
	/// <summary>
	///     Base class for generic specifications
	/// </summary>
	public abstract class SpecificationBase<T> : ISpecification<T>
	{
		public abstract Expression<Func<T, bool>> Predicate { get; }

		public virtual bool IsSatisfiedBy(T obj)
		{
			return Predicate.Compile()(obj);
		}

		public T SatisfyingEntityFrom(IQueryable<T> query)
		{
			return SatisfyingEntitiesFrom(query).FirstOrDefault();
		}

		public IQueryable<T> SatisfyingEntitiesFrom(IQueryable<T> query)
		{
			return Predicate == null ? query : query.Where(Predicate);
		}

		public override string ToString()
		{
			return Predicate != null ? Predicate.ToString() : string.Empty;
		}

		public ISpecification<T> Any()
		{
			return new AnySpecification<T>();
		}

		public static implicit operator string(SpecificationBase<T> specification)
		{
			return specification.ToString();
		}

		public static SpecificationBase<T> Evaluate(Expression<Func<T, bool>> expression)
		{
			return new ExpressionSpecification<T>(expression);
		}

		public static SpecificationBase<T> operator &(SpecificationBase<T> left, SpecificationBase<T> right)
		{
			return new AndSpecification<T>(left, right);
		}

		public static SpecificationBase<T> operator |(SpecificationBase<T> left, SpecificationBase<T> right)
		{
			return new OrSpecification<T>(left, right);
		}

		public static SpecificationBase<T> operator ^(SpecificationBase<T> left, SpecificationBase<T> right)
		{
			return new ExclusiveOrSpecification<T>(left, right);
		}

		public static SpecificationBase<T> operator !(SpecificationBase<T> specification)
		{
			return new NotSpecification<T>(specification);
		}
	}
}