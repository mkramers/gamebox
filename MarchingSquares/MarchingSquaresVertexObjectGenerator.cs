using System;
using System.Collections.Generic;
using Common.Geometry;
using Common.VertexObject;

namespace MarchingSquares
{
    public static class MarchingSquaresVertexObjectGenerator
    {
        public static IEnumerable<IVertexObject> GenerateVertexObjects<T>(
            this MarchingSquaresGenerator<T> _marchingSquaresGenerator, IVertexObjectsGenerator _generator)
            where T : IComparable
        {
            IEnumerable<LineSegment> lineSegments = _marchingSquaresGenerator.GetLineSegments();

            IEnumerable<IVertexObject> polygons = _generator.GetVertexObjects(lineSegments);
            return polygons;
        }
    }
}