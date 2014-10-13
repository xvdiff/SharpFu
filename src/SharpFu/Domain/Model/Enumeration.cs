#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using SharpFu.Domain.Model.Conventions;
using SharpFu.Extensions;

#endregion

namespace SharpFu.Domain.Model
{
	public abstract class FriendlyNamedEnumeration<T> : EnumerationBase<T>
	{
		private readonly string _displayName;

		protected FriendlyNamedEnumeration(T value, string displayName)
			: base(value)
		{
			_displayName = displayName;
		}

		protected string DisplayName
		{
			get { return _displayName; }
		}

		public override string ToString()
		{
			return _displayName;
		}
	}

	public abstract class Enumeration : EnumerationBase<int>, IComparable
	{
		protected Enumeration(int value)
			: base(value)
		{
		}

		public int CompareTo(object obj)
		{
			return Value.CompareTo(((Enumeration) obj).Value);
		}

		public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
		{
			return Math.Abs(firstValue.Value - secondValue.Value);
		}
	}

	[Serializable, ComVisible(true)]
	public abstract class EnumerationBase<T> : ValueObjectBase,
		IEquatable<EnumerationBase<T>>
	{
		private readonly T _value;

		protected EnumerationBase(T value)
		{
			_value = value;
		}

		[DomainSignature]
		protected T Value
		{
			get { return _value; }
		}

		public bool Equals(EnumerationBase<T> other)
		{
			if (ReferenceEquals(this, other))
				return true;

			if (other == null || !(GetType() == other.UnproxiedType))
				return false;

			return _value.Equals(other.Value);
		}

		public override string ToString()
		{
			return _value.ToString();
		}

		public static IEnumerable<TEnum> GetAll<TEnum>()
			where TEnum : EnumerationBase<T>
		{
			var fields = typeof (T).GetFields(BindingFlags.Public
			                                  | BindingFlags.Static | BindingFlags.DeclaredOnly);

			return fields.Select(x => x.GetValue(null)).Cast<TEnum>();
		}

		public static TEnum Parse<TEnum>(T value)
			where TEnum : EnumerationBase<T>
		{
			var matchingItem = GetAll<TEnum>().FirstOrDefault(x => x._value.Equals(value));

			if (matchingItem != null)
				return matchingItem;

			var message = string.Format("'{0}' is not a valid value in {1}", value, typeof (TEnum));
			throw new ArgumentOutOfRangeException(message);
		}

		public override bool Equals(object obj)
		{
			var other = obj as EnumerationBase<T>;
			return Equals(other);
		}

		public override int GetHashCode()
		{
			return _value.GetHashCode();
		}

		protected override IEnumerable<PropertyInfo> GetTypProperties()
		{
			return GetType().GetProperties().Where(
				x => x.HasAttribute<DomainSignatureAttribute>());
		}
	}
}