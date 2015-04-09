using System;

namespace CacheItemPolicyConfiguration
{
	/// <summary>
	/// Implemented by classes that represent a cache item policy configuration item.
	/// </summary>
	public interface ICacheItemPolicyConfigurationItem
	{
		/// <summary>
		/// Gets a value indicating whether this <see cref="ICacheItemPolicyConfigurationItem"/> is enabled.
		/// </summary>
		/// <value>
		///   <c>true</c> if enabled; otherwise, <c>false</c>.
		/// </value>
		bool Enabled { get; }

		/// <summary>
		/// Gets the name of the configured cache item policy.
		/// </summary>
		/// <value>
		/// The name of the configured cache item policy.
		/// </value>
		string Name { get; }

		/// <summary>
		/// Gets the absolute expiration.
		/// </summary>
		/// <value>
		/// The absolute expiration.
		/// </value>
		DateTimeOffset AbsoluteExpiration { get; }

		/// <summary>
		/// Gets the sliding expiration.
		/// </summary>
		/// <value>
		/// The sliding expiration.
		/// </value>
		TimeSpan SlidingExpiration { get; }
	}
}