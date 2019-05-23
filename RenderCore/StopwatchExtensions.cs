using System;
using System.Diagnostics;

namespace RenderCore
{
    public static class StopwatchExtensions
    {
        public static TimeSpan GetElapsedAndRestart(this Stopwatch _stopwatch)
        {
            _stopwatch.Stop();

            TimeSpan elapsed = _stopwatch.Elapsed;

            _stopwatch.Restart();

            return elapsed;
        }
    }
}