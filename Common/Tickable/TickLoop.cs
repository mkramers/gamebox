using System;
using System.Diagnostics;
using System.Threading;
using Common.Extensions;

namespace Common.Tickable
{
    public class TickLoop : ITickLoop
    {
        private readonly TimeSpan m_intervalMs;
        private readonly Stopwatch m_stopwatch;
        private bool m_isRunning;

        public TickLoop(TimeSpan _intervalMs)
        {
            m_intervalMs = _intervalMs;
            m_stopwatch = Stopwatch.StartNew();
            m_isRunning = false;
        }

        public void StartLoop()
        {
            m_isRunning = true;
            while (m_isRunning)
            {
                TimeSpan elapsed = m_stopwatch.GetElapsedAndRestart();

                Tick?.Invoke(this, new TimeElapsedEventArgs(elapsed));

                Thread.Sleep(m_intervalMs);
            }
        }

        public void StopLoop()
        {
            m_isRunning = false;
        }

        public event EventHandler<TimeElapsedEventArgs> Tick;
    }
}