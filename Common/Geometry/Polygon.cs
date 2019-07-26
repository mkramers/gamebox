using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Common.VertexObject;

namespace Common.Geometry
{
    public class Polygon : List<Vector2>, IVertexObject
    {
        public Polygon(int _capacity) : base(_capacity)
        {
        }

        public Polygon()
        {
        }
    }

    public static class VertexObjectExtensions
    {
        public static IVertexObject Translate(this IVertexObject _polygon, Vector2 _translation)
        {
            Vector2[] translatedVertices = _polygon.Select(_vertex => _vertex + _translation).ToArray();

            Polygon polygon = new Polygon(translatedVertices.Length);
            polygon.AddRange(translatedVertices);
            return polygon;
        }
    }
}