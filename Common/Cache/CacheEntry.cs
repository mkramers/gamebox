using System;

namespace Common.Cache
{
    public class CacheEntry<T, TY> : ICacheEntry<T, TY> where T : class where TY : IEquatable<TY>
    {
        public T CachedObject { get; }
        public TY Id { get; }

        public CacheEntry(TY _id, T _cachedObject)
        {
            Id = _id;
            CachedObject = _cachedObject;
        }
    }
}