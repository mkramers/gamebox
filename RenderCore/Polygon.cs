using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RenderCore
{
    public class Polygon : List<Vector2>, IVertexObject
    {
        public Polygon()
        {
        }

        public Polygon(IEnumerable<Vector2> _collection) : base(_collection)
        {
        }

        public Polygon(int _capacity) : base(_capacity)
        {
        }

        public static IVertexObject Translate(IVertexObject _polygon, Vector2 _translation)
        {
            IEnumerable<Vector2> translatedVertices = _polygon.Select(_vertex => _vertex + _translation);

            Polygon translatedPolygon = new Polygon(translatedVertices);
            return translatedPolygon;
        }
    }
}