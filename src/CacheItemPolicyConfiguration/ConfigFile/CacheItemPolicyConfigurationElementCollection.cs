using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace CacheItemPolicyConfiguration.ConfigFile
{
	/// <summary>
	/// A .NET config file configuration element collection implementation of <see cref="ICacheItemPolicyConfigurationItemCollection"/>.
	/// </summary>
	public class CacheItemPolicyConfigurationElementCollection : ConfigurationElementCollection, ICacheItemPolicyConfigurationItemCollection
	{
		/// <summary>
		/// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <returns>
		/// The index of <paramref name="item" /> if found in the list; otherwise, -1.
		/// </returns>
		/// <exception cref="System.InvalidOperationException"></exception>
		public int IndexOf(ICacheItemPolicyConfigurationItem item)
		{
			var element = item as CacheItemPolicyConfigurationElement;
			if (null != element)
				return BaseIndexOf(element);

			throw new InvalidOperationException(string.Format("The item must be an instance of the {0} type.", typeof(CacheItemPolicyConfigurationElement).FullName));
		}

		/// <summary>
		/// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1" /> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void Insert(int index, ICacheItemPolicyConfigurationItem item)
		{
			// This member is intentionally not implemented.
			throw new NotImplementedException();
		}

		/// <summary>
		/// Removes the <see cref="T:System.Collections.Generic.IList`1" /> item at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void RemoveAt(int index)
		{
			// This member is intentionally not implemented.
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		ICacheItemPolicyConfigurationItem IList<ICacheItemPolicyConfigurationItem>.this[int index]
		{
			get { return BaseGet(index) as CacheItemPolicyConfigurationElement; }
			set
			{
				// This member is intentionally not implemented.
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public CacheItemPolicyConfigurationElement this[int index]
		{
			get { return BaseGet(index) as CacheItemPolicyConfigurationElement; }
		}

		/// <summary>
		/// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
		/// </summary>
		/// <returns>
		/// A new <see cref="T:System.Configuration.ConfigurationElement" />.
		/// </returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new CacheItemPolicyConfigurationElement();
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
			return ((CacheItemPolicyConfigurationElement)element).Name;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
		/// </returns>
		IEnumerator<ICacheItemPolicyConfigurationItem> IEnumerable<ICacheItemPolicyConfigurationItem>.GetEnumerator()
		{
			return new CacheItemPolicyConfigurationElementEnumerator(GetEnumerator());
		}

		/// <summary>
		/// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <exception cref="System.InvalidOperationException"></exception>
		public void Add(ICacheItemPolicyConfigurationItem item)
		{
			var element = item as CacheItemPolicyConfigurationElement;
			if (null != element)
				BaseAdd(element);
			else
				throw new InvalidOperationException(string.Format("The item must be an instance of the {0} type.", typeof(CacheItemPolicyConfigurationElement).FullName));
		}

		/// <summary>
		/// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}

		/// <summary>
		/// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <returns>
		/// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
		/// </returns>
		/// <exception cref="System.InvalidOperationException"></exception>
		public bool Contains(ICacheItemPolicyConfigurationItem item)
		{
			var element = item as CacheItemPolicyConfigurationElement;
			if (null != element)
				return (BaseIndexOf(element) > -1);

			throw new InvalidOperationException(string.Format("The item must be an instance of the {0} type.", typeof(CacheItemPolicyConfigurationElement).FullName));
		}

		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="arrayIndex">Index of the array.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void CopyTo(ICacheItemPolicyConfigurationItem[] array, int arrayIndex)
		{
			// This member is intentionally not implemented.
			throw new NotImplementedException();
		}

		/// <summary>
		/// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <returns>
		/// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public bool Remove(ICacheItemPolicyConfigurationItem item)
		{
			// This member is intentionally not implemented.
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
		/// </summary>
		public new bool IsReadOnly
		{
			get { return true; }
		}

		/// <summary>
		/// Enumerator implementation for ICacheItemPolicyConfigurationItem.
		/// </summary>
		public class CacheItemPolicyConfigurationElementEnumerator : IEnumerator<ICacheItemPolicyConfigurationItem>
		{
			private readonly IEnumerator _enumerator;

			/// <summary>
			/// Initializes a new instance of the <see cref="CacheItemPolicyConfigurationElementEnumerator"/> class.
			/// </summary>
			/// <param name="enumerator">The enumerator.</param>
			public CacheItemPolicyConfigurationElementEnumerator(IEnumerator enumerator)
			{
				_enumerator = enumerator;
			}

			/// <summary>
			/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
			/// </summary>
			/// <filterpriority>2</filterpriority>
			public void Dispose()
			{
				var disposable = _enumerator as IDisposable;
				if (null != disposable)
					disposable.Dispose();
			}

			/// <summary>
			/// Advances the enumerator to the next element of the collection.
			/// </summary>
			/// <returns>
			/// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
			/// </returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception><filterpriority>2</filterpriority>
			public bool MoveNext()
			{
				return _enumerator.MoveNext();
			}

			/// <summary>
			/// Sets the enumerator to its initial position, which is before the first element in the collection.
			/// </summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception><filterpriority>2</filterpriority>
			public void Reset()
			{
				_enumerator.Reset();
			}

			/// <summary>
			/// Gets the element in the collection at the current position of the enumerator.
			/// </summary>
			/// <returns>
			/// The element in the collection at the current position of the enumerator.
			/// </returns>
			public ICacheItemPolicyConfigurationItem Current
			{
				get { return (ICacheItemPolicyConfigurationItem)_enumerator.Current; }
			}

			/// <summary>
			/// Gets the current element in the collection.
			/// </summary>
			/// <returns>
			/// The current element in the collection.
			/// </returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception><filterpriority>2</filterpriority>
			object IEnumerator.Current
			{
				get { return Current; }
			}
		}
	}
}