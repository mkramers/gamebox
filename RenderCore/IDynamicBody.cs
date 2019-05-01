using System;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public interface IDynamicEntity : IDynamicBody, IDrawable, ITickable
    {
    }

    public class DynamicBodyEntity : IDynamicEntity
    {
        private readonly Sprite m_sprite;
        private readonly IDynamicBody m_body;

        public DynamicBodyEntity(Sprite _sprite, IDynamicBody _body)
        {
            m_sprite = _sprite;
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
            return m_sprite;
        }
        
        public BodyDescription GetBodyDescription()
        {
            return m_body.GetBodyDescription();
        }

        public void ApplyForce(NormalForce _force)
        {
            m_body.ApplyForce(_force);
        }

        public void RemoveFromSimulation()
        {
            m_body.RemoveFromSimulation();
        }

        public void Tick(long _elapsedMs)
        {
            Vector3 position = m_body.GetPosition();
            m_sprite.Position = position.GetVector2f();
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

    public interface IDynamicBody : IBody
    {
        BodyDescription GetBodyDescription();
        void ApplyForce(NormalForce _force);
    }
    
    public class DynamicBody : IDynamicBody
    {
        private readonly int m_bodyIndex;
        private readonly TypedIndex m_shapeIndex;
        private readonly Simulation m_simulation;

        public DynamicBody(TypedIndex _shapeIndex, int _bodyIndex, Simulation _simulation)
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

        public void ApplyForce(NormalForce _force)
        {
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
    public interface IStaticEntity : IStaticBody, IDrawable, ITickable
    {
    }

    public class StaticBodyEntity : IStaticEntity
    {
        private readonly Sprite m_sprite;
        private readonly IStaticBody m_body;

        public StaticBodyEntity(Sprite _sprite, IStaticBody _body)
        {
            m_sprite = _sprite;
            m_body = _body;
        }

        public void Dispose()
        {
            m_body.Dispose();
            m_sprite.Dispose();
        }

        public Vector3 GetPosition()
        {
            return m_body.GetPosition();
        }

        public void RemoveFromSimulation()
        {
            m_body.RemoveFromSimulation();
        }

        public StaticDescription GetStaticDescription()
        {
            return m_body.GetStaticDescription();
        }

        public Drawable GetDrawable()
        {
            return m_sprite;
        }


        public void Tick(long _elapsedMs)
        {
            Vector3 position = m_body.GetPosition();
            m_sprite.Position = position.GetVector2f();
        }
    }
}