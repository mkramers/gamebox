using System;

namespace Common.Tickable
{
    public interface ITickable
    {
        void Tick(TimeSpan _elapsed);
    }
}