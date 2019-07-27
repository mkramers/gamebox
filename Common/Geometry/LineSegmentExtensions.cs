using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Common.Geometry
{
    public static class LineSegmentExtensions
    {
        public static LineSegment GetTranslated(this LineSegment _lineSegment, Vector2 _translation)
        {
            return new LineSegment(_lineSegment.Start + _translation, _lineSegment.End + _translation);
        }

        public static bool ApproximatelyEqualTo(this LineSegment _lineSegmentA, LineSegment _lineSegmentB)
        {
            bool equalsAligned = _lineSegmentA.Start.ApproximatelyEqualTo(_lineSegmentB.Start) &&
                                 _lineSegmentA.End.ApproximatelyEqualTo(_lineSegmentB.End);
            bool equalsAntiAligned = _lineSegmentA.Start.ApproximatelyEqualTo(_lineSegmentB.End) &&
                                     _lineSegmentA.End.ApproximatelyEqualTo(_lineSegmentB.Start);
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

        private static string GetDisplayString(this LineSegment _lineSegment)
        {
            string displayString = $"{_lineSegment.Start.GetDisplayString()} => {_lineSegment.End.GetDisplayString()}";
            return displayString;
        }

        public static LineSegment GetFlipped(this LineSegment _lineSegment)
        {
            return new LineSegment(_lineSegment.End, _lineSegment.Start);
        }
    }
}