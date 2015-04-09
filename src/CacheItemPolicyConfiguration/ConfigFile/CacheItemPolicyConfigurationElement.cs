using System;
using System.Configuration;

namespace CacheItemPolicyConfiguration.ConfigFile
{
	/// <summary>
	/// A .NET config file implementation of <see cref="ICacheItemPolicyConfigurationItem"/>.
	/// </summary>
	public class CacheItemPolicyConfigurationElement : ConfigurationElement, ICacheItemPolicyConfigurationItem
	{
		private const string EnabledAttributeName = "enabled";
		private const string NameAttributeName = "name";
		private const string AbsoluteExpirationAttributeName = "absoluteExpiration";
		private const string SlidingExpirationAttributeName = "slidingExpiration";

		/// <summary>
		/// Gets a value indicating whether the item is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		[ConfigurationProperty(EnabledAttributeName, DefaultValue = true, IsRequired = false)]
		public bool Enabled
		{
			get { return (bool)this[EnabledAttributeName]; }
		}

		/// <summary>
		/// Gets the name of the item.
		/// </summary>
		/// <value>The name.</value>
		[ConfigurationProperty(NameAttributeName, IsRequired = true)]
		public string Name
		{
			get { return this[NameAttributeName] as string; }
		}

		/// <summary>
		/// Gets the absolute expiration.
		/// </summary>
		/// <value>
		/// The absolute expiration.
		/// </value>
		[ConfigurationProperty(AbsoluteExpirationAttributeName, IsRequired = false)]
		public string AbsoluteExpiration
		{
			get { return this[AbsoluteExpirationAttributeName] as string; }
		}

		/// <summary>
		/// Gets the absolute expiration.
		/// </summary>
		/// <value>
		/// The absolute expiration.
		/// </value>
		DateTimeOffset ICacheItemPolicyConfigurationItem.AbsoluteExpiration
		{
			get { return CacheItemPolicyHelpers.ParseAbsoluteExpiration(AbsoluteExpiration); }
		}

		/// <summary>
		/// Gets the sliding expiration.
		/// </summary>
		/// <value>
		/// The sliding expiration.
		/// </value>
		[ConfigurationProperty(SlidingExpirationAttributeName, IsRequired = false)]
		public string SlidingExpiration
		{
			get { return this[SlidingExpirationAttributeName] as string; }
		}

		/// <summary>
		/// Gets the sliding expiration.
		/// </summary>
		/// <value>
		/// The sliding expiration.
		/// </value>
		TimeSpan ICacheItemPolicyConfigurationItem.SlidingExpiration
		{
			get { return CacheItemPolicyHelpers.ParseSlidingExpiration(SlidingExpiration); }
		}
	}
}