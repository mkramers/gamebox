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

                {8, new []{new LineSegment(new Vector2(0.0f, 0.0f), new Vector2(0.5f, 0.5f))}},
                {9, new []{new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f))}},
                {10, new []{
                        new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f)),
                        new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f))
                }},
                {11, new []{new LineSegment(new Vector2(0.5f, 0.5f), new Vector2(1.0f, 0.0f))}},

                {12, new []{new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(1.0f, 0.5f))}},
                {13, new []{new LineSegment(new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f))}},
                {14, new []{new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f))}},
                {15, null}
            };

        public static IEnumerable<LineSegment> GetLineSegments(Grid<byte> _classifiedCells)
        {
            List<LineSegment> lineSegments = new List<LineSegment>();

            GridBounds gridBounds = GridBounds.GetGridBounds(_classifiedCells);

            foreach (GridCell<byte> classifiedCell in _classifiedCells)
            {
                IEnumerable<LineSegment> lines = SegmentLookupTable[classifiedCell.Value];
                if (lines == null)
                {
                    continue;
                }

                Vector2 positionOffset = new Vector2(classifiedCell.X, classifiedCell.Y) + 0.5f * Vector2.One;

                IEnumerable<LineSegment> adjustedLines = lines.Select(_lineSegment =>
                {
                    Vector2 lineSegmentStart = _lineSegment.Start + positionOffset;
                    Vector2 lineSegmentEnd = _lineSegment.End + positionOffset;
                    LineSegment lineSegment = new LineSegment(lineSegmentStart, lineSegmentEnd);
                    return lineSegment;
                });
                lineSegments.AddRange(adjustedLines);
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
            LineSegment[] lineSegments = _lineSegments as LineSegment[] ?? _lineSegments.ToArray();

            List<Polygon> vertexObjects = new List<Polygon>();
            foreach (LineSegment lineSegment in lineSegments)
            {
                foreach (LineSegment otherLineSegment in lineSegments)
                {
                    if (!lineSegment.IsConnectedEndToStart(otherLineSegment))
                    {
                        continue;
                    }

                    Polygon existingVertexObject = vertexObjects.FirstOrDefault(_vertexObject => _vertexObject.ContainsLineSegment(lineSegment));
                    if (existingVertexObject != null)
                    {
                        if (!existingVertexObject.Contains(otherLineSegment.End))
                        {
                            existingVertexObject.Add(otherLineSegment.End);
                        }
                    }
                    else
                    {
                        Polygon polygon = new Polygon();
                        polygon.AddRange(lineSegment.ToArray());
                        polygon.Add(otherLineSegment.End);

                        vertexObjects.Add(polygon);
                    }
                }
            }

            return vertexObjects;
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