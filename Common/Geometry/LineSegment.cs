using System.Numerics;

namespace Common.Geometry
{
    public class LineSegment
    {
        public LineSegment(Vector2 _start, Vector2 _end)
        {
            Start = _start;
            End = _end;
        }

        private Vector2 End { get; }

        public float Length => Vector.Length();
        public Vector2 Start { get; }
        private Vector2 Vector => End - Start;
        public Vector2 Direction => Vector2.Normalize(Vector);
    }
}