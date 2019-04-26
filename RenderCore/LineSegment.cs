using System.Numerics;

namespace RenderCore
{
    public class LineSegment
    {
        public Vector2 End { get; }

        public LineSegment(Vector2 _start, Vector2 _end)
        {
            Start = _start;
            End = _end;
        }

        public float Length => Vector.Length();
        public Vector2 Start { get; }
        public Vector2 Vector => End - Start;
        public Vector2 Direction => Vector2.Normalize(Vector);
    }
}