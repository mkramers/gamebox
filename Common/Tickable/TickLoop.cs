using System;
using System.Diagnostics;
using System.Threading;
using Common.Extensions;

namespace Common.Tickable
{
    public class TickLoop : ITickLoop
    {
        private readonly Stopwatch m_stopwatch;
        private bool m_isRunning;

        public TickLoop(TimeSpan _interval)
        {
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

                Thread.Sleep(30);
            }
        }

        public void StopLoop()
        {
            m_isRunning = false;
        }

        public event EventHandler<TimeElapsedEventArgs> Tick;
    }
}