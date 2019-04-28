using System;
using System.Collections.Concurrent;

namespace RenderCore
{
    public static class BlockingCollectionExtensions
    {
        public static void Clear<T>(this BlockingCollection<T> _blockingCollection)
        {
            if (_blockingCollection == null)
            {
                throw new ArgumentNullException("_blockingCollection");
            }

            while (_blockingCollection.Count > 0)
            {
                T item;
                _blockingCollection.TryTake(out item);
            }
        }
    }
}