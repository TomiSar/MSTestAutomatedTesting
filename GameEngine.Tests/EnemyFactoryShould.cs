using System; 
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngine.Tests
{
	[TestClass]
	public class EnemyFactoryShould
	{
		private readonly EnemyFactory _sut;
		public EnemyFactoryShould()
		{
			_sut = new EnemyFactory();
		}

		[TestMethod]
		public void NotAllowedNullName()
		{
			// Assert
			Assert.ThrowsException<ArgumentNullException>(()  => _sut.Create(null));
		}

		[TestMethod]
		public void OnlyAllowKingOrQueenBossEnemies()
		{
			// Act
			EnemyCreationException ex = Assert.ThrowsException<EnemyCreationException>(() => _sut.Create("Panda", true));

			// Assert
			Assert.AreEqual("Panda", ex.RequestedEnemyName);
		}

		[TestMethod]
		public void CreateNormalEnemyByDefault()
		{
			// Act
			Enemy enemy = _sut.Create("Zombie");

			// Assert
			Assert.IsInstanceOfType(enemy, typeof(NormalEnemy));
		}

		[TestMethod]
		public void CreateNormalEnemyByDefault_NotType()
		{
			// Act
			Enemy enemy = _sut.Create("Zombie");

			// Assert
			Assert.IsNotInstanceOfType(enemy, typeof(DateTime));
		}

		[TestMethod]
		public void CreateBossEnemy()
		{
			// Act
			Enemy enemy = _sut.Create("Zombie King", true);

			// Assert
			Assert.IsInstanceOfType(enemy, typeof(BossEnemy));
		}


		[TestMethod]
		public void CreateSeparateIntances()
		{
			// Act
			Enemy enemy1 = _sut.Create("Zombie");
			Enemy enemy2 = _sut.Create("Zombie");

			// Assert
			Assert.AreNotSame(enemy1, enemy2);
		}
	}
}
