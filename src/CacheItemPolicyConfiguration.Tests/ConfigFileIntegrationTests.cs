using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using CacheItemPolicyConfiguration.ConfigFile;
using CacheItemPolicyConfiguration.TestHelpers;
using Should;
using Xunit.Extensions;

namespace CacheItemPolicyConfiguration
{
	/// <summary>
	/// Component integration tests that obtain configuration from the app.config file.
	/// </summary>
	public class ConfigFileIntegrationTests
	{
		[Theory, PropertyData("CanCreateCacheItemPolicyWithAbsoluteExpirationByTimeSpanFromDotNetConfigFileTestData")]
		public void CanCreateCacheItemPolicyWithAbsoluteExpirationByTimeSpanFromDotNetConfigFile(string cacheItemPolicyName, TimeSpan expectedExpires, bool cacheItemPolicyShouldBeNull)
		{
			// Arrange
			var currentDateTimeUtc = new DateTime(2073, 09, 15, 11, 32, 27);

			var expected =
				(expectedExpires == TimeSpan.MaxValue)
					? ObjectCache.InfiniteAbsoluteExpiration
					: new DateTimeOffset(currentDateTimeUtc.Add(expectedExpires));

			var config = new ConfigFileBasedCacheItemPolicyConfiguration();
			var factory = new CacheItemPolicyFactory(config);
			MockCacheItemPolicyDateTimeProvider.Instance.SetUtcNow(currentDateTimeUtc);
			MockCacheItemPolicyDateTimeProvider.Install();
			try
			{
				// Act
				var cacheItemPolicy = factory.Create(cacheItemPolicyName);

				// Assert
				if (cacheItemPolicyShouldBeNull)
				{
					cacheItemPolicy.ShouldBeNull();
					return;
				}

				cacheItemPolicy.AbsoluteExpiration.ShouldEqual(expected);
				cacheItemPolicy.SlidingExpiration.ShouldEqual(ObjectCache.NoSlidingExpiration);
			}
			finally
			{
				MockCacheItemPolicyDateTimeProvider.Uninstall();
			}
		}

		public static IEnumerable<object[]> CanCreateCacheItemPolicyWithAbsoluteExpirationByTimeSpanFromDotNetConfigFileTestData
		{
			get
			{
				yield return new object[] { "TestPolicyAbsoluteByTimeSpanWithEnabledAttributeFalse", TimeSpan.MaxValue, true };
				yield return new object[] { "TestPolicyAbsoluteByTimeSpanSetToAllZerosWithEnabledAttributeTrue", TimeSpan.MaxValue, false };
				yield return new object[] { "TestPolicyAbsoluteByTimeSpanSetToSecondsWithEnabledAttributeTrue", TimeSpan.FromSeconds(30), false };
				yield return new object[] { "TestPolicyAbsoluteByTimeSpanSetToMinutesWithEnabledAttributeTrue", TimeSpan.FromMinutes(15), false };
				yield return new object[] { "TestPolicyAbsoluteByTimeSpanSetToHoursWithEnabledAttributeTrue", TimeSpan.FromHours(1), false };
				yield return new object[] { "TestPolicyAbsoluteByTimeSpanSetToDaysWithEnabledAttributeTrue", TimeSpan.FromDays(7), false };
				yield return new object[] { "TestPolicyAbsoluteByTimeSpanWithoutEnabledAttribute", TimeSpan.FromMinutes(5), false };
			}
		}

		[Theory, PropertyData("CanCreateCacheItemPolicyWithAbsoluteExpirationByDateTimeFromDotNetConfigFileTestData")]
		public void CanCreateCacheItemPolicyWithAbsoluteExpirationByDateTimeFromDotNetConfigFile(string cacheItemPolicyName, DateTime expectedExpires, bool cacheItemPolicyShouldBeNull)
		{
			// Arrange
			var expected = new DateTimeOffset(expectedExpires);
			var config = new ConfigFileBasedCacheItemPolicyConfiguration();
			var factory = new CacheItemPolicyFactory(config);

			// Act
			var cacheItemPolicy = factory.Create(cacheItemPolicyName);

			// Assert
			if (cacheItemPolicyShouldBeNull)
			{
				cacheItemPolicy.ShouldBeNull();
				return;
			}

			cacheItemPolicy.AbsoluteExpiration.ShouldEqual(expected);
			cacheItemPolicy.SlidingExpiration.ShouldEqual(ObjectCache.NoSlidingExpiration);
		}

