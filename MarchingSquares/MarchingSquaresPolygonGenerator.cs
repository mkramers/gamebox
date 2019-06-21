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
        private static Dictionary<byte, IVertexObject> SegmentLookupTable =>
            new Dictionary<byte, IVertexObject>
            {
                {0, null},
                {1, new LineSegment(new Vector2(0, 0.5f), new Vector2(0.5f, 1.0f))},
                {2, new LineSegment(new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f))},
                {3, new LineSegment(new Vector2(0, 0.5f), new Vector2(1.0f, 0.5f))},

                {4, new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f))},
                {
                    5, new LineSegmentCollection(new[]
                    {
                        new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f)),
                        new LineSegment(new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f))
                    })
                },
                {6, new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f))},
                {7, new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f))},

                {8, new LineSegment(new Vector2(0.0f, 0.0f), new Vector2(0.5f, 0.5f))},
                {9, new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f))},
                {
                    10, new LineSegmentCollection(new[]
                    {
                        new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f)),
                        new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f))
                    })
                },
                {11, new LineSegment(new Vector2(0.5f, 0.5f), new Vector2(1.0f, 0.0f))},

                {12, new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(1.0f, 0.5f))},
                {13, new LineSegment(new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f))},
                {14, new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f))},
                {15, null}
            };

        public static IEnumerable<IVertexObject> GeneratePolygons(Grid<byte> _classifiedCells)
        {
            Polygon polygon = new Polygon(1);

            foreach (GridCell<byte> classifiedCell in _classifiedCells)
            {
                IEnumerable<Vector2> lines = SegmentLookupTable[classifiedCell.Value];
                if (lines == null)
                {
                    continue;
                }

                IEnumerable<Vector2> adjustedLine = lines.Select(_vertex => _vertex += new Vector2(classifiedCell.X, classifiedCell.Y));
                polygon.AddRange(adjustedLine);
            }

            return new[] { polygon };
        }
    }
}