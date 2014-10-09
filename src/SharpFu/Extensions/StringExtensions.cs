using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Extensions
{

	/// <summary>
	///		Extension methods for <see cref="string"/>
	/// </summary>
	public static class StringExtensions
	{

		/// <summary>
		///		Evaluates if the string is null or empty
		/// </summary>
		public static bool IsNullOrEmpty(this string source)
		{
			return String.IsNullOrEmpty(source);
		}

		/// <summary>
		///		Evaluates if the string is null or whitespace
		/// </summary>
		public static bool IsNullOrWhitespace(this string source)
		{
			return String.IsNullOrWhiteSpace(source);
		}

		/// <summary>
		///		Returns a formatted string by arguments, similar
		///		to <see cref="string.Format(string,object)"/>
		/// </summary>
		public static string Args(this string @string, params object[] args)
		{
			return String.Format(@string, args);
		}
	}
}
