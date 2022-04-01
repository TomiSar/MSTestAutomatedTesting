using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngine.Tests
{
	[TestClass]
	public class BossEnemyShould
	{
		[TestMethod]
		public void HaveCorrecSpecialAttackPower()
		{
			// Act
			BossEnemy sut = new BossEnemy();

			// Assert
			Assert.AreEqual(166.6, sut.SpecialAttackPower, 0.07);
		}
	}
}
