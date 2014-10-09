namespace SharpFu.Domain.Persistence.Traits
{

	/// <summary>
	///		Denotes that the underlying store can
	///		aggregate over its entities
	/// </summary>
	public interface ICanAggregate
	{
		/// <summary>
		///		Returns the total amount of
		///		entities inside the underlying store
		/// </summary>
		int Count();

		/// <summary>
		///		Returns the total amount of
		///		entities inside the underlying store
		/// </summary>
		long LongCount();
	}
}