using System;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class Entity : IEntity
    {
        private readonly IBody m_body;
        private readonly IDrawable m_drawable;

        public Entity(IDrawable _drawable, IBody _body)
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

        public void SetPosition(Vector2 _position)
        {
            m_body.SetPosition(_position);
        }

        public void Tick(TimeSpan _elapsed)
        {
            Vector2 position = m_body.GetPosition();
            m_drawable.SetPosition(position);
        }

        public void Dispose()
        {
            RemoveFromWorld();

            m_drawable.Dispose();
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            m_drawable.Draw(_target, _states);
        }
    }
}