using System;
using System.Collections.Generic;
using Aether.Physics2D.Common.Maths;
using Aether.Physics2D.Dynamics;
using Vector2 = System.Numerics.Vector2;

namespace RenderCore
{
    public class EntityPhysics : Physics2
    {
        private readonly List<IEntity> m_entities;

        public EntityPhysics()
        {
            m_entities = new List<IEntity>();
        }

        public override void Tick(long _elapsedMs)
        {
            base.Tick(_elapsedMs);

            foreach (IEntity entity in m_entities)
            {
                entity.Tick(_elapsedMs);
            }
        }

        public void Add(IEntity _entity)
        {
            m_entities.Add(_entity);
        }
    }

    public class Physics2 : ITickable, IDisposable
    {
        private readonly World m_world;

        public Physics2()
        {
            Vector2 gravity = new Vector2(0, -10);
            m_world = new World();
        }

        public virtual void Tick(long _elapsedMs)
        {
            m_world.Step(10);
        }

        public void Dispose()
        {
            m_world.Clear();
        }

        public IBody CreateDynamicBody(float _mass)
        {
            Vector2 position = new Vector2(0, 5);

            Aether.Physics2D.Dynamics.Body physicsBody = m_world.CreateRectangle(1, 1, _mass, position.GetVector2(), 0, BodyType.Dynamic);
            physicsBody.SetRestitution(0.3f);
            physicsBody.SetFriction(0.5f);

            Body body = new Body(physicsBody);
            return body;
        }

        public IBody CreateStaticBody(Vector2 _position, float _mass)
        {
            Aether.Physics2D.Dynamics.Body physicsBody = m_world.CreateRectangle(1, 1, _mass, _position.GetVector2());
            physicsBody.SetRestitution(0.3f);
            physicsBody.SetFriction(0.5f);

            Body body = new Body(physicsBody);
            return body;
       }
    }
}