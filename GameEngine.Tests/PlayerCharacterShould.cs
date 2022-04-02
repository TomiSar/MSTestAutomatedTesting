using System;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngine.Tests
{
	[TestClass]
	[TestCategory("Player")]
	public class PlayerCharacterShould
	{
		private readonly PlayerCharacter _sut;

		public PlayerCharacterShould()
		{
			_sut = new PlayerCharacter();
		}

		[TestMethod]
		[TestCategory("PlayerDefaults")]
		//[Ignore]
		public void BeInexperiencedWhenNew()
		{
			// Assert
			Assert.IsTrue(_sut.IsNoob);
		}

		[TestMethod]
		public void BeExperiencedAfterSetup()
		{
			// Act
			_sut.IsNoob = false;

			// Assert
			Assert.IsFalse(_sut.IsNoob);
		}

		[TestMethod]
		public void ValidateFullName()
		{
			// Act
			_sut.FirstName = "John";
			_sut.LastName = "Doe";

			// Assert
			Assert.AreEqual("John", _sut.FirstName);
			Assert.AreEqual("Doe", _sut.LastName);
			Assert.AreEqual("John Doe", _sut.FullName);
		}

		[TestMethod]
		public void ValidateFullName_IgnoreCaseSensitivity()
		{
			// Act
			_sut.FirstName = "JOHN";
			_sut.LastName = "DOE";

			// Assert
			Assert.AreEqual("John Doe".ToLower(), _sut.FullName, ignoreCase: true);
		}

		[TestMethod]
		public void ValidateFullNameStartingWithFirstname()
		{
			// Act
			_sut.FirstName = "John";
			_sut.LastName = "Doe";

			// Assert
			StringAssert.StartsWith(_sut.FullName, "John");
		}

		[TestMethod]
		public void ValidateFullNameEndsWithLastname()
		{
			// Act
			_sut.FirstName = "John";
			_sut.LastName = "Doe";

			// Assert
			StringAssert.EndsWith(_sut.FullName, "Doe");
		}

		[TestMethod]
		public void ValidateFullNameContainsSubstring()
		{
			// Act
			_sut.FirstName = "Steven";
			_sut.LastName = "Seagal";

			// Assert
			StringAssert.Contains(_sut.FullName, "ven Sea");
		}

		[TestMethod]
		public void ValidateFullNameWithTitleCase()
		{
			// Act
			_sut.FirstName = "Steven";
			_sut.LastName = "Seagal";

			// Assert
			StringAssert.Matches(_sut.FullName, new Regex( "[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
		}

		[TestMethod]
		[TestCategory("PlayerDefaults")]
		//[Ignore]
		public void NotHaveNicknameByDefault()
		{
			// Assert
			Assert.IsNull(_sut.Nickname);
		}

		[TestMethod]
		[TestCategory("PlayerDefaults")]
		//[Ignore]
		public void StartsWithDefaultHealth()
		{
			// Assert
			Assert.AreEqual(100, _sut.Health);
		}

		[TestMethod]
		[TestCategory("PlayerDefaults")]
		//[Ignore]
		public void StartsWithDefaultHealth_NotEqual()
		{
			// Assert
			Assert.AreNotEqual(99, _sut.Health);
		}

		[TestMethod]
		[TestCategory("PlayerHealth")]
		public void IncreaseHealthAfterSleeping()
		{
			// Act
			_sut.Sleep();

			// Assert
			Assert.IsTrue(_sut.Health >= 101 && _sut.Health <= 200);
			Assert.AreEqual(99, _sut.Health, 100);
		}

		[TestMethod]
		[TestCategory("PlayerHealth")]
		public void TakeDamage()
		{
			// Act
			int damage = 5;
			_sut.TakeDamage(damage);

			// Assert
			Assert.AreEqual(100 - damage, _sut.Health);
		}

		[TestMethod]
		[TestCategory("PlayerHealth")]
		public void TakeDamage_NotEqual()
		{
			// Act
			_sut.TakeDamage(1);

			// Assert
			Assert.AreNotEqual(100, _sut.Health);
		}

		[TestMethod]
		public void StartingWeaponsContainsLongBow()
		{
			// Assert
			CollectionAssert.Contains(_sut.Weapons, "Long Bow");
		}

		[TestMethod]
		public void StartingWeaponsContainsShortSword()
		{
			// Assert
			CollectionAssert.Contains(_sut.Weapons, "Short Sword");
		}

		[TestMethod]
		public void StartingWeaponsDoesNotContainKalashnikov()
		{
			// Assert
			CollectionAssert.DoesNotContain(_sut.Weapons, "Kalashnikov");
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
			CollectionAssert.AreEqual(expectedWeapons, _sut.Weapons);
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
			CollectionAssert.AreEquivalent(expectedWeapons, _sut.Weapons);
		}

		[TestMethod]
		public void HaveNoDuplicateWeapons()
		{
			// Assert
			CollectionAssert.AllItemsAreUnique(_sut.Weapons);
		}

		[TestMethod]
		public void HaveAtLeastOneKindOfSword()
		{
			// Assert
			Assert.IsTrue(_sut.Weapons.Any(weapon => weapon.Contains("Sword")));
		}

		[TestMethod]
		public void HaveNoEmptyDefaultValues()
		{
			// Assert
			Assert.IsFalse(_sut.Weapons.Any(weapon => string.IsNullOrWhiteSpace(weapon)));
		}
	}
}
