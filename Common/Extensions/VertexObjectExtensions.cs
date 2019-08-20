using System.Linq;
using System.Numerics;
using Common.Geometry;

namespace Common.Extensions
{
    public static class VertexObjectExtensions
    {
        public static IVertexObject Translate(this IVertexObject _polygon, Vector2 _translation)
        {
            Vector2[] translatedVertices = _polygon.Select(_vertex => _vertex + _translation).ToArray();

            Polygon polygon = new Polygon();
            polygon.AddRange(translatedVertices);
            return polygon;
        }
    }
}