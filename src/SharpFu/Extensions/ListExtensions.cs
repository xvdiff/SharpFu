using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;

namespace SharpFu.Extensions
{
	public static class ListExtensions
	{
		public static List<T> Clone<T>(this List<T> source)
		{
			Guard.AgainstNullArgument(source, "source");
			return new List<T>(source);
		}
	}
}
