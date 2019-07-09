using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using Common.VertexObject;

namespace Common.Geometry
{
    public class LineSegment : ReadOnlyCollection<Vector2>, IVertexObject
    {
        public LineSegment(Vector2 _start, Vector2 _end) : base(new List<Vector2>(new[] { _start, _end }))
        {
        }

        public Vector2 End => this[1];

        public float Length => Vector.Length();
        public Vector2 Start => this[0];
        private Vector2 Vector => End - Start;
        public Vector2 Direction => Vector2.Normalize(Vector);
    }
}