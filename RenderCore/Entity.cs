using System;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class Entity : IEntity
    {
        private readonly IBody m_body;
        private readonly Sprite m_sprite;

        public Entity(Sprite _sprite, IBody _body)
        {
            m_sprite = _sprite;
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

        public Drawable GetDrawable()
        {
            return m_sprite;
        }

        public void Tick(TimeSpan _elapsed)
        {
            Vector2 position = m_body.GetPosition();
            m_sprite.Position = position.GetVector2F();
        }

        public void Dispose()
        {
            m_body.RemoveFromWorld();
            m_sprite.Dispose();
        }
    }
}