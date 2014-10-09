#region

using System;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace SharpFu.Domain.Persistence.Repositories.Queries
{
	public class SortingOptions<TEntity, TSortKey> : IQueryOptions<TEntity>
		where TEntity : class
	{
		private readonly Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _sortAction;

		public SortingOptions(Expression<Func<TEntity, TSortKey>> sortExpression, bool isDescending = false)
		{
			if (isDescending)
			{
				_sortAction = q => q.OrderByDescending(sortExpression);
			}
			else
			{
				_sortAction = q => q.OrderBy(sortExpression);
			}
		}

		protected Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> SortAction
		{
			get { return _sortAction; }
		}

		public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> query)
		{
			return _sortAction(query);
		}
	}
}