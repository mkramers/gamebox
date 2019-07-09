using System;
using System.Collections.Generic;
using Common.Geometry;
using Common.Grid;
using Common.VertexObject;

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

        public IEnumerable<IVertexObject> Generate(IVertexObjectsGenerator _generator)
        {
            Grid<bool> binaryMask = BinaryMaskCreator.CreateBinaryMask(m_grid, m_threshold);

            Grid<byte> classifiedCells = MarchingSquaresClassifier.ClassifyCells(binaryMask);

            IEnumerable<LineSegment> lineSegments = MarchingSquaresPolygonGenerator.GetLineSegments(classifiedCells);

            IEnumerable<IVertexObject> polygons = _generator.GetVertexObjects(lineSegments);
            return polygons;
        }
    }
}