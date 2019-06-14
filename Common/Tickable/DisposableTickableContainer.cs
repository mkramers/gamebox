using System;

namespace Common.Tickable
{
    public class DisposableTickableContainer<T> : TickableContainer<T> where T : ITickable, IDisposable
    {
        protected override void Dispose(bool _disposing)
        {
            foreach (T disposable in this)
            {
                disposable.Dispose();
            }

            base.Dispose(_disposing);
        }
    }
}