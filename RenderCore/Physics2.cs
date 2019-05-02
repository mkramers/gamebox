using System;
using Aether.Physics2D.Common.Maths;
using Aether.Physics2D.Dynamics;
using Vector2 = System.Numerics.Vector2;

namespace RenderCore
{
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