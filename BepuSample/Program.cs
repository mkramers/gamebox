using System;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities.Memory;
using RenderCore;
using RenderCore.Physics;

namespace BepuSample
{
    internal class Program
    {
        private static void Main(string[] _args)
        {
            BufferPool bufferPool = new BufferPool();

            Vector3 gravity = new Vector3(0, -10, 0);
            Simulation simulation = Simulation.Create(bufferPool, new NarrowPhaseCallbacks(),
                new PoseIntegratorCallbacks(gravity));

            TestCharacter tc = new TestCharacter(simulation);
            
            Vector3 staticPosition = new Vector3(0, 0, 0);
            Box staticBox = new Box(500, 1, 500);
            TypedIndex boxShapeReference = simulation.Shapes.Add(staticBox);
            CollidableDescription staticCollidable = new CollidableDescription(boxShapeReference, 0.1f);
            StaticDescription staticDescription = new StaticDescription(staticPosition, staticCollidable);

            simulation.Statics.Add(staticDescription);

            for (int i = 0; i < 100; ++i)
            {
                simulation.Timestep(0.01f);

                Vector3 bd = tc.GetPosition();
                Console.WriteLine(bd.ToString());
            }

            simulation.Dispose();
            bufferPool.Clear();
        }
    }
}