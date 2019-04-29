using System;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;

namespace RenderCore
{
    public interface IBody : IDisposable
    {
        Vector3 GetPosition();
    }

    public interface ICharacter : IBody
    {
    }

    public class TestCharacter : ICharacter
    {
        private readonly int m_bodyIndex;
        private readonly TypedIndex m_shapeIndex;
        private readonly Simulation m_simulation;

        public TestCharacter(Simulation _simulation)
        {
            m_simulation = _simulation;

            Sphere sphere = new Sphere(1);
            sphere.ComputeInertia(1, out BodyInertia sphereInertia);

            Vector3 position = new Vector3(0, 5, 0);

            m_shapeIndex = _simulation.Shapes.Add(sphere);

            CollidableDescription collidableDescription = new CollidableDescription(m_shapeIndex, 0.1f);
            BodyActivityDescription bodyActivityDescription = new BodyActivityDescription(0.01f);

            BodyDescription bodyDescription = BodyDescription.CreateDynamic(position, sphereInertia,
                collidableDescription, bodyActivityDescription);

            m_bodyIndex = _simulation.Bodies.Add(bodyDescription);
        }

        private BodyDescription GetBodyDescription()
        {
            m_simulation.Bodies.GetDescription(m_bodyIndex, out BodyDescription bodyDescription);

            return bodyDescription;
        }

        public Vector3 GetPosition()
        {
            BodyDescription bodyDescription = GetBodyDescription();
            return bodyDescription.Pose.Position;
        }

        private void RemoveFromSimulation()
        {
            m_simulation.Shapes.Remove(m_shapeIndex);
            m_simulation.Bodies.Remove(m_bodyIndex);
        }

        public void Dispose()
        {
            RemoveFromSimulation();
        }
    }

    public interface ILandscape : IBody
    {

    }

    public class TestLandscape : ILandscape
    {
        private readonly int m_handle;
        private readonly Simulation m_simulation;

        public TestLandscape(Simulation _simulation)
        {
            m_simulation = _simulation;

            Vector3 staticPosition = new Vector3(0, 0, 0);
            Box staticBox = new Box(500, 1, 500);
            TypedIndex boxShapeReference = _simulation.Shapes.Add(staticBox);
            CollidableDescription staticCollidable = new CollidableDescription(boxShapeReference, 0.1f);
            StaticDescription staticDescription = new StaticDescription(staticPosition, staticCollidable);

            m_handle = _simulation.Statics.Add(staticDescription);
        }

        public Vector3 GetPosition()
        {
            StaticDescription staticDescription = GetStaticDescription();
            return staticDescription.Pose.Position;
        }

        private StaticDescription GetStaticDescription()
        {
            m_simulation.Statics.GetDescription(m_handle, out StaticDescription staticDescription);
            return staticDescription;
        }

        public void Dispose()
        {
            m_simulation.Statics.Remove(m_handle);
        }
    }
}