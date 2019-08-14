using System;
using System.Collections.Generic;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.Geometry;
using Common.VertexObject;
using LibExtensions;

namespace PhysicsCore
{
    public class Physics : IPhysics
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
            Aether.Physics2D.Dynamics.Body pBody = new Aether.Physics2D.Dynamics.Body
            {
                Position = _position.GetVector2(), Rotation = 0, BodyType = _bodyType
            };

            pBody.CreatePolygon(_vertexObject.GetVertices(), _mass);

            World.Add(pBody);

            Body body = new Body(pBody);
            return body;
        }

        public IBody CreateEdges(IEnumerable<LineSegment> _lineSegments, Vector2 _position)
        {
            Aether.Physics2D.Dynamics.Body body = new Aether.Physics2D.Dynamics.Body
            {
                Position = _position.GetVector2(),
                Rotation = 0,
                BodyType = BodyType.Static,
            };

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