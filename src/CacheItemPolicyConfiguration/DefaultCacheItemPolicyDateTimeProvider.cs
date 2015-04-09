using System;

namespace CacheItemPolicyConfiguration
{
	/// <summary>
	/// The default implementation of <see cref="CacheItemPolicyDateTimeProvider"/> that wraps <see cref="DateTime"/>.
	/// </summary>
	internal class DefaultCacheItemPolicyDateTimeProvider : CacheItemPolicyDateTimeProvider
	{
		private static readonly DefaultCacheItemPolicyDateTimeProvider _instance = new DefaultCacheItemPolicyDateTimeProvider();

		/// <summary>
		/// Gets the singleton instance of <see cref="DefaultCacheItemPolicyDateTimeProvider"/>.
		/// </summary>
		/// <value>
		/// The singleton instance of <see cref="DefaultCacheItemPolicyDateTimeProvider"/>.
		/// </value>
		public static DefaultCacheItemPolicyDateTimeProvider Instance { get { return _instance; } }

		/// <summary>
		/// Gets the immediate local <see cref="DateTime" /> value.
		/// </summary>
		/// <value>
		/// The immediate local <see cref="DateTime" /> value.
		/// </value>
		public override DateTime Now
		{
			get { return DateTime.Now; }
		}

		/// <summary>
		/// Gets the immediate UTC <see cref="DateTime" /> value.
		/// </summary>
		/// <value>
		/// The immediate UTC <see cref="DateTime" /> value.
		/// </value>
		public override DateTime UtcNow
		{
			get { return DateTime.UtcNow; }
		}

		/// <summary>
		/// Converts the specified UTC DateTime into a DateTime in the system's local time zone.
		/// </summary>
		/// <param name="utcDateTime">The UTC date time to be converted.</param>
		/// <returns>
		/// A DateTime in the system's local time zone.
		/// </returns>
		public override DateTime FromUtcToSystemLocal(DateTime utcDateTime)
		{
			return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, TimeZoneInfo.Local);
		}
	}
}