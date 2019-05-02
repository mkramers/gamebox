
using SFML.Graphics;
using Vector2 = System.Numerics.Vector2;

namespace RenderCore
{
    public interface IEntity : IBody, IDrawable, ITickable
    {
    }

    public interface IDrawable
    {
        Drawable GetDrawable();
    }

    public interface IBody
    {
        Vector2 GetPosition();
        void ApplyForce(NormalForce _force);

        void RemoveFromWorld();
    }

    public class Body : IBody
    {
        private readonly Aether.Physics2D.Dynamics.Body m_body;

        public Body(Aether.Physics2D.Dynamics.Body _body)
        {
            m_body = _body;
        }

        public Vector2 GetPosition()
        {
            return m_body.Position.GetVector2();
        }

        public void ApplyForce(NormalForce _force)
        {
            m_body.ApplyLinearImpulse(_force.ForceVector.GetVector2());
        }

        public void RemoveFromWorld()
        {
            var world = m_body.World;
            world.Remove(m_body);
        }
    }

    public class Entity : IEntity
    {
        private readonly Sprite m_sprite;
        private readonly IBody m_body;

        public Entity(Sprite _sprite, IBody _body)
        {
            m_sprite = _sprite;
            m_body = _body;
        }

        public void Dispose()
        {
            m_sprite.Dispose();
        }

        public Vector2 GetPosition()
        {
            return m_body.GetPosition();
        }

        public void ApplyForce(NormalForce _force)
        {
            m_body.ApplyForce(_force);
        }

        public void RemoveFromWorld()
        {
            m_body.RemoveFromWorld();
        }


        public Drawable GetDrawable()
        {
            return m_sprite;
        }

        public void Tick(long _elapsedMs)
        {
            Vector2 position = m_body.GetPosition();
            m_sprite.Position = position.GetVector2f();
        }
    }
}