using System;
using System.Runtime.Caching;

namespace CacheItemPolicyConfiguration
{
	/// <summary>
	/// Helper class used to create <see cref="CacheItemPolicy"/> instances from cache item policy configuration.
	/// </summary>
	public static class CacheItemPolicyHelpers
	{
		/// <summary>
		/// Creates a cache item policy using the information obtained from the specified cache item policy configuration item.
		/// </summary>
		/// <param name="configurationItem">The cache item policy configuration item.</param>
		/// <returns>
		/// A new instance of <see cref="CacheItemPolicy"/> or null is the specified configuration is disabled.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">configurationItem</exception>
		public static CacheItemPolicy CreateCacheItemPolicy(ICacheItemPolicyConfigurationItem configurationItem)
		{
			if (null == configurationItem)
				throw new ArgumentNullException("configurationItem");

			if (false == configurationItem.Enabled)
				return null;

			var policy = new CacheItemPolicy();

			// Determine if an absolute or sliding expiration should be set.
			if (configurationItem.SlidingExpiration == ObjectCache.NoSlidingExpiration)
			{
				// An absolute expiration is configured and will be set in the policy.
				policy.AbsoluteExpiration = configurationItem.AbsoluteExpiration;
			}
			else
			{
				// A sliding expiration is configured and will be set in the policy.
				policy.SlidingExpiration = configurationItem.SlidingExpiration;
			}

			return policy;
		}

		/// <summary>
		/// Parses the string representation of an absolute expiration.
		/// </summary>
		/// <param name="input">The string input.</param>
		/// <returns>
		/// A <see cref="DateTimeOffset"/>.
		/// </returns>
		public static DateTimeOffset ParseAbsoluteExpiration(string input)
		{
			if (false == string.IsNullOrWhiteSpace(input))
			{
				DateTime dateTime;
				TimeSpan timeSpan;

				if (TimeSpan.TryParse(input, out timeSpan))
				{
					// If the time span represents zero, return infinite.
					if (timeSpan.TotalMilliseconds <= 0)
						return ObjectCache.InfiniteAbsoluteExpiration;

					dateTime = CacheItemPolicyDateTimeProvider.Current.UtcNow.Add(timeSpan);
					return new DateTimeOffset(dateTime);
				}

				if (DateTime.TryParse(input, out dateTime))
				{
					return new DateTimeOffset(dateTime);
				}
			}
			return ObjectCache.InfiniteAbsoluteExpiration;
		}

		/// <summary>
		/// Parses the string representation of a sliding expiration.
		/// </summary>
		/// <param name="input">The string input.</param>
		/// <returns>
		/// A <see cref="TimeSpan"/> instance.
		/// </returns>
		public static TimeSpan ParseSlidingExpiration(string input)
		{
			if (false == string.IsNullOrWhiteSpace(input))
			{
				TimeSpan timeSpan;
				if (TimeSpan.TryParse(input, out timeSpan))
				{
					return timeSpan;
				}
			}
			return ObjectCache.NoSlidingExpiration;
		}
	}
}