using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using CacheItemPolicyConfiguration.TestHelpers;
using Should;
using Xunit.Extensions;
using System.Collections.ObjectModel;

namespace CacheItemPolicyConfiguration
{
	/// <summary>
	/// Component integration tests that are configured programmatically.
	/// </summary>
	public class ProgrammaticConfigIntegrationTests
	{
		private static readonly CacheItemPolicyConfiguration _configuration;

		static ProgrammaticConfigIntegrationTests()
		{
			// Initialize the programmatic configuration store, _configuration.

			var currentDateTimeUtc = new DateTime(2073, 09, 15, 11, 32, 27);
			MockCacheItemPolicyDateTimeProvider.Instance.SetUtcNow(currentDateTimeUtc);
			MockCacheItemPolicyDateTimeProvider.Install();

			try
			{
				_configuration =
					new CacheItemPolicyConfiguration(
						new List<ICacheItemPolicyConfigurationItem>
					{
						new CacheItemPolicyConfigurationItem("TestPolicyAbsoluteByTimeSpanWithEnabledAttributeFalse", ObjectCache.InfiniteAbsoluteExpiration, false),
						new CacheItemPolicyConfigurationItem("TestPolicyAbsoluteByTimeSpanSetToAllZerosWithEnabledAttributeTrue", ObjectCache.InfiniteAbsoluteExpiration),
						new CacheItemPolicyConfigurationItem("TestPolicyAbsoluteByTimeSpanSetToSecondsWithEnabledAttributeTrue", new DateTimeOffset(CacheItemPolicyDateTimeProvider.Current.UtcNow.Add(TimeSpan.FromSeconds(30)))),
						new CacheItemPolicyConfigurationItem("TestPolicyAbsoluteByTimeSpanSetToMinutesWithEnabledAttributeTrue", new DateTimeOffset(CacheItemPolicyDateTimeProvider.Current.UtcNow.Add(TimeSpan.FromMinutes(15)))),
						new CacheItemPolicyConfigurationItem("TestPolicyAbsoluteByTimeSpanSetToHoursWithEnabledAttributeTrue", new DateTimeOffset(CacheItemPolicyDateTimeProvider.Current.UtcNow.Add(TimeSpan.FromHours(1)))),
						new CacheItemPolicyConfigurationItem("TestPolicyAbsoluteByTimeSpanSetToDaysWithEnabledAttributeTrue", new DateTimeOffset(CacheItemPolicyDateTimeProvider.Current.UtcNow.Add(TimeSpan.FromDays(7)))),
						new CacheItemPolicyConfigurationItem("TestPolicyAbsoluteByTimeSpanWithoutEnabledAttribute", new DateTimeOffset(CacheItemPolicyDateTimeProvider.Current.UtcNow.Add(TimeSpan.FromMinutes(5)))),

						new CacheItemPolicyConfigurationItem("TestPolicyAbsoluteByDateTimeWithEnabledAttributeFalse", new DateTimeOffset(new DateTime(2073, 09, 15, 08, 32, 00)), false),
						new CacheItemPolicyConfigurationItem("TestPolicyAbsoluteByDateTimeWithEnabledAttributeTrue", new DateTimeOffset(new DateTime(2073, 09, 15, 08, 32, 00))),
						new CacheItemPolicyConfigurationItem("TestPolicyAbsoluteByDateTimeWithoutEnabledAttribute", new DateTimeOffset(new DateTime(2073, 09, 15, 08, 32, 00))),

						new CacheItemPolicyConfigurationItem("TestPolicyAbsoluteInfiniteWithEnabledAttributeFalse", ObjectCache.InfiniteAbsoluteExpiration, false),
						new CacheItemPolicyConfigurationItem("TestPolicyAbsoluteInfiniteWithEnabledAttributeTrue", ObjectCache.InfiniteAbsoluteExpiration),
						new CacheItemPolicyConfigurationItem("TestPolicyAbsoluteInfiniteWithoutEnabledAttribute", ObjectCache.InfiniteAbsoluteExpiration),

						new CacheItemPolicyConfigurationItem("TestPolicySlidingWithEnabledAttributeFalse", TimeSpan.MaxValue, false),
						new CacheItemPolicyConfigurationItem("TestPolicySlidingSetToZeroWithEnabledAttributeTrue", ObjectCache.InfiniteAbsoluteExpiration),
						new CacheItemPolicyConfigurationItem("TestPolicySlidingSetToSecondsWithEnabledAttributeTrue", TimeSpan.FromSeconds(30)),
						new CacheItemPolicyConfigurationItem("TestPolicySlidingWithoutEnabledAttribute", TimeSpan.FromMinutes(5)),

                        new CacheItemPolicyConfigurationItem("TestPolicyCacheEntries", new string[] { "aKeyToBeMonitored", "anotherOne", "andSoOn" }),
                        new CacheItemPolicyConfigurationItem("TestPolicyCacheEntriesEmpty", new string[0])
                    });
			}
			finally
			{
				MockCacheItemPolicyDateTimeProvider.Uninstall();
			}
		}

