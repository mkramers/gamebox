using System;
using Common.Tickable;

namespace Common.Tests
{
    public class MockTickable : ITickable
    {
        public bool Ticked { get; private set; }
        public void Tick(TimeSpan _elapsed)
        {
            Ticked = true;
        }
    }
}