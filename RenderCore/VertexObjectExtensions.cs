using System.Collections.Generic;
using System.Linq;
using Aether.Physics2D.Common;
using Aether.Physics2D.Common.Maths;
using Common.VertexObject;

namespace RenderCore
{
    public static class VertexObjectExtensions
    {
        public static Vertices GetVertices(this IVertexObject _polygon)
        {
            IEnumerable<Vector2> vectors = _polygon.Select(_vertex => _vertex.GetVector2());

            Vertices vertices = new Vertices(vectors);

            return vertices;
        }
    }
}