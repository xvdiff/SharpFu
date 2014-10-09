using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;

namespace SharpFu.Persistence.EntityFramework
{
	public static class DbSetExtensions
	{
		public static void RemoveRange<TEntity>(this DbSet<TEntity> set, Expression<Func<TEntity, bool>> predicate = null)
			where TEntity : class
		{
			set.RemoveRange(set.Where(predicate));
		}
	}
}
