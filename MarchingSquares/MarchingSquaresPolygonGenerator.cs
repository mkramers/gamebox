using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Common.Geometry;
using Common.Grid;
using Common.VertexObject;

namespace MarchingSquares
{
    public static class MarchingSquaresPolygonGenerator
    {
        private static Dictionary<byte, IEnumerable<LineSegment>> SegmentLookupTable =>
            new Dictionary<byte, IEnumerable<LineSegment>>
            {
                {0, null},
                {1, new []{new LineSegment(new Vector2(0, 0.5f), new Vector2(0.5f, 1.0f))}},
                {2, new []{new LineSegment(new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f))}},
                {3, new []{new LineSegment(new Vector2(0, 0.5f), new Vector2(1.0f, 0.5f))}},

                {4, new []{new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f))}},
                {5, new []{
                    new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f)),
                    new LineSegment(new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f))
                }},
                {6, new []{new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f))}},
                {7, new []{new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f))}},

                {8, new []{new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f))}},
                {9, new []{new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f))}},
                {10, new []{
                        new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f)),
                        new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f))
                }},
                {11, new []{new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f))}},

                {12, new []{new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(1.0f, 0.5f))}},
                {13, new []{new LineSegment(new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f))}},
                {14, new []{new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f))}},
                {15, null}
            };

        public static IEnumerable<LineSegment> GetLineSegments(Grid<byte> _classifiedCells)
        {
            List<LineSegment> lineSegments = new List<LineSegment>();

            for (int y = 0; y < _classifiedCells.Rows; y++)
            {
                for (int x = 0; x < _classifiedCells.Columns; x++)
                {
                    byte cell = _classifiedCells[x, y];
                    IEnumerable<LineSegment> lines = SegmentLookupTable[cell];
                    if (lines == null)
                    {
                        continue;
                    }

                    Vector2 positionOffset = new Vector2(x, y) + 0.5f * Vector2.One;

                    IEnumerable<LineSegment> adjustedLines = lines.Select(_lineSegment =>
                    {
                        Vector2 lineSegmentStart = _lineSegment.Start + positionOffset;
                        Vector2 lineSegmentEnd = _lineSegment.End + positionOffset;
                        LineSegment lineSegment = new LineSegment(lineSegmentStart, lineSegmentEnd);
                        return lineSegment;
                    });
                    lineSegments.AddRange(adjustedLines);
                }
            }

            return lineSegments;
        }
    }

    public interface IVertexObjectsGenerator
    {
        IEnumerable<IVertexObject> GetVertexObjects(IEnumerable<LineSegment> _lineSegments);
    }

    public class HeadToTailGenerator : IVertexObjectsGenerator
    {
        public IEnumerable<IVertexObject> GetVertexObjects(IEnumerable<LineSegment> _lineSegments)
        {
            List<LineSegment> lineSegments = _lineSegments.ToList();

            List<Polygon> vertexObjects = new List<Polygon>();

            Polygon polygon = GetVertexObject(lineSegments);
            vertexObjects.Add(polygon);

            return vertexObjects;
        }


        public Polygon GetVertexObject(IEnumerable<LineSegment> _lineSegments)
        {
            Polygon polygon = new Polygon();

            List<LineSegment> lineSegments = _lineSegments.ToList();

            Console.WriteLine($"GetVertexObject called with {lineSegments.Count} LineSegments");

            LineSegment currentLine = lineSegments.First();
            lineSegments.Remove(currentLine);

            polygon.AddRange(currentLine);

            int count = 0;
            int max = lineSegments.Count;
            while (lineSegments.Any() && count < max)
            {
                Console.WriteLine($"Looking for match of {currentLine.GetDisplayString()}\n\tin\n{lineSegments.Where(_lineSegment => _lineSegment != null).GetDisplayString()}");

                List<LineSegment> remainingLineSegments = new List<LineSegment>(lineSegments);

                foreach (LineSegment lineSegment in remainingLineSegments)
                {
                    bool isConnectedAtStart = currentLine.End.ApproximatelyEqualTo(lineSegment.Start);
                    bool isConnectedAtEnd = currentLine.End.ApproximatelyEqualTo(lineSegment.End);

                    if (!isConnectedAtStart && !isConnectedAtEnd)
                    {
                        continue;
                    }

                    LineSegment nextLine = lineSegment;
                    if (isConnectedAtEnd)
                    {
                        nextLine = lineSegment.GetFlipped();
                    }

                    Console.WriteLine($"Found match!\nline = {nextLine.GetDisplayString()}\ncurrentLineEnd = {currentLine.GetDisplayString()}\ncount = {count}\n");

                    currentLine = nextLine;

                    polygon.Add(nextLine.End);
                    lineSegments.Remove(lineSegment);
                    break;
                }

                count++;
            }

            return polygon;
        }
    }

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
}