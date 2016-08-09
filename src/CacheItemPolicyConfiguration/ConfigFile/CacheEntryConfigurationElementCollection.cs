using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheItemPolicyConfiguration.ConfigFile
{
    public class CacheEntryConfigurationElementCollection : ConfigurationElementCollection
    {
        private static readonly string _ElementName = "cacheEntry";


        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMapAlternate; }
        }

        /// <summary>
        /// Gets the name used to identify this collection of elements in the configuration file when overridden in a derived class.
        /// </summary>
        /// <returns>
        /// The name of the collection; otherwise, an empty string. The default is an empty
        /// </returns>
        protected override string ElementName
        {
            get { return _ElementName; }
        }

        /// <summary>
		/// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
		/// </summary>
		/// <returns>
		/// A new <see cref="T:System.Configuration.ConfigurationElement" />.
		/// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new CacheEntryConfigurationElement();
        }

        /// <summary>
		/// Gets the element key for a specified configuration element when overridden in a derived class.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Object"/> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement"/>.
		/// </returns>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement"/> to return the key for. </param>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CacheEntryConfigurationElement)element).Key;
        }
    }
}
