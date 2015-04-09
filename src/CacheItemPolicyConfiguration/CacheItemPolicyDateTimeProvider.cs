using System;

namespace CacheItemPolicyConfiguration
{
	/// <summary>
	/// Inherited by classes that provide date and time information and functionality.
	/// </summary>
	public abstract class CacheItemPolicyDateTimeProvider
	{
		private static CacheItemPolicyDateTimeProvider _current;

		/// <summary>
		/// Gets or sets the current instance of <see cref="CacheItemPolicyDateTimeProvider"/>.
		/// </summary>
		/// <value>
		/// The current instance of <see cref="CacheItemPolicyDateTimeProvider"/>.
		/// </value>
		/// <remarks>Provides ambient context for <see cref="CacheItemPolicyDateTimeProvider"/>.</remarks>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public static CacheItemPolicyDateTimeProvider Current
		{
			get { return _current ?? (_current = GetDefaultProvider()); }
			set
			{
				if (null == value)
					throw new ArgumentNullException("value");
				_current = value;
			}
		}

		/// <summary>
		/// Resets the current <see cref="CacheItemPolicyDateTimeProvider"/> instance (ambient context) to the default instance.
		/// </summary>
		public static void ResetToDefault()
		{
			Current = GetDefaultProvider();
		}

		/// <summary>
		/// Gets the immediate local <see cref="DateTime"/> value.
		/// </summary>
		/// <value>
		/// The immediate local <see cref="DateTime"/> value.
		/// </value>
		public abstract DateTime Now { get; }

		/// <summary>
		/// Gets the immediate UTC <see cref="DateTime"/> value.
		/// </summary>
		/// <value>
		/// The immediate UTC <see cref="DateTime"/> value.
		/// </value>
		public abstract DateTime UtcNow { get; }

		/// <summary>
		/// Converts the specified UTC DateTime into a DateTime in the system's local time zone.
		/// </summary>
		/// <param name="utcDateTime">The UTC date time to be converted.</param>
		/// <returns>A DateTime in the system's local time zone.</returns>
		public abstract DateTime FromUtcToSystemLocal(DateTime utcDateTime);

		private static CacheItemPolicyDateTimeProvider GetDefaultProvider()
		{
			return DefaultCacheItemPolicyDateTimeProvider.Instance;
		}
	}
}