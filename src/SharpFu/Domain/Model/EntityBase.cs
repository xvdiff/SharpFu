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

	/// <summary>
	///		Base class for domain entities
	///		with identity equality operations
	/// </summary>
	/// <typeparam name="TIdentity">Identity type</typeparam>
	public abstract class EntityBase<TIdentity> : ObjectBase, IEntity<TIdentity>,
		IEquatable<EntityBase<TIdentity>>
	{
		private int? _cachedHashCode;


		
		/// <summary>
		///		Denotes the identity of the domain
		///		entity
		/// </summary>
		[Identity]
		// virtual to allow NHibernate-backed objects to be lazily loaded
		public virtual TIdentity Id { get; protected set; }

		/// <summary>
		///		Denotes if the entity is currently transient
		/// </summary>
		public bool IsTransient
		{
			get { return EqualityComparer<TIdentity>.Default.Equals(Id, default(TIdentity)); }
		}

		public bool Equals(IEntity<TIdentity> other)
		{
			return Equals(other as EntityBase<TIdentity>);
		}

		public bool Equals(EntityBase<TIdentity> other)
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
			var other = obj as EntityBase<TIdentity>;
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

		protected override IEnumerable<PropertyInfo> GetTypeProperties()
		{
			return GetType().GetProperties().Where(
				x => x.HasAttribute<DomainSignatureAttribute>());
		}

		private bool HasSameNonDefaultIdAs(IEntity<TIdentity> other)
		{
			return !IsTransient && !other.IsTransient && Id.Equals(other.Id);
		}
	}
}
