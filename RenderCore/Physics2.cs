using System;
using System.Numerics;
using BepuPhysics;
using BepuUtilities.Memory;
using RenderCore.Physics;

namespace RenderCore
{
    public class Physics2 : ITickable, IDisposable
    {
        public Simulation Simulation { get; }

        public Physics2(BufferPool _bufferPool)
        {
            Vector3 gravity = new Vector3(0, -10, 0);

            Simulation = Simulation.Create(_bufferPool, new NarrowPhaseCallbacks(),
                new PoseIntegratorCallbacks(gravity));
        }

        public void Tick(long _elapsedMs)
        {
            Simulation.Timestep(_elapsedMs / 100.0f);
        }

        public void Dispose()
        {
            Simulation?.Dispose();
        }
    }
}