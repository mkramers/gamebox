using System;
using System.Diagnostics;

namespace Common.Extensions
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