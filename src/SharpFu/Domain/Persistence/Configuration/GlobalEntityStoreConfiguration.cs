﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;
using SharpFu.Domain.Model;
using SharpFu.Domain.Persistence.Configuration.Conventions;

namespace SharpFu.Domain.Persistence.Configuration
{
	public static class GlobalEntityStoreConfiguration
	{

		private readonly static Dictionary<Tuple<Type, Type>, dynamic> _identitySelectors = new Dictionary<Tuple<Type, Type>, dynamic>(); 

		private static List<IIdentityConvention> _defaultConventions = new List<IIdentityConvention>
		{
			new AffixIdentityConvention("Id", AffixIdentityKind.Both),
			new AffixIdentityConvention("Key", AffixIdentityKind.Both),
			new EqualsNameIdentityConvention("Id"),
			new EqualsNameIdentityConvention("Key"),
			new IdentityAttributeConvention()
		};

		public static List<IIdentityConvention> DefaultConventions
		{
			get { return _defaultConventions; }
			set {
				Guard.AgainstNullArgument(value);
				_defaultConventions = value;
			}
		}

		public static Expression<Func<TEntity, TIdentity>> GetDefaultIdentitySelector<TEntity, TIdentity>()
			where TEntity : class 
		{
			var key = Tuple.Create(typeof (TEntity), typeof (TIdentity));

			return _identitySelectors.ContainsKey(key) ? 
				_identitySelectors[key] : DefaultConventions.Select(x => x.Apply<TEntity, TIdentity>()).Single(x => x != null);
		}

	}
}
