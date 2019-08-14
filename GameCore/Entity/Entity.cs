using System;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using PhysicsCore;
using RenderCore.Drawable;
using SFML.Graphics;
using Body = Aether.Physics2D.Dynamics.Body;

namespace GameCore.Entity
{
    public class Entity : IEntity
    {
        private readonly IBody m_body;
        private readonly IPositionDrawable m_drawable;

        public Entity(IPositionDrawable _drawable, IBody _body)
        {
            m_drawable = _drawable;
            m_body = _body;
        }

        public Vector2 GetPosition()
        {
            return m_body.GetPosition();
        }

        public void ApplyForce(Vector2 _force)
        {
            m_body.ApplyForce(_force);
        }

        public void ApplyLinearImpulse(Vector2 _force)
        {
            m_body.ApplyLinearImpulse(_force);
        }

        public void RemoveFromWorld()
        {
            m_body.RemoveFromWorld();
        }

        public Body GetBody()
        {
            return m_body.GetBody();
        }

        public void SetPosition(Vector2 _position)
        {
            m_body.SetPosition(_position);
        }

        public void Tick(TimeSpan _elapsed)
        {
            Vector2 position = m_body.GetPosition();
            m_drawable?.SetPosition(position);
        }

        public void Dispose()
        {
            RemoveFromWorld();

            m_drawable?.Dispose();
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            m_drawable?.Draw(_target, _states);
        }

        public event EventHandler<CollisionEventArgs> Collided
        {
            add => m_body.Collided += value;
            remove => m_body.Collided -= value;
        }

        public event EventHandler<SeparationEventArgs> Separated
        {
            add => m_body.Separated += value;
            remove => m_body.Separated -= value;
        }

        public bool ContainsFixture(Fixture _fixture)
        {
            return m_body.ContainsFixture(_fixture);
        }
    }
}