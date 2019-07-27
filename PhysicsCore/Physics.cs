using System;
using System.Collections.Generic;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.Geometry;
using Common.VertexObject;
using LibExtensions;

namespace PhysicsCore
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

        public IBody CreateEdges(IEnumerable<LineSegment> _lineSegments, Vector2 _position)
        {
            Aether.Physics2D.Dynamics.Body body = World.CreateBody(_position.GetVector2());

            foreach (LineSegment lineSegment in _lineSegments)
            {
                body.CreateEdge(lineSegment.Start.GetVector2(), lineSegment.End.GetVector2());
            }

            World.Add(body);

            return new Body(body);
        }

        public void SetGravity(Vector2 _gravity)
        {
            World.Gravity = _gravity.GetVector2();
        }
    }
}