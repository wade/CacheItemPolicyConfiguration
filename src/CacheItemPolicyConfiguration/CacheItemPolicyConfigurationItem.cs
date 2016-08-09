using System;
using System.Collections.Generic;

namespace CacheItemPolicyConfiguration
{
	/// <summary>
	/// Cache item policy configuration item class used for programmatic configuration.
	/// </summary>
	public class CacheItemPolicyConfigurationItem : ICacheItemPolicyConfigurationItem
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CacheItemPolicyConfigurationItem"/> class.
		/// </summary>
		public CacheItemPolicyConfigurationItem()
		{
			Enabled = true;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItemPolicyConfigurationItem"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cacheEntries">The cache keys to be monitored</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        public CacheItemPolicyConfigurationItem(string name, IEnumerable<string> cacheEntries, bool enabled = true)
        {
            Name = name;
            CacheEntries = cacheEntries;
            Enabled = enabled;
        }


		/// <summary>
		/// Initializes a new instance of the <see cref="CacheItemPolicyConfigurationItem"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="enabled">if set to <c>true</c> [enabled].</param>
		public CacheItemPolicyConfigurationItem(string name, bool enabled = true)
		{
			Name = name;
			Enabled = enabled;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheItemPolicyConfigurationItem"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="absoluteExpiration">The absolute expiration.</param>
		/// <param name="enabled">if set to <c>true</c> [enabled].</param>
		public CacheItemPolicyConfigurationItem(string name, DateTimeOffset absoluteExpiration, bool enabled = true)
		{
			Name = name;
			AbsoluteExpiration = absoluteExpiration;
			Enabled = enabled;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheItemPolicyConfigurationItem"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="slidingExpiration">The sliding expiration.</param>
		/// <param name="enabled">if set to <c>true</c> [enabled].</param>
		public CacheItemPolicyConfigurationItem(string name, TimeSpan slidingExpiration, bool enabled = true)
		{
			Name = name;
			SlidingExpiration = slidingExpiration;
			Enabled = enabled;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ICacheItemPolicyConfigurationItem" /> is enabled.
		/// </summary>
		/// <value>
		///   <c>true</c> if enabled; otherwise, <c>false</c>.
		/// </value>
		public bool Enabled { get; set; }

		/// <summary>
		/// Gets or sets the name of the configured cache item policy.
		/// </summary>
		/// <value>
		/// The name of the configured cache item policy.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the absolute expiration.
		/// </summary>
		/// <value>
		/// The absolute expiration.
		/// </value>
		public DateTimeOffset AbsoluteExpiration { get; set; }

		/// <summary>
		/// Gets or sets the sliding expiration.
		/// </summary>
		/// <value>
		/// The sliding expiration.
		/// </value>
		public TimeSpan SlidingExpiration { get; set; }

        /// <summary>
        /// Gets a collection of cache keys that are monitored for changes. See <see cref="System.Runtime.Caching.CacheEntryChangeMonitor"/>.
        /// </summary>
        /// <value>
        /// The cache keys to be monitored.
        /// </value>
        public IEnumerable<string> CacheEntries { get; set; }
    }
}