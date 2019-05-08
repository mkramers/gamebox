using System;
using System.Numerics;
using Aether.Physics2D.Dynamics;

namespace RenderCore
{
    public class Physics : IPhysics, IDisposable
    {
        public Physics(Vector2 _gravity)
        {
            World = new World(_gravity.GetVector2());
        }

        private World World { get; }

        public void Dispose()
        {
            World.Clear();
        }

        public void Tick(TimeSpan _elapsed)
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

        public IBody CreateEdge(Vector2 _edgeStart, Vector2 _edgeEnd)
        {
            Aether.Physics2D.Dynamics.Body edgeBody = World.CreateEdge(_edgeStart.GetVector2(), _edgeEnd.GetVector2());
            edgeBody.SetRestitution(0.3f);
            edgeBody.SetFriction(0.33f);

            Body body = new Body(edgeBody);
            return body;
        }
    }
}