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

        public IBody CreateVertexBody(IVertexObject _vertexObject, Vector2 _position, float _mass, BodyType _bodyType)
        {
            Aether.Physics2D.Dynamics.Body physicsBody = World.CreatePolygon(_vertexObject.GetVertices(), _mass,
                _position.GetVector2(), 0, _bodyType);
            
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