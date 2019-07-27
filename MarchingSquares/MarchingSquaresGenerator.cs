using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Common.Geometry;
using Common.Grid;

namespace MarchingSquares
{
    public class MarchingSquaresGenerator<T> where T : IComparable
    {
        private readonly Grid<T> m_grid;
        private readonly T m_threshold;

        public MarchingSquaresGenerator(Grid<T> _grid, T _threshold)
        {
            m_grid = _grid;
            m_threshold = _threshold;
        }

        private static Dictionary<byte, IEnumerable<LineSegment>> SegmentLookupTable =>
            new Dictionary<byte, IEnumerable<LineSegment>>
            {
                {0, null},
                {1, new[] {new LineSegment(new Vector2(0, 0.5f), new Vector2(0.5f, 1.0f))}},
                {2, new[] {new LineSegment(new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f))}},
                {3, new[] {new LineSegment(new Vector2(0, 0.5f), new Vector2(1.0f, 0.5f))}},

                {4, new[] {new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f))}},
                {
                    5, new[]
                    {
                        new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f)),
                        new LineSegment(new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f))
                    }
                },
                {6, new[] {new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f))}},
                {7, new[] {new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f))}},

                {8, new[] {new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f))}},
                {9, new[] {new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f))}},
                {
                    10, new[]
                    {
                        new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f)),
                        new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f))
                    }
                },
                {11, new[] {new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f))}},

                {12, new[] {new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(1.0f, 0.5f))}},
                {13, new[] {new LineSegment(new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f))}},
                {14, new[] {new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f))}},
                {15, null}
            };

        private static IEnumerable<LineSegment> GetLineSegments(Grid<byte> _classifiedCells)
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

        public IEnumerable<LineSegment> GetLineSegments()
        {
            Grid<bool> binaryMask = BinaryMaskCreator.CreateBinaryMask(m_grid, m_threshold);

            Grid<byte> classifiedCells = MarchingSquaresClassifier.ClassifyCells(binaryMask);

            IEnumerable<LineSegment> lineSegments = GetLineSegments(classifiedCells);
            return lineSegments;
        }
    }
}