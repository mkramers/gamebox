using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Common.Cache
{
    public class Cache<T, TY> where T : class
    {
        private readonly List<ICacheEntry<T, TY>> m_entries;

        protected Cache()
        {
            m_entries = new List<ICacheEntry<T, TY>>();
        }
        
        public static Cache<T, TY> Instance { get; } = new Cache<T, TY>();

        public T GetObject(TY _id)
        {
            ICacheEntry<T, TY> existingEntry = FindCacheEntry(_id);
            if (existingEntry == null)
            {
                throw new Exception("Item does not exist in cache!");
            }

            T cachedObject = existingEntry.CachedObject;
            return cachedObject;
        }

        private ICacheEntry<T, TY> FindCacheEntry(TY _id)
        {
            ICacheEntry<T, TY> existingEntry = m_entries.Find(_entry => _entry.Id.Equals(_id));
            return existingEntry;
        }

        public void AddObject(TY _id, T _value)
        {
            ICacheEntry<T, TY> existingEntry = FindCacheEntry(_id);
            if (existingEntry != null)
            {
                throw new Exception("Item already exists in cache!");
            }

            ICacheEntry<T, TY> entry = new CacheEntry<T, TY>(_id, _value);
            m_entries.Add(entry);
        }
    }
}