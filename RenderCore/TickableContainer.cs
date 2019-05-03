using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace RenderCore
{
    public class TickableContainer : BlockingCollection<ITickable>, ITickable
    {
        public void Tick(TimeSpan _elapsed)
        {
            ITickable[] tickables = ToArray();
            foreach (ITickable tickable in tickables)
            {
                tickable.Tick(_elapsed);
            }
        }
    }
}