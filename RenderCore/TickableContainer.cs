using System.Collections.Concurrent;
using System.Diagnostics;

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
            long elapsed = m_stopwatch.ElapsedMilliseconds;

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