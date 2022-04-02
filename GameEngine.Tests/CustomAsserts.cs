using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngine.Tests
{
	public static class CustomAsserts
	{
		public static void IsInRange(this Assert assert, int actual, int expectedMinValue, int expectedMaxValue)
		{
			if (actual < expectedMinValue || actual > expectedMaxValue)
			{
				throw new AssertFailedException($"Actual value: {actual} was not in range: {expectedMinValue}-{expectedMaxValue}");
			}
		}

		public static void AllItemsNullOrWhitespace(this CollectionAssert collectionAssert, ICollection<string> collection)
		{
			foreach (var item in collection)
			{
				if (string.IsNullOrWhiteSpace(item))
				{
					throw new AssertFailedException("One or more items in collection are null or whitespace");
				}
			}
		}

		public static void AllItemsSatisfy<T>(this CollectionAssert collectionAssert, ICollection<T> collection, Predicate<T> predicate)
		{
			foreach (var item in collection)
			{
				if (!predicate(item))
				{
					throw new AssertFailedException("All items don't satisfy predicate");
				}
			}
		}

		public static void AtLeastOneItemsSatisfies<T>(this CollectionAssert collectionAssert, ICollection<T> collection, Predicate<T> predicate)
		{
			foreach (var item in collection)
			{
				if (predicate(item))
				{
					return;
				}
			}
			throw new AssertFailedException("No items satisfy predicate");
		}

		public static void All<T>(this CollectionAssert collectionAssert, ICollection<T> collection, Action<T> assert)
		{
			foreach (var item in collection)
			{
				assert(item);
			}
		}

		public static void NotNullOrWhitespace(this StringAssert assert, string actual)
		{
			if (string.IsNullOrWhiteSpace(actual))
			{
				throw new AssertFailedException("Value is null or whitespace");
			}
		}
	}
}
