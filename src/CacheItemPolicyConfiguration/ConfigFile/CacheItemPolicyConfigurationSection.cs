using System.Collections.Generic;
using System.Configuration;

namespace CacheItemPolicyConfiguration.ConfigFile
{
	/// <summary>
	/// A configuration section that is used to access cache item policy configuration items stored in a .NET config file.
	/// </summary>
	public class CacheItemPolicyConfigurationSection : ConfigurationSection, ICacheItemPolicyConfiguration
	{
		private const string CacheItemPoliciesChildElementName = "cacheItemPolicy";
		private const string CacheItemPoliciesElementName = "";	// <-- Must be an empty string because the property is the default collection.

		/// <summary>
		/// Gets the collection of cache item policy configuration elements.
		/// </summary>
		/// <value>The collection of cache item policy configuration elements.</value>
		[ConfigurationProperty(CacheItemPoliciesElementName, IsDefaultCollection = true)]
		[ConfigurationCollection(typeof(CacheItemPolicyConfigurationElementCollection), AddItemName = CacheItemPoliciesChildElementName)]
		public CacheItemPolicyConfigurationElementCollection CacheItemPoliciesConfiguration
		{
			get { return this[CacheItemPoliciesElementName] as CacheItemPolicyConfigurationElementCollection; }
		}

		/// <summary>
		/// Gets the cache item policy configuration items.
		/// </summary>
		/// <value>
		/// The cache item policy configuration items.
		/// </value>
		IList<ICacheItemPolicyConfigurationItem> ICacheItemPolicyConfiguration.CacheItemPolicyConfigurationItems
		{
			get { return CacheItemPoliciesConfiguration; }
		}
	}
}