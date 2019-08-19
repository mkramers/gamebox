using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Cache
{
    public class Cache<TValue, TType> : ICache<TValue, TType> where TValue : class
    {
        private readonly List<ICacheEntry<TValue, TType>> m_entries;

        public Cache()
        {
            m_entries = new List<ICacheEntry<TValue, TType>>();
        }

        public TValue GetObject(TType _id)
        {
            ICacheEntry<TValue, TType> existingEntry = FindCacheEntry(_id);
            if (existingEntry == null)
            {
                throw new Exception("Item does not exist in cache!");
            }

            TValue cachedObject = existingEntry.CachedObject;
            return cachedObject;
        }

        private ICacheEntry<TValue, TType> FindCacheEntry(TType _id)
        {
            ICacheEntry<TValue, TType> existingEntry = m_entries.Find(_entry => _entry.Id.Equals(_id));
            return existingEntry;
        }

        public bool EntryExists(TType _id)
        {
            bool entryExists = m_entries.Any(_entry => _entry.Id.Equals(_id));
            return entryExists;
        }

        public void AddObject(TType _id, TValue _value)
        {
            ICacheEntry<TValue, TType> existingEntry = FindCacheEntry(_id);
            if (existingEntry != null)
            {
                throw new Exception("Item already exists in cache!");
            }

            ICacheEntry<TValue, TType> entry = new CacheEntry<TValue, TType>(_id, _value);
            m_entries.Add(entry);
        }
    }
}