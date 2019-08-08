using System;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Aether.Physics2D.Dynamics.Contacts;
using LibExtensions;
// ReSharper disable MemberCanBePrivate.Global

namespace PhysicsCore
{
    public class CollisionEventArgs : EventArgs
    {
        public CollisionEventArgs(Fixture _sender, Fixture _other, Contact _contact)
        {
            Sender = _sender;
            Other = _other;
            Contact = _contact;
        }

        public Fixture Sender { get; }
        public Fixture Other { get; }
        public Contact Contact { get; }

        public bool AllowCollision { get; set; } = true;
    }

    public class SeparationEventArgs : EventArgs
    {
        public SeparationEventArgs(Fixture _sender, Fixture _other, Contact _contact)
        {
            Sender = _sender;
            Other = _other;
            Contact = _contact;
        }

        public Fixture Sender { get; }
        public Fixture Other { get; }
        public Contact Contact { get; }
    }

    public interface ICollidable
    {
        event EventHandler<CollisionEventArgs> Collided;
        event EventHandler<SeparationEventArgs> Separated;

        bool ContainsFixture(Fixture _fixture);
    }

    public class Body : IBody
    {
        private readonly Aether.Physics2D.Dynamics.Body m_body;

        public Body(Aether.Physics2D.Dynamics.Body _body)
        {
            m_body = _body;
            m_body.OnSeparation += OnSeparation;
            m_body.OnCollision += OnCollision;
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

        public event EventHandler<CollisionEventArgs> Collided;
        public event EventHandler<SeparationEventArgs> Separated;

        public bool ContainsFixture(Fixture _fixture)
        {
            return m_body.FixtureList.Contains(_fixture);
        }

        private void OnSeparation(Fixture _sender, Fixture _other, Contact _contact)
        {
            Separated?.Invoke(this, new SeparationEventArgs(_sender, _other, _contact));
        }

        private bool OnCollision(Fixture _sender, Fixture _other, Contact _contact)
        {
            CollisionEventArgs collisionEventArgs = new CollisionEventArgs(_sender, _other, _contact);

            Collided?.Invoke(this, collisionEventArgs);

            return collisionEventArgs.AllowCollision;
        }
    }
}