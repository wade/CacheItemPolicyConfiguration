using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Caching;

namespace CacheItemPolicyConfiguration
{
	/// <summary>
	/// The default implementation of <see cref="ICacheItemPolicyFactory"/> that
	/// creates <see cref="CacheItemPolicy"/> instances using the specified
	/// <see cref="ICacheItemPolicyConfiguration"/> instance.
	/// </summary>
	public class IndexedCacheItemPolicyFactory : ICacheItemPolicyFactory
	{
		private readonly IReadOnlyDictionary<string, ICacheItemPolicyConfigurationItem> _index;

		/// <summary>
		/// Initializes a new instance of the <see cref="IndexedCacheItemPolicyFactory"/> class.
		/// </summary>
		/// <param name="configuration">The cache item policy configuration.</param>
		/// <exception cref="System.ArgumentNullException">configuration</exception>
		public IndexedCacheItemPolicyFactory(ICacheItemPolicyConfiguration configuration)
		{
			if (null == configuration)
				throw new ArgumentNullException("configuration");

			_index = CreateIndex(configuration);
		}

		/// <summary>
		/// Creates a CacheItemPolicy instance from cache item policy configuration for the specified name.
		/// </summary>
		/// <param name="name">The configured cache item policy name.</param>
		/// <returns>
		/// An instance of <see cref="CacheItemPolicy" /> or null if the
		/// cache item policy configuration for the specified name was not found.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">name</exception>
		public CacheItemPolicy Create(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException("name");

			var configurationItem = _index[name];
			return CacheItemPolicyHelpers.CreateCacheItemPolicy(configurationItem);
		}

		/// <summary>
		/// Creates the index from the specified cache item policy configuration.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <returns>
		/// A IReadOnlyDictionary&lt;string, ICacheItemPolicyConfigurationItem&gt; instance.
		/// </returns>
		private static IReadOnlyDictionary<string, ICacheItemPolicyConfigurationItem> CreateIndex(ICacheItemPolicyConfiguration configuration)
		{
			var items = configuration.CacheItemPolicyConfigurationItems;
			var index = new Dictionary<string, ICacheItemPolicyConfigurationItem>(items.Count);

			foreach (var item in items)
			{
				// Skip disabled items.
				if (false == item.Enabled)
					continue;

				index[item.Name] = item;
			}

			return new ReadOnlyDictionary<string, ICacheItemPolicyConfigurationItem>(index);
		}
	}
}