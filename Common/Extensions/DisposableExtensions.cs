using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Extensions
{
    public static class DisposableExtensions
    {
        public static void DisposeAllAndClear<T>(this IList<T> _items) where T : IDisposable
        {
            foreach (T disposable in _items)
            {
                disposable.Dispose();
            }

            _items.Clear();
        }
    }
}
