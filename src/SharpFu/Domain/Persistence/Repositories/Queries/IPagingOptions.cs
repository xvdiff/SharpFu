namespace SharpFu.Domain.Persistence.Repositories.Queries
{

	/// <summary>
	///		Denotes options for paging queries
	/// </summary>
	public interface IPagingOptions
	{
		/// <summary>
		///		Gets or sets the page size
		/// </summary>
		int PageSize { get; set; }

		/// <summary>
		///		Gets or sets the page number
		/// </summary>
		int PageNumber { get; set; }
		
		/// <summary>
		///		Gets or sets the number of pages
		///		to skip
		/// </summary>
		int Skip { get; }

		/// <summary>
		///		Gets or sets the amount of
		///		items to take
		/// </summary>
		int Take { get; }

		/// <summary>
		///		Gets or sets the total
		///		amount of items
		/// </summary>
		int TotalItems { get; set; }
	}
}