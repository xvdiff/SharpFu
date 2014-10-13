using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core;
using SharpFu.Core.Internal;
using SharpFu.Domain.Model.Conventions;
using SharpFu.Extensions;

namespace SharpFu.Domain.Model
{
	public abstract class EntityBase<TKey> : ObjectBase, IEntity<TKey>,
		IEquatable<EntityBase<TKey>>
	{
		private int? _cachedHashCode;

		// virtual to allow NHibernate-backed objects to be lazily loaded
		[Identity]
		public virtual TKey Id { get; protected set; }

		public bool IsTransient
		{
			get { return EqualityComparer<TKey>.Default.Equals(Id, default(TKey)); }
		}

		public bool Equals(IEntity<TKey> other)
		{
			return Equals(other as EntityBase<TKey>);
		}

		public bool Equals(EntityBase<TKey> other)
		{
			if (ReferenceEquals(this, other))
				return true;

			if (other == null || !(GetType() == other.UnproxiedType))
				return false;

			return (IsTransient && other.IsTransient && HasEqualSignatureAs(other))
				   || HasSameNonDefaultIdAs(other);
		}

		public override bool Equals(object obj)
		{
			var other = obj as EntityBase<TKey>;
			return Equals(other);
		}

		// https://stackoverflow.com/questions/7687551/ddd-gethashcode-and-primary-id
		public override int GetHashCode()
		{
			if (_cachedHashCode.HasValue)
				return _cachedHashCode.Value;

			if (IsTransient)
			{
				_cachedHashCode = base.GetHashCode();
			}
			else
			{
				unchecked
				{
					var hashCode = GetType().GetHashCode();
					_cachedHashCode = (hashCode * Constants.HashMultiplier) ^ Id.GetHashCode();
				}
			}

			return _cachedHashCode.Value;
		}

		protected override IEnumerable<PropertyInfo> GetTypProperties()
		{
			return GetType().GetProperties().Where(
				x => x.HasAttribute<DomainSignatureAttribute>());
		}

		private bool HasSameNonDefaultIdAs(IEntity<TKey> other)
		{
			return !IsTransient && !other.IsTransient && Id.Equals(other.Id);
		}
	}
}
