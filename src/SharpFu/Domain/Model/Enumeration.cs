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

	/// <summary>
	///		Friendly named enumeration
	/// </summary>
	/// <typeparam name="T">Enumeration value type</typeparam>
	public abstract class FriendlyNamedEnumeration<T> : EnumerationBase<T>
	{
		private readonly string _displayName;

		/// <summary>
		///		Creates a new instance of a <see cref="FriendlyNamedEnumeration{T}"/>
		/// </summary>
		protected FriendlyNamedEnumeration(T value, string displayName)
			: base(value)
		{
			_displayName = displayName;
		}

		/// <summary>
		///		Returns the enumeration display name
		/// </summary>
		protected string DisplayName
		{
			get { return _displayName; }
		}

		public override string ToString()
		{
			return _displayName;
		}
	}

	/// <summary>
	///		Integer based enumeration class
	/// </summary>
	public class Enumeration : EnumerationBase<int>, IComparable
	{

		/// <summary>
		///		Creates a new instance of a <see cref="Enumeration"/>
		/// </summary>
		protected Enumeration(int value)
			: base(value)
		{
		}

		public int CompareTo(object obj)
		{
			return Value.CompareTo(((Enumeration) obj).Value);
		}

		/// <summary>
		///		Calculates the absolute difference between
		///		two integer based enumerations
		/// </summary>
		public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
		{
			return Math.Abs(firstValue.Value - secondValue.Value);
		}
	}

	/// <summary>
	///		Base class for class enumerations
	/// </summary>
	/// <typeparam name="T">Enumeration value type</typeparam>
	[Serializable, ComVisible(true)]
	public abstract class EnumerationBase<T> : ValueObjectBase,
		IEquatable<EnumerationBase<T>>
	{
		private readonly T _value;

		/// <summary>
		///		Creates a new instance of <see cref="EnumerationBase{T}"/>
		/// </summary>
		protected EnumerationBase(T value)
		{
			_value = value;
		}

		/// <summary>
		///		Returns the value of the current enumeration
		/// </summary>
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

		/// <summary>
		///		Returns all values of the enumeration
		/// </summary>
		public static IEnumerable<TEnum> GetAll<TEnum>()
			where TEnum : EnumerationBase<T>
		{
			var fields = typeof (T).GetFields(BindingFlags.Public
			                                  | BindingFlags.Static | BindingFlags.DeclaredOnly);

			return fields.Select(x => x.GetValue(null)).Cast<TEnum>();
		}

		/// <summary>
		///		Parses a value to a corresponding enumeration
		/// </summary>
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

	}
}