namespace SharpFu.Domain.Persistence.Traits
{

	/// <summary>
	///		Denotes that the underlying store
	///		can find entites by identity
	/// </summary>
	/// <typeparam name="TIdentity">Identity type</typeparam>
	public interface ICanFindByIdentity<in TIdentity>
	{
		
		/// <summary>
		///		Denotes if the entity exists
		///		in the underlying store
		/// </summary>
		/// <param name="identity">Identity to lookup</param>
		bool Exists(TIdentity identity);
	}
}