		public static IEnumerable<object[]> CanCreateCacheItemPolicyWithAbsoluteExpirationByDateTimeFromDotNetConfigFileTestData
		{
			get
			{
				yield return new object[] { "TestPolicyAbsoluteByDateTimeWithEnabledAttributeFalse", new DateTime(2073, 09, 15, 08, 32, 00), true };
				yield return new object[] { "TestPolicyAbsoluteByDateTimeWithEnabledAttributeTrue", new DateTime(2073, 09, 15, 08, 32, 00), false };
				yield return new object[] { "TestPolicyAbsoluteByDateTimeWithoutEnabledAttribute", new DateTime(2073, 09, 15, 08, 32, 00), false };
			}
		}

		[Theory, PropertyData("CanCreateCacheItemPolicyWithInfiniteAbsoluteExpirationFromDotNetConfigFileTestData")]
		public void CanCreateCacheItemPolicyWithInfiniteAbsoluteExpirationFromDotNetConfigFile(string cacheItemPolicyName, DateTimeOffset expectedExpires, bool cacheItemPolicyShouldBeNull)
		{
			// Arrange
			var expected = expectedExpires;
			var config = new ConfigFileBasedCacheItemPolicyConfiguration();
			var factory = new CacheItemPolicyFactory(config);

			// Act
			var cacheItemPolicy = factory.Create(cacheItemPolicyName);

			// Assert
			if (cacheItemPolicyShouldBeNull)
			{
				cacheItemPolicy.ShouldBeNull();
				return;
			}

			cacheItemPolicy.AbsoluteExpiration.ShouldEqual(expected);
			cacheItemPolicy.SlidingExpiration.ShouldEqual(ObjectCache.NoSlidingExpiration);
		}

		public static IEnumerable<object[]> CanCreateCacheItemPolicyWithInfiniteAbsoluteExpirationFromDotNetConfigFileTestData
		{
			get
			{
				yield return new object[] { "TestPolicyAbsoluteInfiniteWithEnabledAttributeFalse", ObjectCache.InfiniteAbsoluteExpiration, true };
				yield return new object[] { "TestPolicyAbsoluteInfiniteWithEnabledAttributeTrue", ObjectCache.InfiniteAbsoluteExpiration, false };
				yield return new object[] { "TestPolicyAbsoluteInfiniteWithoutEnabledAttribute", ObjectCache.InfiniteAbsoluteExpiration, false };
			}
		}

		[Theory, PropertyData("CanCreateCacheItemPolicyWithSlidingExpirationFromDotNetConfigFileTestData")]
		public void CanCreateCacheItemPolicyWithSlidingExpirationFromDotNetConfigFile(string cacheItemPolicyName, TimeSpan expectedExpires, bool cacheItemPolicyShouldBeNull)
		{
			// Arrange
			var expected = expectedExpires;
			var config = new ConfigFileBasedCacheItemPolicyConfiguration();
			var factory = new CacheItemPolicyFactory(config);

			// Act
			var cacheItemPolicy = factory.Create(cacheItemPolicyName);

			// Assert
			if (cacheItemPolicyShouldBeNull)
			{
				cacheItemPolicy.ShouldBeNull();
				return;
			}

			cacheItemPolicy.SlidingExpiration.ShouldEqual(expected);
			cacheItemPolicy.AbsoluteExpiration.ShouldEqual(ObjectCache.InfiniteAbsoluteExpiration);
		}

		public static IEnumerable<object[]> CanCreateCacheItemPolicyWithSlidingExpirationFromDotNetConfigFileTestData
		{
			get
			{
				yield return new object[] { "TestPolicySlidingWithEnabledAttributeFalse", TimeSpan.MaxValue, true };
				yield return new object[] { "TestPolicySlidingSetToZeroWithEnabledAttributeTrue", TimeSpan.Zero, false };
				yield return new object[] { "TestPolicySlidingSetToSecondsWithEnabledAttributeTrue", TimeSpan.FromSeconds(30), false };
				yield return new object[] { "TestPolicySlidingWithoutEnabledAttribute", TimeSpan.FromMinutes(5), false };
			}
		}
	}
}