		[Theory, PropertyData("CanCreateCacheItemPolicyWithAbsoluteExpirationByTimeSpanProgrammaticallyTestData")]
		public void CanCreateCacheItemPolicyWithAbsoluteExpirationByTimeSpanProgrammatically(string cacheItemPolicyName, TimeSpan expectedExpires, bool cacheItemPolicyShouldBeNull)
		{
			// Arrange
			var currentDateTimeUtc = new DateTime(2073, 09, 15, 11, 32, 27);

			var expected =
				(expectedExpires == TimeSpan.MaxValue)
					? ObjectCache.InfiniteAbsoluteExpiration
					: new DateTimeOffset(currentDateTimeUtc.Add(expectedExpires));

			var factory = new CacheItemPolicyFactory(_configuration);
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

		public static IEnumerable<object[]> CanCreateCacheItemPolicyWithAbsoluteExpirationByTimeSpanProgrammaticallyTestData
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

		[Theory, PropertyData("CanCreateCacheItemPolicyWithAbsoluteExpirationByDateTimeProgrammaticallyTestData")]
		public void CanCreateCacheItemPolicyWithAbsoluteExpirationByDateTimeProgrammatically(string cacheItemPolicyName, DateTime expectedExpires, bool cacheItemPolicyShouldBeNull)
		{
			// Arrange
			var expected = new DateTimeOffset(expectedExpires);
			var factory = new CacheItemPolicyFactory(_configuration);

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

		public static IEnumerable<object[]> CanCreateCacheItemPolicyWithAbsoluteExpirationByDateTimeProgrammaticallyTestData
		{
			get
			{
				yield return new object[] { "TestPolicyAbsoluteByDateTimeWithEnabledAttributeFalse", new DateTime(2073, 09, 15, 08, 32, 00), true };
				yield return new object[] { "TestPolicyAbsoluteByDateTimeWithEnabledAttributeTrue", new DateTime(2073, 09, 15, 08, 32, 00), false };
				yield return new object[] { "TestPolicyAbsoluteByDateTimeWithoutEnabledAttribute", new DateTime(2073, 09, 15, 08, 32, 00), false };
			}
		}

		[Theory, PropertyData("CanCreateCacheItemPolicyWithInfiniteAbsoluteExpirationProgrammaticallyTestData")]
		public void CanCreateCacheItemPolicyWithInfiniteAbsoluteExpirationProgrammatically(string cacheItemPolicyName, DateTimeOffset expectedExpires, bool cacheItemPolicyShouldBeNull)
		{
			// Arrange
			var expected = expectedExpires;
			var factory = new CacheItemPolicyFactory(_configuration);

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

		public static IEnumerable<object[]> CanCreateCacheItemPolicyWithInfiniteAbsoluteExpirationProgrammaticallyTestData
		{
			get
			{
				yield return new object[] { "TestPolicyAbsoluteInfiniteWithEnabledAttributeFalse", ObjectCache.InfiniteAbsoluteExpiration, true };
				yield return new object[] { "TestPolicyAbsoluteInfiniteWithEnabledAttributeTrue", ObjectCache.InfiniteAbsoluteExpiration, false };
				yield return new object[] { "TestPolicyAbsoluteInfiniteWithoutEnabledAttribute", ObjectCache.InfiniteAbsoluteExpiration, false };
			}
		}

		[Theory, PropertyData("CanCreateCacheItemPolicyWithSlidingExpirationProgrammaticallyTestData")]
		public void CanCreateCacheItemPolicyWithSlidingExpirationProgrammatically(string cacheItemPolicyName, TimeSpan expectedExpires, bool cacheItemPolicyShouldBeNull)
		{
			// Arrange
			var expected = expectedExpires;
			var factory = new CacheItemPolicyFactory(_configuration);

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

		public static IEnumerable<object[]> CanCreateCacheItemPolicyWithSlidingExpirationProgrammaticallyTestData
		{
			get
			{
				yield return new object[] { "TestPolicySlidingWithEnabledAttributeFalse", TimeSpan.MaxValue, true };
				yield return new object[] { "TestPolicySlidingSetToZeroWithEnabledAttributeTrue", TimeSpan.Zero, false };
				yield return new object[] { "TestPolicySlidingSetToSecondsWithEnabledAttributeTrue", TimeSpan.FromSeconds(30), false };
				yield return new object[] { "TestPolicySlidingWithoutEnabledAttribute", TimeSpan.FromMinutes(5), false };
			}
		}


        [Theory, PropertyData("CanCreateCacheItemPolicyWithCacheEntriesProgrammaticallyTestData")]
        public void CanCreateCacheItemPolicyWithCacheEntriesProgrammatically(string cacheItemPolicyName, ReadOnlyCollection<string> cacheEntries, bool monitorShouldBeNull)
        {
            // Arrange
            var expected = cacheEntries;
            var factory = new CacheItemPolicyFactory(_configuration);

            // Act
            var cacheItemPolicy = factory.Create(cacheItemPolicyName);
            var entriesMonitor = cacheItemPolicy.ChangeMonitors.FirstOrDefault() as CacheEntryChangeMonitor;

            // Assert
            if (monitorShouldBeNull)
            {
                entriesMonitor.ShouldBeNull();
                return;
            }

            entriesMonitor.ShouldNotBeNull();
            entriesMonitor.ShouldImplement<CacheEntryChangeMonitor>();

            entriesMonitor.CacheKeys.ShouldEqual(expected);
        }

        public static IEnumerable<object[]> CanCreateCacheItemPolicyWithCacheEntriesProgrammaticallyTestData
        {
            get
            {
                yield return new object[] { "TestPolicyCacheEntries", new ReadOnlyCollection<string>(new string[] { "aKeyToBeMonitored", "anotherOne", "andSoOn" }), false };
                yield return new object[] { "TestPolicyCacheEntriesEmpty", null, true };
            }
        }
    }
}