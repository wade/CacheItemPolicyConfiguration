using System;

namespace CacheItemPolicyConfiguration.TestHelpers
{
	/// <summary>
	/// The default implementation of <see cref="DateTime"/> that wraps <see cref="CacheItemPolicyDateTimeProvider"/>.
	/// </summary>
	public class MockCacheItemPolicyDateTimeProvider : CacheItemPolicyDateTimeProvider
	{
		private static readonly MockCacheItemPolicyDateTimeProvider _instance = new MockCacheItemPolicyDateTimeProvider();

		private DateTime _now;
		private DateTime _utcNow;

		private MockCacheItemPolicyDateTimeProvider()
		{
			_utcNow = DateTime.UtcNow;
			_now = FromUtcToSystemLocalImpl(_utcNow);
		}

		public static void Install()
		{
			if (Current is MockCacheItemPolicyDateTimeProvider)
				return;

			Current = _instance;
		}

		public static void Uninstall()
		{
			if (Current is MockCacheItemPolicyDateTimeProvider)
			{
				ResetToDefault();
			}
		}

		/// <summary>
		/// Gets the singleton instance of <see cref="DefaultCacheItemPolicyDateTimeProvider"/>.
		/// </summary>
		/// <value>
		/// The singleton instance of <see cref="DefaultCacheItemPolicyDateTimeProvider"/>.
		/// </value>
		public static MockCacheItemPolicyDateTimeProvider Instance { get { return _instance; } }

		/// <summary>
		/// Gets the immediate local <see cref="DateTime" /> value.
		/// </summary>
		/// <value>
		/// The immediate local <see cref="DateTime" /> value.
		/// </value>
		public override DateTime Now { get { return _now; } }

		public void SetNow(DateTime dateTime)
		{
			_now = dateTime;
		}

		/// <summary>
		/// Gets the immediate UTC <see cref="DateTime" /> value.
		/// </summary>
		/// <value>
		/// The immediate UTC <see cref="DateTime" /> value.
		/// </value>
		public override DateTime UtcNow { get { return _utcNow; } }

		public void SetUtcNow(DateTime dateTimeUtc)
		{
			_utcNow = dateTimeUtc;
		}

		/// <summary>
		/// Converts the specified UTC DateTime into a DateTime in the system's local time zone.
		/// </summary>
		/// <param name="utcDateTime">The UTC date time to be converted.</param>
		/// <returns>
		/// A DateTime in the system's local time zone.
		/// </returns>
		/// <exception cref="NotImplementedException"></exception>
		public override DateTime FromUtcToSystemLocal(DateTime utcDateTime)
		{
			return FromUtcToSystemLocalImpl(utcDateTime);
		}

		private static DateTime FromUtcToSystemLocalImpl(DateTime utcDateTime)
		{
			return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, TimeZoneInfo.Local);
		}
	}
}