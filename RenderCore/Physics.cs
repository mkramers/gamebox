using System;
using System.Numerics;
using Aether.Physics2D.Dynamics;

namespace RenderCore
{
    public class Physics : IPhysics, IDisposable
    {
        private World World { get; }

        public Physics(Vector2 _gravity)
        {
            World = new World(_gravity.GetVector2());
        }

        public void Dispose()
        {
            World.Clear();
        }

        public virtual void Tick(TimeSpan _elapsed)
        {
            World.Step(_elapsed);
        }

        public IBody CreateBody(Vector2 _position, float _mass, BodyType _bodyType)
        {
            Aether.Physics2D.Dynamics.Body physicsBody =
                World.CreateRectangle(1, 1, _mass, _position.GetVector2(), 0, _bodyType);

            physicsBody.SetRestitution(0.3f);
            physicsBody.SetFriction(0.33f);

            World.Add(physicsBody);

            Body body = new Body(physicsBody);
            return body;
        }

        public void SetGravity(Vector2 _gravity)
        {
            World.Gravity = _gravity.GetVector2();
        }
    }
}