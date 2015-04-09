using System.Collections.Generic;

namespace CacheItemPolicyConfiguration
{
	/// <summary>
	/// Implemented by classes tht provide cache item policy configuration.
	/// </summary>
	public interface ICacheItemPolicyConfiguration
	{
		/// <summary>
		/// Gets the cache item policy configuration items.
		/// </summary>
		/// <value>
		/// The cache item policy configuration items.
		/// </value>
		IList<ICacheItemPolicyConfigurationItem> CacheItemPolicyConfigurationItems { get; }
	}
}