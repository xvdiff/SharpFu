namespace SharpFu.Domain.Persistence.Specifications
{
	/// <summary>
	///     Base class for left/right (composite) specifications
	/// </summary>
	public abstract class CompositeSpecificationBase<T> : SpecificationBase<T>, ICompositeSpecification<T>
	{
		private readonly ISpecification<T> _left;
		private readonly ISpecification<T> _right;

		protected CompositeSpecificationBase(ISpecification<T> left, ISpecification<T> right)
		{
			_left = left;
			_right = right;
		}

		public ISpecification<T> Left
		{
			get { return _left; }
		}

		public ISpecification<T> Right
		{
			get { return _right; }
		}
	}
}