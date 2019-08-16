using System;

namespace Common.Tickable
{
    public interface ITickLoop
    {
        void StartLoop();
        void StopLoop();
        event EventHandler<TimeElapsedEventArgs> Tick;
    }
}