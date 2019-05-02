using System;

namespace RenderCore
{
    public interface ITickable
    {
        void Tick(TimeSpan _elapsed);
    }
}