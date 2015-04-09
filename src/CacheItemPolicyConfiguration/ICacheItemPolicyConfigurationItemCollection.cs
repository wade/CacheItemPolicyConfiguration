using System.Collections.Generic;

namespace CacheItemPolicyConfiguration
{
	/// <summary>
	/// Implemented by classes that represent a list-based collection of cache item policy configuration items.
	/// </summary>
	public interface ICacheItemPolicyConfigurationItemCollection : IList<ICacheItemPolicyConfigurationItem>
	{
	}
}