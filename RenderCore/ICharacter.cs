using System;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;

namespace RenderCore
{
    public interface ICharacter
    {
        BodyDescription GetBodyDescription();
    }

    public class TestCharacter : IDisposable
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
}