using System;

namespace Common.Tickable
{
    public class TimeElapsedEventArgs : EventArgs
    {
        public TimeElapsedEventArgs(TimeSpan _elapsed)
        {
            Elapsed = _elapsed;
        }

        public TimeSpan Elapsed { get; }
    }
}