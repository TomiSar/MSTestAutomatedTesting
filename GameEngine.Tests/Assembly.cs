using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngine.Tests
{
	[TestClass]
	public class Assembly
	{
		[AssemblyInitialize]
		public static void AassemblyInitialize(TestContext context)
		{
			Console.WriteLine("AssemblyInit");
		}

		[AssemblyCleanup]
		public static void AassemblyCleanUp()
		{
			Console.WriteLine("AssemblyCleanUp");
		}
	}
}
