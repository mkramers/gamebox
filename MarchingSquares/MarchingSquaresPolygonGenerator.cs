﻿using System;
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

        public static IEnumerable<IVertexObject> GeneratePolygons(Grid<byte> _classifiedCells)
        {
            List<LineSegment> lineSegments = new List<LineSegment>();

            foreach (GridCell<byte> classifiedCell in _classifiedCells)
            {
                IEnumerable<LineSegment> lines = SegmentLookupTable[classifiedCell.Value];
                if (lines == null)
                {
                    continue;
                }

                Vector2 positionOffset = new Vector2(classifiedCell.X, classifiedCell.Y);
                IEnumerable<LineSegment> adjustedLines = lines.Select(_lineSegment => new LineSegment(_lineSegment.Start + positionOffset, _lineSegment.End + positionOffset));
                lineSegments.AddRange(adjustedLines);
            }

            IEnumerable<IVertexObject> polygons = VertexObjectHeadTailBuilder.GetVertexObjects(lineSegments);
            return polygons;
        }
    }

    public static class VertexObjectHeadTailBuilder
    {
        public static IEnumerable<IVertexObject> GetVertexObjects(IEnumerable<LineSegment> _lineSegments)
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

        private static bool IsConnectedEndToStart(this LineSegment _lineSegment, LineSegment _otherLineSegment)
        {
            bool isConnected = _lineSegment.End == _otherLineSegment.Start;
            return isConnected;
        }

        private static bool ContainsLineSegment(this IVertexObject _vertexObject, LineSegment _lineSegment)
        {
            bool belongs = _vertexObject.Contains(_lineSegment.Start) && _vertexObject.Contains(_lineSegment.End);
            return belongs;
        }
    }
}