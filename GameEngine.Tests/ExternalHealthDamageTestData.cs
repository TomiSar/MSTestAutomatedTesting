using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Tests
{
	public class ExternalHealthDamageTestData
	{
		public static IEnumerable<object[]> Testdata
		{
			get
			{
				string[] csvLines = File.ReadAllLines("Damage.csv");
				var testCases = new List<object[]>();
				foreach (var csvline in csvLines)
				{
					IEnumerable<int> values = csvline.Split(",").Select(int.Parse);
					object[] testCase = values.Cast<object>().ToArray();
					testCases.Add(testCase);
				}
				return testCases;
			}
		}
	}
}
