using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using SFML.Window;

namespace RenderCore
{
    public class TickableContainer
    {
        private readonly Stopwatch m_stopwatch;
        private readonly BlockingCollection<ITickable> m_tickables;

        public TickableContainer()
        {
            m_tickables = new BlockingCollection<ITickable>();

            m_stopwatch = new Stopwatch();
            m_stopwatch.Start();
        }

        public void Tick()
        {
            TimeSpan elapsed = m_stopwatch.Elapsed;

            foreach (ITickable tickable in m_tickables)
            {
                tickable.Tick(elapsed);
            }

            m_stopwatch.Restart();
        }

        public void Add(ITickable _tickable)
        {
            m_tickables.Add(_tickable);
        }
    }
}