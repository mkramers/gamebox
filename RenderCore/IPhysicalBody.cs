using System;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using SFML.Graphics;

namespace RenderCore
{
    public interface IEntity : IPhysicalBody, IDrawable
    {
    }

    public class PhysicalBodyEntity : IEntity
    {
        private readonly Drawable m_drawable;
        private readonly IPhysicalBody m_body;

        public PhysicalBodyEntity(Drawable _drawable, IPhysicalBody _body)
        {
            m_drawable = _drawable;
            m_body = _body;
        }

        public void Dispose()
        {
            m_body.Dispose();
        }

        public Vector3 GetPosition()
        {
            return m_body.GetPosition();
        }
        
        public Drawable GetDrawable()
        {
            return m_drawable;
        }

        public BodyDescription GetBodyDescription()
        {
            return m_body.GetBodyDescription();
        }

        public void RemoveFromSimulation()
        {
            m_body.RemoveFromSimulation();
        }
    }

    public interface IDrawable
    {
        Drawable GetDrawable();
    }

    public interface IBody : IDisposable
    {
        Vector3 GetPosition();

        void RemoveFromSimulation();
    }

    public interface IPhysicalBody : IBody
    {
        BodyDescription GetBodyDescription();
    }
    
    public class PhysicalBody : IPhysicalBody
    {
        private readonly int m_bodyIndex;
        private readonly TypedIndex m_shapeIndex;
        private readonly Simulation m_simulation;

        public PhysicalBody(TypedIndex _shapeIndex, int _bodyIndex, Simulation _simulation)
        {
            m_shapeIndex = _shapeIndex;
            m_bodyIndex = _bodyIndex;
            m_simulation = _simulation;
        }

        public BodyDescription GetBodyDescription()
        {
            m_simulation.Bodies.GetDescription(m_bodyIndex, out BodyDescription bodyDescription);

            return bodyDescription;
        }

        public Vector3 GetPosition()
        {
            BodyDescription bodyDescription = GetBodyDescription();
            return bodyDescription.Pose.Position;
        }

        public void RemoveFromSimulation()
        {
            m_simulation.Shapes.Remove(m_shapeIndex);
            m_simulation.Bodies.Remove(m_bodyIndex);
        }

        public void Dispose()
        {
            RemoveFromSimulation();
        }
    }

    public interface IStaticBody : IBody
    {
        StaticDescription GetStaticDescription();
    }

    public class StaticBody : IStaticBody
    {
        private readonly int m_handle;
        private readonly Simulation m_simulation;

        public StaticBody(int _handle, Simulation _simulation)
        {
            m_handle = _handle;
            m_simulation = _simulation;
        }

        public Vector3 GetPosition()
        {
            StaticDescription staticDescription = GetStaticDescription();
            return staticDescription.Pose.Position;
        }

        public StaticDescription GetStaticDescription()
        {
            m_simulation.Statics.GetDescription(m_handle, out StaticDescription staticDescription);
            return staticDescription;
        }

        public void Dispose()
        {
            RemoveFromSimulation();
        }

        public void RemoveFromSimulation()
        {
            m_simulation.Statics.Remove(m_handle);
        }
    }
}