#region

using System;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace SharpFu.Domain.Persistence.Repositories.Queries
{
	public class PagingOptions<TEntity, TSortKey> : SortingOptions<TEntity, TSortKey>, IPagingOptions
		where TEntity : class
	{
		public PagingOptions(int pageNumber, int pageSize, Expression<Func<TEntity, TSortKey>> sortExpression,
			bool isDescending = false)
			: base(sortExpression, isDescending)
		{
			PageSize = pageSize;
			PageNumber = pageNumber;
		}

		public int PageSize { get; set; }
		public int PageNumber { get; set; }

		public int Skip
		{
			get { return (PageNumber - 1)*PageSize; }
		}

		public int Take
		{
			get { return PageSize; }
		}

		public int TotalItems { get; set; }

		public override IQueryable<TEntity> Apply(IQueryable<TEntity> query)
		{
			// apply sorting
			query = SortAction(query);

			TotalItems = query.Count();
			if (Skip > 0 || Take > 0)
				return query.Skip(Skip).Take(Take);

			return query;
		}
	}
}