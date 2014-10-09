#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SharpFu.Core.Internal;

#endregion

namespace SharpFu.Core
{
	/// <summary>
	///     Provides a standard base class for facilitating comparison of objects.
	///     originally forked from
	///     https://github.com/sharparchitecture/Sharp-Architecture/blob/master/Solutions/SharpArch.Domain/DomainModel/BaseObject.cs
	/// </summary>
	[Serializable]
	public abstract class ObjectBase : IEquatable<ObjectBase>
	{
		// http://www.dotnetjunkies.com/WebLog/chris.taylor/archive/2005/08/18/132026.aspx
		[ThreadStatic] private static Dictionary<Type, IEnumerable<PropertyInfo>> _signaturePropertyCache;

		protected virtual Type UnproxiedType
		{
			get { return GetType(); }
		}

		protected IEnumerable<PropertyInfo> TypeSignature
		{
			get { return GetSignatureProperties(); }
		}

		public bool Equals(ObjectBase other)
		{
			// return true if both are the same instance
			if (ReferenceEquals(this, other))
				return true;

			return other != null && GetType() == other.UnproxiedType
			       && HasEqualSignatureAs(other);
		}

		public override bool Equals(object obj)
		{
			var other = obj as ObjectBase;
			return Equals(other);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var signatureProperties = GetSignatureProperties();

				// It's possible for two objects to return the same hash code based on 
				// identically valued properties, even if they're of two different types, 
				// so we include the object's type in the hash calculation
				var hashCode = UnproxiedType.GetHashCode();

				var propertyInfos = signatureProperties as PropertyInfo[] ?? signatureProperties.ToArray();
				hashCode = propertyInfos.Select(property => property.GetValue(this, null))
					.Where(value => value != null)
					.Aggregate(hashCode, (current, value) => (current*Constants.HashMultiplier) ^ value.GetHashCode());

				// ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
				return propertyInfos.Any() ? hashCode : base.GetHashCode();
			}
		}

		private IEnumerable<PropertyInfo> GetSignatureProperties()
		{
			IEnumerable<PropertyInfo> properties;

			// Init the signaturePropertiesDictionary here due to reasons described at 
			// http://blogs.msdn.com/jfoscoding/archive/2006/07/18/670497.aspx
			if (_signaturePropertyCache == null)
			{
				_signaturePropertyCache = new Dictionary<Type, IEnumerable<PropertyInfo>>();
			}

			if (_signaturePropertyCache.TryGetValue(GetType(), out properties))
			{
				return properties;
			}

			return _signaturePropertyCache[GetType()] = GetTypeSignature()
				.Where(x => x != null && !x.GetIndexParameters().Any());
		}

		protected bool HasEqualSignatureAs(ObjectBase other)
		{
			var signatureProperties = GetSignatureProperties();

			var infos = signatureProperties as PropertyInfo[] ?? signatureProperties.ToArray();
			if ((from property in infos
				let valueOfThisObject = property.GetValue(this, null)
				let valueToCompareTo = property.GetValue(other, null)
				where valueOfThisObject != null || valueToCompareTo != null
				where (valueOfThisObject == null ^ valueToCompareTo == null) || (!valueOfThisObject.Equals(valueToCompareTo))
				select valueOfThisObject).Any())
			{
				return false;
			}

			// ReSharper disable once BaseObjectEqualsIsObjectEquals
			return infos.Any() || base.Equals(other);
		}

		public static bool operator ==(ObjectBase objectBase1, ObjectBase objectBase2)
		{
			if ((object) objectBase1 == null)
			{
				return (object) objectBase2 == null;
			}

			return objectBase1.Equals(objectBase2);
		}

		public static bool operator !=(ObjectBase objectBase1, ObjectBase objectBase2)
		{
			return !(objectBase1 == objectBase2);
		}

		protected virtual IEnumerable<PropertyInfo> GetTypeSignature()
		{
			return UnproxiedType.GetProperties();
		}
	}
}