using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using Common.VertexObject;

namespace Common.Geometry
{
    public class LineSegmentCollection : ReadOnlyCollection<Vector2>, IVertexObject
    {
        public LineSegmentCollection(IEnumerable<LineSegment> _lineSegments) : base(_lineSegments.SelectMany(_lineSegment => new[] { _lineSegment.Start, _lineSegment.End }).ToList())
        {
        }
    }

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