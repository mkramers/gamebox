using System;
using System.Collections.Concurrent;

namespace RenderCore
{
    public static class BlockingCollectionExtensions
    {
        public static void Clear<T>(this BlockingCollection<T> _collection)
        {
            if (_collection == null)
            {
                throw new ArgumentNullException("_collection");
            }

            while (_collection.Count > 0)
            {
                _collection.TryTake(out T _);
            }
        }
    }
}