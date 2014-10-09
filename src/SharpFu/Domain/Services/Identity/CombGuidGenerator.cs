#region

using System;

#endregion

namespace SharpFu.Domain.Services.Identity
{
	/// <summary>
	///     A sequential guid generator that uses the COMB algorithm
	///     to generate fast but still random unique identifiers similar
	///     NEWSEQUENTIALID() in MS SqlServer
	///     https://en.wikipedia.org/wiki/Globally_unique_identifier#Sequential_algorithms
	///     http://www.siepman.nl/blog/post/2013/10/28/ID-Sequential-Guid-COMB-Vs-Int-Identity-using-Entity-Framework.aspx
	/// </summary>
	public class CombGuidGenerator : IIdentifierGenerator<Guid>
	{
		public Guid Generate()
		{
			var guidArray = new GuidGenerator()
				.Generate().ToByteArray();

			var baseDate = new DateTime(1900, 1, 1);
			var now = DateTime.Now;

			// Get the days and milliseconds which will be used to build the byte string 
			var days = new TimeSpan(now.Ticks - baseDate.Ticks);
			var msecs = now.TimeOfDay;

			// Convert to a byte array 
			// Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
			var daysArray = BitConverter.GetBytes(days.Days);
			var msecsArray = BitConverter.GetBytes((long) (msecs.TotalMilliseconds/3.333333));

			// Reverse the bytes to match SQL Servers ordering 
			Array.Reverse(daysArray);
			Array.Reverse(msecsArray);

			// Copy the bytes into the guid 
			Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
			Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

			return new Guid(guidArray);
		}
	}
}