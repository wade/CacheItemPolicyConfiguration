# CacheItemPolicyConfiguration #

CacheItemPolicyConfiguration is a .NET library that provides a programmatic and .NET config file-based configuration of cache item policies used with the `System.Runtime.Caching.ObjectCache` class and its derived classes (such as `System.Runtime.Caching.MemoryCache`).

## NuGet Package ##

This library is available from the NuGet Gallery as the **CacheItemPolicyConfiguration** package.

To install **CacheItemPolicyConfiguration**, run the following command in the Package Manager Console

    Install-Package CacheItemPolicyConfiguration 

The package currently provides a version built against the Microsoft .NET Framework 4.5.


## Example Usage ##

	var config = new ConfigFileBasedCacheItemPolicyConfiguration();
	var factory = new CacheItemPolicyFactory(config);
	var cacheItemPolicy = factory.Create("MyPolicy01");
	cache.Add(key, value, cacheItemPolicy);

In the C# example above, the `cache` variable is an instance of the `System.Runtime.Caching.ObjectCache` class, `key` is a string and `value` is the object to be cached.

## Config Section ##

	<?xml version="1.0" encoding="utf-8" ?>
	<configuration>
		<configSections>
			<section name="cacheItemPolicies" type="CacheItemPolicyConfiguration.ConfigFile.CacheItemPolicyConfigurationSection, CacheItemPolicyConfiguration" />
		</configSections>
		<cacheItemPolicies>
			<cacheItemPolicy name="MyPolicy01" absoluteExpiration="0.00:01:00" />
			<cacheItemPolicy name="MyPolicy02" absoluteExpiration="09/15/2073 16:45:00" enabled="true" />
			<cacheItemPolicy name="MyPolicy03" absoluteExpiration="infinite" enabled="true" />
			<cacheItemPolicy name="MyPolicy04" slidingExpiration="0.00:20:00" enabled="true" />
		</cacheItemPolicies>
	</configuration>


