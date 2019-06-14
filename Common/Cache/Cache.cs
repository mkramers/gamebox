using System;
using System.Collections.Generic;

namespace Common.Cache
{
    public abstract class Cache<T, TY> where T : class where TY : IEquatable<TY>
    {
        private readonly ICacheObjectProvider<T, TY> m_cacheObjectProvider;
        private readonly List<ICacheEntry<T, TY>> m_entries;

        protected Cache(ICacheObjectProvider<T, TY> _cacheObjectProvider)
        {
            m_cacheObjectProvider = _cacheObjectProvider;
            m_entries = new List<ICacheEntry<T, TY>>();
        }

        protected T GetObject(TY _id)
        {
            T cachedObject;

            ICacheEntry<T, TY> existingEntry = m_entries.Find(_entry => _entry.Id.Equals(_id));
            if (existingEntry == null)
            {
                cachedObject = m_cacheObjectProvider.GetObject(_id);

                ICacheEntry<T, TY> entry = new CacheEntry<T, TY>(_id, cachedObject);
                m_entries.Add(entry);
            }
            else
            {
                cachedObject = existingEntry.CachedObject;
            }

            return cachedObject;
        }
    }
}