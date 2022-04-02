using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngine.Tests
{
	[TestClass]
	public class Lifecycle
	{
		static string TestContext;

		[TestInitialize]
		public void LifecycleInitialize()
		{
			Console.WriteLine("    TestInitialize Lifecycle");
		}

		[TestCleanup]
		public void LifecycleCleanup()
		{
			Console.WriteLine("    TestCleanup Lifecycle");
		}

		[ClassInitialize]
        public static void LifecycleClassInitialize(TestContext context)
		{
			Console.WriteLine("    ClassInitialize Lifecycle");
			Console.WriteLine("    Data loaded from disk or some expensive object creation");
			TestContext = "42";
		}

		[ClassCleanup]
		public static void LifecycleClassClean()
		{
			Console.WriteLine("    ClassCleanup Lifecycle");
		}


		[TestMethod]
		public void TestA()
		{
			Console.WriteLine("		Test A starting");
			Console.WriteLine($"		Shared test context: {TestContext}");
		}

		[TestMethod]
		public void TestB()
		{
			Console.WriteLine("		Test B starting");
			Console.WriteLine($"		Shared test context: {TestContext}");
		}
	}
}
