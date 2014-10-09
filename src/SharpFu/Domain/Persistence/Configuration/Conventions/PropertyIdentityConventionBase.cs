using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;

namespace SharpFu.Domain.Persistence.Configuration.Conventions
{
	public abstract class PropertyIdentityConventionBase : IIdentityConvention
	{

		private static readonly IDictionary<Tuple<Type, Type>, IEnumerable<PropertyInfo>> PropertiesCache
			= new Dictionary<Tuple<Type, Type>, IEnumerable<PropertyInfo>>(); 

		private readonly Func<PropertyInfo, bool> _condition; 

		protected PropertyIdentityConventionBase(Func<PropertyInfo, bool> condition)
		{
			Guard.AgainstNullArgument(condition);
			_condition = condition;
		}

		public Expression<Func<TEntity, TIdentity>> Apply<TEntity, TIdentity>()
			where TEntity : class
		{

			var entityType = typeof (TEntity);
			var identityType = typeof (TEntity);
			
			var property = GetProperties(entityType, identityType).FirstOrDefault(x => _condition(x));
			if (property == null)
				return null;

			var parameter = Expression.Parameter(entityType, "x");
			return Expression.Lambda<Func<TEntity, TIdentity>>(Expression.Property(parameter, property.Name), parameter);
		}

		private static IEnumerable<PropertyInfo> GetProperties(Type entityType, Type identityType)
		{
			var key = Tuple.Create(entityType, identityType);

			if (PropertiesCache.ContainsKey(key))
				return PropertiesCache[key];

			var properties = entityType.GetProperties()
				.Where(x => x != null && x.PropertyType == identityType && !x.GetIndexParameters().Any()).ToArray();
			PropertiesCache.Add(key, properties);

			return properties;
		} 

	}
}
