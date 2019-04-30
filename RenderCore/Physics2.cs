using System;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities.Memory;
using RenderCore.Physics;

namespace RenderCore
{
    public class Physics2 : TickableContainer, IDisposable
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

        public IPhysicalBody CreatePhysicalObject()
        {
            Sphere sphere = new Sphere(1);
            sphere.ComputeInertia(1, out BodyInertia sphereInertia);

            Vector3 position = new Vector3(0, 5, 0);

            TypedIndex shapeIndex = Simulation.Shapes.Add(sphere);

            CollidableDescription collidableDescription = new CollidableDescription(shapeIndex, 0.1f);
            BodyActivityDescription bodyActivityDescription = new BodyActivityDescription(0.01f);

            BodyDescription bodyDescription = BodyDescription.CreateDynamic(position, sphereInertia,
                collidableDescription, bodyActivityDescription);

            int bodyIndex = Simulation.Bodies.Add(bodyDescription);

            PhysicalBody physicalBody = new PhysicalBody(shapeIndex, bodyIndex, Simulation);
            return physicalBody;
        }

        public IStaticBody CreateStaticObject()
        {
            Vector3 staticPosition = new Vector3(0, 0, 0);
            Box staticBox = new Box(500, 1, 500);
            TypedIndex boxShapeReference = Simulation.Shapes.Add(staticBox);
            CollidableDescription staticCollidable = new CollidableDescription(boxShapeReference, 0.1f);
            StaticDescription staticDescription = new StaticDescription(staticPosition, staticCollidable);

            int handle = Simulation.Statics.Add(staticDescription);

            StaticBody staticBody = new StaticBody(handle, Simulation);
            return staticBody;
        }
    }
}