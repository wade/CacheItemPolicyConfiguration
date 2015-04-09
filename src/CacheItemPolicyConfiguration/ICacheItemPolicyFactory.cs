using System.Runtime.Caching;

namespace CacheItemPolicyConfiguration
{
	/// <summary>
	/// Implemented by classes that create CacheItemPolicy instances from cache item policy configuration.
	/// </summary>
	public interface ICacheItemPolicyFactory
	{
		/// <summary>
		/// Creates a CacheItemPolicy instance from cache item policy configuration for the specified name.
		/// </summary>
		/// <param name="name">The configured cache item policy name.</param>
		/// <returns>
		/// An instance of <see cref="CacheItemPolicy"/> or null if the
		/// cache item policy configuration for the specified name was not found.
		/// </returns>
		CacheItemPolicy Create(string name);
	}
}