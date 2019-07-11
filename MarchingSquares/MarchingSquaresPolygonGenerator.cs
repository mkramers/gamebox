using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

            IEnumerable<LineSegment> lineSegments = _lineSegments as LineSegment[] ?? _lineSegments.ToArray();
            foreach (LineSegment lineSegment in lineSegments)
            {
                LineSegment nextSegment =
                    lineSegments.FirstOrDefault(_lineSegment => lineSegment.IsConnectedEndToStart(_lineSegment));
                if (nextSegment != null)
                {
                }
            }

            return polygon;
        }
    }

    public static class LineSegmentExtensions
    {
        public static bool IsConnectedEndToStart(this LineSegment _lineSegment, LineSegment _otherLineSegment)
        {
            bool isConnected = _lineSegment.End == _otherLineSegment.Start;
            return isConnected;
        }

        public static bool ContainsLineSegment(this IVertexObject _vertexObject, LineSegment _lineSegment)
        {
            bool belongs = _vertexObject.Contains(_lineSegment.Start) && _vertexObject.Contains(_lineSegment.End);
            return belongs;
        }
    }
}