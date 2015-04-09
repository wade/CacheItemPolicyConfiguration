using System;
using System.Linq;
using System.Runtime.Caching;

namespace CacheItemPolicyConfiguration
{
	/// <summary>
	/// Creates <see cref="CacheItemPolicy"/> instances using the specified
	/// <see cref="ICacheItemPolicyConfiguration"/> instance.
	/// </summary>
	/// <remarks>
	/// This is a basic, default implementation of <see cref="ICacheItemPolicyFactory"/>.
	/// </remarks>
	public class CacheItemPolicyFactory : ICacheItemPolicyFactory
	{
		private readonly ICacheItemPolicyConfiguration _configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheItemPolicyFactory"/> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <exception cref="System.ArgumentNullException">configuration</exception>
		public CacheItemPolicyFactory(ICacheItemPolicyConfiguration configuration)
		{
			if (null == configuration)
				throw new ArgumentNullException("configuration");

			_configuration = configuration;
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

			var configurationItem = _configuration
				.CacheItemPolicyConfigurationItems
					.FirstOrDefault(item =>
						item.Enabled && item.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)
						);

			if (null == configurationItem || false == configurationItem.Enabled)
				return null;

			return CacheItemPolicyHelpers.CreateCacheItemPolicy(configurationItem);
		}
	}
}