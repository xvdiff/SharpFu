#region

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFu.Core.Guarding;

#endregion

namespace SharpFu.Tests.Core.Guarding
{
	[TestClass]
	public class GuardingTests
	{
		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void CanGuardAgainstNullArgument()
		{
			Guard.AgainstNullArgument<object>(null);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void CanGuardAgainstStringNotNullOrEmpty()
		{
			Guard.AgainstNullOrEmpty("");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CanGuardAgainstDefaultArgument()
		{
			Guard.AgainstDefaultArgument(default(Guid));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void CanGuardAgainstOutOfRangeArgument()
		{
			Guard.AgainstOutOfRange(11, 0, 10);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CanGuardAgainstNullProperty()
		{
			var user = new User();
			Guard.AgainstPropertyIsNull(user, x => x.Name);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CanGuardAgainstDefaultProperty()
		{
			var user = new User();
			Guard.AgainstPropertyIsDefault(user, x => x.Id);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void CanGuardAgainstCondition()
		{
			var user = new User();
			Guard.AgainstCondition(String.IsNullOrEmpty(""));
		}

		internal class User
		{
			public Guid Id { get; set;}
			public string Name { get; set; }
		}
	}
}