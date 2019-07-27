using System.Collections.Generic;
using Common.Geometry;
using Common.VertexObject;

namespace MarchingSquares
{
    public interface IVertexObjectsGenerator
    {
        IEnumerable<IVertexObject> GetVertexObjects(IEnumerable<LineSegment> _lineSegments);
    }
}