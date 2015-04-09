using System.Collections.Generic;

namespace CacheItemPolicyConfiguration
{
	/// <summary>
	/// Programmatic cache item policy configuration.
	/// </summary>
	public class CacheItemPolicyConfiguration : ICacheItemPolicyConfiguration
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CacheItemPolicyConfiguration"/> class.
		/// </summary>
		public CacheItemPolicyConfiguration()
		{
			CacheItemPolicyConfigurationItems = new List<ICacheItemPolicyConfigurationItem>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheItemPolicyConfiguration"/> class.
		/// </summary>
		/// <param name="configurationItems">The configuration items.</param>
		public CacheItemPolicyConfiguration(IEnumerable<ICacheItemPolicyConfigurationItem> configurationItems)
		{
			CacheItemPolicyConfigurationItems = new List<ICacheItemPolicyConfigurationItem>(configurationItems);
		}

		/// <summary>
		/// Gets or sets the cache item policy configuration items.
		/// </summary>
		/// <value>
		/// The cache item policy configuration items.
		/// </value>
		public IList<ICacheItemPolicyConfigurationItem> CacheItemPolicyConfigurationItems { get; set; }
	}
}