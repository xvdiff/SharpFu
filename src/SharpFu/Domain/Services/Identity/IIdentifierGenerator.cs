namespace SharpFu.Domain.Services.Identity
{

	/// <summary>
	///		Service for generating identifiers
	/// </summary>
	/// <typeparam name="T">Identifier type</typeparam>
	public interface IIdentifierGenerator<out T>
	{
		
		/// <summary>
		///		Generates a new identifier
		/// </summary>
		T Generate();
	}
}