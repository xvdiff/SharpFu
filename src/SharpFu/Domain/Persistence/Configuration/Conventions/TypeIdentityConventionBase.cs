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

	/// <summary>
	///		Identity selector convention base class
	///		for type conditions
	/// </summary>
	public abstract class TypeIdentityConventionBase<T, TProperty> : IIdentityConvention
		where T: class 
	{

		private readonly Func<Type, bool> _condition;
		private readonly Expression<Func<T, TProperty>> _selector;

		/// <summary>
		///		Creates a new instance of a <see cref="TypeIdentityConventionBase{T,TProperty}"/>
		/// </summary>
		protected TypeIdentityConventionBase(Func<Type, bool> condition, Expression<Func<T, TProperty>> selector)
		{
			Guard.AgainstNullArgument(condition);
			Guard.AgainstNullArgument(selector);
			_condition = condition;
			_selector = selector;
		}

		/// <summary>
		///		Evaluates if the convention can be applied
		///		on certain entity/identity types
		/// </summary>
		public bool CanApplyOn(Type entityType, Type identityType)
		{
			return 
				(entityType == typeof(T) 
					|| entityType.Implements(entityType) 
					|| entityType.InheritsFrom(entityType))
				 && 
				 (identityType == typeof(TProperty) 
					|| identityType.Implements(identityType) 
					|| identityType.InheritsFrom(identityType));
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
