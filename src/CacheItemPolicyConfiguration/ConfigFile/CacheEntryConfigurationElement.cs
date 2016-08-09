using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheItemPolicyConfiguration.ConfigFile
{
    /// <summary>
    /// The configuration element contained in <see cref="CacheEntryConfigurationElementCollection"/>
    /// </summary>
    public class CacheEntryConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// The key of a cache entry to be monitored
        /// </summary>
        [ConfigurationProperty("key")]
        public string Key
        {
            get { return (string)base["key"]; }
            set { base["key"] = value; }
        }
    }
}
