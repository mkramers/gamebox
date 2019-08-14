using System.Collections.Generic;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.Geometry;
using Common.VertexObject;
using LibExtensions;

namespace PhysicsCore
{
    public static class BodyFactory
    {
        public static IBody CreateVertexBody(IVertexObject _vertexObject, Vector2 _position, float _mass, BodyType _bodyType)
        {
            Aether.Physics2D.Dynamics.Body physicsBody = new Aether.Physics2D.Dynamics.Body
            {
                Position = _position.GetVector2(),
                Rotation = 0,
                BodyType = _bodyType
            };

            physicsBody.CreatePolygon(_vertexObject.GetVertices(), _mass);

            Body body = new Body(physicsBody);
            return body;
        }

        public static IBody CreateEdges(IEnumerable<LineSegment> _lineSegments, Vector2 _position)
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

            return new Body(body);
        }
    }
}