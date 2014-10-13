using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;

namespace SharpFu.Extensions
{

	/// <summary>
	///		Extension methods for <see cref="List{T}"/>
	/// </summary>
	public static class ListExtensions
	{

		/// <summary>
		///		Creates a new <see cref="List{T}"/> based on the
		///		values of a current one
		/// </summary>
		public static List<T> Clone<T>(this ICollection<T> source)
		{
			return new List<T>(source);
		}
	}
}
