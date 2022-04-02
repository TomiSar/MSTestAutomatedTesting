using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngine.Tests
{
	[TestClass]
	[TestCategory("Player")]
	public class PlayerCharacterShould
	{
		PlayerCharacter sut;

		[TestInitialize]
		public void InitializePlayerCharacter()
		{
			sut = new PlayerCharacter();
			{
				sut.FirstName = "Chuck";
				sut.LastName = "Norris";
			}
		}

		[TestMethod]
		[PlayerDefaults]
		//[Ignore]
		public void BeInexperiencedWhenNew()
		{
			// Assert
			Assert.IsTrue(sut.IsNoob);
		}

		[TestMethod]
		public void BeExperiencedAfterSetup()
		{
			// Act
			sut.IsNoob = false;

			// Assert
			Assert.IsFalse(sut.IsNoob);
		}

		[TestMethod]
		public void ValidateFullName()
		{
			// Assert
			Assert.AreEqual("Chuck", sut.FirstName);
			Assert.AreEqual("Norris", sut.LastName);
			Assert.AreEqual("Chuck Norris", sut.FullName);
		}

		[TestMethod]
		public void ValidateFullName_IgnoreCaseSensitivity()
		{
			// Assert
			Assert.AreEqual("Chuck Norris".ToLower(), sut.FullName, ignoreCase: true);
		}

		[TestMethod]
		public void ValidateFullNameStartingWithFirstname()
		{
			// Assert
			StringAssert.StartsWith(sut.FullName, "Chuck");
		}

		[TestMethod]
		public void ValidateFullNameEndsWithLastname()
		{
			// Assert
			StringAssert.EndsWith(sut.FullName, "Norris");
		}

		[TestMethod]
		public void ValidateFullNameContainsSubstring()
		{
			// Assert
			StringAssert.Contains(sut.FullName, "uck Nor");
		}

		[TestMethod]
		public void ValidateFullNameWithTitleCase()
		{
			// Assert
			StringAssert.Matches(sut.FullName, new Regex( "[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
		}

		[TestMethod]
		[PlayerDefaults]
		//[Ignore]
		public void NotHaveNicknameByDefault()
		{
			// Assert
			Assert.IsNull(sut.Nickname);
		}

		[TestMethod]
		[PlayerDefaults]
		public void StartsWithDefaultHealth()
		{
			// Assert
			Assert.AreEqual(100, sut.Health);
		}

		[TestMethod]
		[PlayerDefaults]
		public void StartsWithDefaultHealth_NotEqual()
		{
			// Assert
			Assert.AreNotEqual(99, sut.Health);
		}

		[TestMethod]
		[PlayerHealth]
		public void IncreaseHealthAfterSleeping()
		{
			// Act
			sut.Sleep();

			// Assert
			//Assert.IsTrue(sut.Health >= 101 && sut.Health <= 200);
			//Assert.AreEqual(99, sut.Health, 100);
			Assert.That.IsInRange(sut.Health, 101, 200);
		}

		#region TestData Damages Helpers
		//public static IEnumerable<object[]> Damages
		//{
		//	get
		//	{
		//		return new List<object[]>
		//		{
		//			new object[] { 1, 99 },
		//			new object[] { 0, 100 },
		//			new object[] { 25, 75 },
		//			new object[] { 50, 50 },
		//			new object[] { 75, 25 },
		//			new object[] { 100, 1 },
		//		};
		//	}
		//}

		//public static IEnumerable<object[]> GetDamages()
		//{
		//	return new List<object[]>
		//	{
		//		new object[] { 1, 99 },
		//		new object[] { 0, 100 },
		//		new object[] { 25, 75 },
		//		new object[] { 50, 50 },
		//		new object[] { 100, 1 },
		//		new object[] { 101, 1 },
		//		new object[] { 10, 90 },
		//	};
		//}
		#endregion

		[DataTestMethod]
		[CsvDataSource("Damage.csv")]
		//[DynamicData(nameof(ExternalHealthDamageTestData.Testdata), typeof(ExternalHealthDamageTestData))]
		#region TestData sources
		//[DynamicData(nameof(Damages))]
		//[DynamicData(nameof(GetDamages), DynamicDataSourceType.Method)]
		//[DynamicData(nameof(DamageData.GetDamages), typeof(DamageData), DynamicDataSourceType.Method)]
		//[DataRow(0, 100)]
		//[DataRow(1, 99)]
		//[DataRow(25, 75)]
		//[DataRow(50, 50)]
		//[DataRow(100, 1)]
		//[DataRow(101, 1)] 
		#endregion

		[PlayerHealth]
		public void TakeDamage(int damage, int expectedHealth)
		{
			// Act
			sut.TakeDamage(damage);

			// Assert
			Assert.AreEqual(expectedHealth, sut.Health);
		}

		[TestMethod]
		[PlayerHealth]
		public void TakeDamage_NotEqual()
		{
			// Act
			sut.TakeDamage(1);

			// Assert
			Assert.AreNotEqual(100, sut.Health);
		}

		[TestMethod]
		public void StartingWeaponsContainsLongBow()
		{
			// Assert
			CollectionAssert.Contains(sut.Weapons, "Long Bow");
		}

		[TestMethod]
		public void StartingWeaponsContainsShortSword()
		{
			// Assert
			CollectionAssert.Contains(sut.Weapons, "Short Sword");
		}

		[TestMethod]
		public void StartingWeaponsDoesNotContainKalashnikov()
		{
			// Assert
			CollectionAssert.DoesNotContain(sut.Weapons, "Kalashnikov");
		}

		[TestMethod]
		public void HaveAllExpectedWeapons()
		{
			var expectedWeapons = new[]
			{
				"Long Bow",
				"Short Bow",
				"Short Sword"
			};

			// Assert
			CollectionAssert.AreEqual(expectedWeapons, sut.Weapons);
		}

		[TestMethod]
		public void HaveAllExpectedWeaponsAnyOrder()
		{
			var expectedWeapons = new[]
			{
				"Short Sword",
				"Short Bow",
				"Long Bow"
			};

			// Assert
			CollectionAssert.AreEquivalent(expectedWeapons, sut.Weapons);
		}

		[TestMethod]
		public void HaveNoDuplicateWeapons()
		{
			// Assert
			CollectionAssert.AllItemsAreUnique(sut.Weapons);
		}

		[TestMethod]
		public void HaveAtLeastOneKindOfSword()
		{
			// Assert
			//Assert.IsTrue(sut.Weapons.Any(weapon => weapon.Contains("Sword")));
			CollectionAssert.That.AtLeastOneItemsSatisfies(sut.Weapons, weapon => weapon.Contains("Sword"));
		}

		[TestMethod]
		public void HaveNoEmptyDefaultValues()
		{
			// Assert
			//Assert.IsFalse(sut.Weapons.Any(weapon => string.IsNullOrWhiteSpace(weapon)));
			//CollectionAssert.That.AllItemsNullOrWhitespace(sut.Weapons);
			//sut.Weapons.Add(" ");
			//CollectionAssert.That.AllItemsSatisfy(sut.Weapons, weapon => !string.IsNullOrWhiteSpace(weapon));

			CollectionAssert.That.All(sut.Weapons, weapon =>
			{
				StringAssert.That.NotNullOrWhitespace(weapon);
				Assert.IsTrue(weapon.Length > 5);
			});
		}
	}
}
