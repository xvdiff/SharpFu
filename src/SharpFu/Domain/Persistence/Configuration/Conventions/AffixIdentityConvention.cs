using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Persistence.Configuration.Conventions
{

	/// <summary>
	///		Denotes a convention that checks if the property name
	///		is affixed by a certain pattern
	/// </summary>
	public class AffixIdentityConvention : NameIdentityConventionBase
	{

		private static readonly Func<string, string, AffixIdentityKind, bool> AffixIdentityFunc = (name, pattern, affix) => 
			affix == AffixIdentityKind.Prefix
			? name.StartsWith(pattern, StringComparison.CurrentCultureIgnoreCase)
			: affix == AffixIdentityKind.Suffix
				? name.EndsWith(pattern, StringComparison.CurrentCultureIgnoreCase)
				: affix == AffixIdentityKind.Both && (name.StartsWith(pattern, StringComparison.CurrentCultureIgnoreCase) ||
				                                      name.EndsWith(pattern, StringComparison.CurrentCultureIgnoreCase));

		/// <summary>
		///		Creates a new instance of a <see cref="AffixIdentityConvention"/>
		/// </summary>
		/// <param name="pattern">Pattern to check</param>
		/// <param name="affixKind">The affix kind</param>
		public AffixIdentityConvention(string pattern, AffixIdentityKind affixKind)
			: base(name => AffixIdentityFunc(name, pattern, affixKind))
		{
			
		}
	}

	/// <summary>
	///		Denotes the kind of the affix
	/// </summary>
	public enum AffixIdentityKind
	{
		/// <summary>
		///		Denotes a prefix
		/// </summary>
		Prefix,

		/// <summary>
		///		Denotes a suffix
		/// </summary>
		Suffix,

		/// <summary>
		///		Denotes that the affix is both, a prefix and a suffix
		/// </summary>
		Both
	}
}
