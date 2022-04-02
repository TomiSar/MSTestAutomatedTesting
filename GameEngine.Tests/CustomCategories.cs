﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameEngine.Tests
{
	public class PlayerDefaultsAttribute : TestCategoryBaseAttribute
	{	
		public override IList<string> TestCategories => new[] { "PlayerDefaults" };
	}

	public class PlayerHealthAttribute : TestCategoryBaseAttribute
	{
		public override IList<string> TestCategories => new[] { "PlayerHealth" };
	}
}
