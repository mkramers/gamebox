namespace Common.Geometry
{
    public static class LineSegmentExtensions
    {
        public static LineSegment GetFlipped(this LineSegment _lineSegment)
        {
            return new LineSegment(_lineSegment.End, _lineSegment.Start);
        }
    }
}