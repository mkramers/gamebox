using System;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities.Memory;
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

            //Drop a ball on a big static box.
            Sphere sphere = new Sphere(1);
            sphere.ComputeInertia(1, out BodyInertia sphereInertia);

            Vector3 position = new Vector3(0, 5, 0);
            TypedIndex sphereReference = simulation.Shapes.Add(sphere);
            CollidableDescription collidableDescription = new CollidableDescription(sphereReference, 0.1f);
            BodyActivityDescription bodyActivityDescription = new BodyActivityDescription(0.01f);

            BodyDescription bodyDescription = BodyDescription.CreateDynamic(position, sphereInertia,
                collidableDescription, bodyActivityDescription);

            int bodyHandle = simulation.Bodies.Add(bodyDescription);

            Vector3 staticPosition = new Vector3(0, 0, 0);
            Box staticBox = new Box(500, 1, 500);
            TypedIndex boxShapeReference = simulation.Shapes.Add(staticBox);
            CollidableDescription staticCollidable = new CollidableDescription(boxShapeReference, 0.1f);
            StaticDescription staticDescription = new StaticDescription(staticPosition, staticCollidable);

            simulation.Statics.Add(staticDescription);

            for (int i = 0; i < 100; ++i)
            {
                simulation.Timestep(0.01f);

                simulation.Bodies.GetDescription(bodyHandle, out BodyDescription foundBody);
                Console.WriteLine(foundBody.Pose.Position.ToString());
            }

            simulation.Dispose();
            bufferPool.Clear();
        }
    }
}