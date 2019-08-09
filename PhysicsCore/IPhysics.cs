using System;
using System.Collections.Generic;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.Geometry;
using Common.Tickable;
using Common.VertexObject;

namespace PhysicsCore
{
    public interface IPhysics : ITickable, IDisposable
    {
        void SetGravity(Vector2 _gravity);
        IBody CreateVertexBody(IVertexObject _vertexObject, Vector2 _position, float _mass, BodyType _bodyType);
        IBody CreateEdges(IEnumerable<LineSegment> _lineSegments, Vector2 _position);
    }
}