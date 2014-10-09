#region

using System;
using System.ComponentModel;

#endregion

namespace SharpFu.Core.Internal
{
	/// <summary>
	///     Hides System.Object members from interfaces
	///     http://blogs.clariusconsulting.net/kzu/how-to-hide-system-object-members-from-your-interfaces/
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public interface IFluentInterface
	{
		/// <summary>
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		Type GetType();

		/// <summary>
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		int GetHashCode();

		/// <summary>
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		string ToString();

		/// <summary>
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		bool Equals(object obj);
	}
}