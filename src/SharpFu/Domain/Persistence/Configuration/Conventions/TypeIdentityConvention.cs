using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;
using SharpFu.Extensions;
using SharpFu.Linq.Expressions;

namespace SharpFu.Domain.Persistence.Configuration.Conventions
{
	public abstract class TypeIdentityConvention<T, TProperty> : IIdentityConvention
		where T: class 
	{

		private readonly Func<Type, bool> _condition;
		private readonly Expression<Func<T, TProperty>> _selector;

		protected TypeIdentityConvention(Func<Type, bool> condition, Expression<Func<T, TProperty>> selector)
		{
			Guard.AgainstNullArgument(condition);
			Guard.AgainstNullArgument(selector);
			_condition = condition;
			_selector = selector;
		}

		public bool CanApplyOn(Type entityType, Type identityType)
		{
			return entityType == typeof(T) && identityType == typeof(TProperty);
		}
		
		public Expression<Func<TEntity, TIdentity>> Apply<TEntity, TIdentity>()
			where TEntity : class 
		{
			var entityType = typeof (TEntity);
			var identityType = typeof (TIdentity);

			if (!CanApplyOn(entityType, identityType) || !_condition(entityType))
				return null;

			var parameter = Expression.Parameter(entityType, "x");
			return Expression.Lambda<Func<TEntity, TIdentity>>(_selector.Body, parameter);
		}

		
	}
}
