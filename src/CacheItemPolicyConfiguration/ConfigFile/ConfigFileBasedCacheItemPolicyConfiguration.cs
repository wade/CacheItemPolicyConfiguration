using System;
using System.Collections.Generic;
using System.Configuration;

namespace CacheItemPolicyConfiguration.ConfigFile
{
	/// <summary>
	/// Used to retrieve cache item policy configuration items from a .NET config file.
	/// </summary>
	/// <remarks>
	/// This is a .NET config file-based implementation of <see cref="ICacheItemPolicyConfiguration"/>.
	/// If the default constructor is used, the default config section name, "cacheItemPolicies", is used.
	/// It is possible to configure multiple cache item policy configuration config sections, if needed.
	/// </remarks>
	public class ConfigFileBasedCacheItemPolicyConfiguration : ICacheItemPolicyConfiguration
	{
		private const string DefaultConfigSectionName = "cacheItemPolicies";
		private readonly ICacheItemPolicyConfiguration _cacheItemPolicyConfiguration;

		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigFileBasedCacheItemPolicyConfiguration"/> class.
		/// </summary>
		public ConfigFileBasedCacheItemPolicyConfiguration() : this(DefaultConfigSectionName)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigFileBasedCacheItemPolicyConfiguration"/> class.
		/// </summary>
		/// <param name="configSectionName">Name of the configuration section.</param>
		/// <exception cref="System.ArgumentNullException">configSectionName</exception>
		public ConfigFileBasedCacheItemPolicyConfiguration(string configSectionName)
		{
			if (string.IsNullOrWhiteSpace(configSectionName))
				throw new ArgumentNullException("configSectionName");

			_cacheItemPolicyConfiguration = ConfigurationManager.GetSection(configSectionName) as ICacheItemPolicyConfiguration;
		}

		/// <summary>
		/// Gets the cache item policy configuration items.
		/// </summary>
		/// <value>
		/// The cache item policy configuration items.
		/// </value>
		public IList<ICacheItemPolicyConfigurationItem> CacheItemPolicyConfigurationItems
		{
			get { return _cacheItemPolicyConfiguration.CacheItemPolicyConfigurationItems; }
		}
	}
}