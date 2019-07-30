using System.Numerics;
using Aether.Physics2D.Dynamics;
using Aether.Physics2D.Dynamics.Contacts;
using LibExtensions;

namespace PhysicsCore
{
    public interface ICollidable
    {
        event OnCollisionEventHandler Collided;
        event OnSeparationEventHandler Separated;
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

        public void ApplyForce(Vector2 _force)
        {
            m_body.ApplyForce(_force.GetVector2());
        }

        public void ApplyLinearImpulse(Vector2 _force)
        {
            m_body.ApplyLinearImpulse(_force.GetVector2());
        }

        public void RemoveFromWorld()
        {
            World world = m_body.World;
            world?.Remove(m_body);
        }

        public void SetPosition(Vector2 _position)
        {
            m_body.Position = _position.GetVector2();
        }

        public event OnCollisionEventHandler Collided
        {
            add => m_body.OnCollision += value;
            remove => m_body.OnCollision -= value;
        }
        public event OnSeparationEventHandler Separated
        {
            add => m_body.OnSeparation += value;
            remove => m_body.OnSeparation -= value;
        }
    }
}