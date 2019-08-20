using System.Collections.Generic;
using Common.Geometry;

namespace MarchingSquares
{
    public interface IVertexObjectsGenerator
    {
        IEnumerable<IVertexObject> GetVertexObjects(IEnumerable<LineSegment> _lineSegments);
    }
}