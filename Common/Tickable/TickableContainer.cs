using System;
using System.Collections.Concurrent;

namespace Common.Tickable
{
    public class TickableContainer<T> : BlockingCollection<T>, ITickable where T : ITickable
    {
        public void Tick(TimeSpan _elapsed)
        {
            T[] tickables = ToArray();
            foreach (T tickable in tickables)
            {
                tickable.Tick(_elapsed);
            }
        }
    }
}