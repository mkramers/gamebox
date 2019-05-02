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

        public EntityPhysics(Vector2 _gravity) : base(_gravity)
        {
            m_entities = new List<IEntity>();
        }

        public override void Tick(TimeSpan _elapsed)
        {
            base.Tick(_elapsed);

            foreach (IEntity entity in m_entities)
            {
                entity.Tick(_elapsed);
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

        public Physics2(Vector2 _gravity)
        {
            m_world = new World(_gravity.GetVector2());
        }

        public virtual void Tick(TimeSpan _elapsed)
        {
            m_world.Step(_elapsed);
        }

        public void Dispose()
        {
            m_world.Clear();
        }

        public IBody CreateBody(Vector2 _position, float _mass, BodyType _bodyType)
        {
            Aether.Physics2D.Dynamics.Body physicsBody = m_world.CreateRectangle(1, 1, _mass, _position.GetVector2(), 0, _bodyType);
            physicsBody.SetRestitution(0.3f);
            physicsBody.SetFriction(0.33f);

            m_world.Add(physicsBody);

            Body body = new Body(physicsBody);
            return body;
        }
    }
}