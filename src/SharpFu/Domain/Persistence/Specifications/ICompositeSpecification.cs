namespace SharpFu.Domain.Persistence.Specifications
{
	/// <summary>
	///     Interface for composite specifications
	/// </summary>
	public interface ICompositeSpecification<T> : ISpecification<T>
	{
		/// <summary>
		///		Returns the left side of the specification
		/// </summary>
		ISpecification<T> Left { get; }

		/// <summary>
		///		Returns the right side of the specification
		/// </summary>
		ISpecification<T> Right { get; }
	}
}