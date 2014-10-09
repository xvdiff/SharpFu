#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SharpFu.Domain.Persistence.Specifications;
using SharpFu.Extensions;
using SharpFu.Linq.Expressions;

#endregion

namespace SharpFu.Domain.Persistence.Repositories
{
	public abstract partial class RepositoryBase<TEntity, TIdentity>
		where TEntity : class
	{

		private ISpecification<TEntity> ByIdentitySpecification(TIdentity identity)
		{
			var parameter				= Expression.Parameter(typeof (TIdentity), "identity");
			var equalsBinaryExpression	= Expression.Equal(IdentitySelector.Body, Expression.Constant(identity));
			var equalsExpression		= Expression.Lambda<Func<TIdentity, bool>>(equalsBinaryExpression, parameter);
			var predicateExpression		= IdentitySelector.Combine(equalsExpression);

			return new ExpressionSpecification<TEntity>(predicateExpression);
		}

	}
}