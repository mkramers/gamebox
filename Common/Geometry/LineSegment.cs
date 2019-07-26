using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using Common.VertexObject;

namespace Common.Geometry
{

    public static class Vector2Extensions
    {
        public static IList<double[]> GetDoubleArrays(this IEnumerable<Vector2> _vectors)
        {
            List<double[]> doubleArrays = _vectors.Select(_vector => new double[] { _vector.X, _vector.Y }).ToList();
            return doubleArrays;
        }
        public static IEnumerable<Vector2> FromDoubleArrays(this IEnumerable<double[]> _doubleArrays)
        {
            IEnumerable<Vector2> vectors = _doubleArrays.Select(_doubleArray =>
                new Vector2((float)_doubleArray[0], (float)_doubleArray[1]));
            return vectors;
        }

        public static string GetDisplayString(this IEnumerable<Vector2> _vectors)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Vector2 vector in _vectors)
            {
                string displayString = vector.GetDisplayString();
                stringBuilder.AppendLine(displayString);
            }

            return stringBuilder.ToString();
        }

        public static string GetDisplayString(this Vector2 _vector)
        {
            return $"{{{_vector.X}, {_vector.Y}}}";
        }

        public static bool ApproximatelyEqualTo(this Vector2 _vectorA, Vector2 _vectorB)
        {
            const float tolerance = 0.0001f;
            return (_vectorA - _vectorB).Length() < tolerance;
        }
    }

    public static class LineSegmentExtensions
    {
        public static LineSegment GetTranslated(this LineSegment _lineSegment, Vector2 _translation)
        {
            return new LineSegment(_lineSegment.Start + _translation, _lineSegment.End + _translation);
        }

        public static bool ApproximatelyEqualTo(this LineSegment _lineSegmentA, LineSegment _lineSegmentB)
        {
            bool equalsAligned = _lineSegmentA.Start.ApproximatelyEqualTo(_lineSegmentB.Start) && _lineSegmentA.End.ApproximatelyEqualTo(_lineSegmentB.End);
            bool equalsAntiAligned = _lineSegmentA.Start.ApproximatelyEqualTo(_lineSegmentB.End) && _lineSegmentA.End.ApproximatelyEqualTo(_lineSegmentB.Start);
            return equalsAligned || equalsAntiAligned;
        }

        public static string GetDisplayString(this IEnumerable<LineSegment> _lineSegments)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (LineSegment lineSegment in _lineSegments)
            {
                string displayString = lineSegment.GetDisplayString();
                stringBuilder.AppendLine(
                    displayString);
            }

            return stringBuilder.ToString();
        }

        public static string GetDisplayString(this LineSegment _lineSegment)
        {
            string displayString = $"{_lineSegment.Start.GetDisplayString()} => {_lineSegment.End.GetDisplayString()}";
            return displayString;
        }

        public static LineSegment GetFlipped(this LineSegment _lineSegment)
        {
            return new LineSegment(_lineSegment.End, _lineSegment.Start);
        }
    }
    public class LineSegment : ReadOnlyCollection<Vector2>, IVertexObject
    {
        public LineSegment(Vector2 _start, Vector2 _end) : base(new List<Vector2>(new[] { _start, _end }))
        {
        }

        public LineSegment(float _startX, float _startY, float _endX, float _endY) : this(new Vector2(_startX, _startY),
            new Vector2(_endX, _endY))
        {
        }

        public Vector2 End => this[1];

        public float Length => Vector.Length();
        public Vector2 Start => this[0];
        private Vector2 Vector => End - Start;
        public Vector2 Direction => Vector2.Normalize(Vector);
    }
}