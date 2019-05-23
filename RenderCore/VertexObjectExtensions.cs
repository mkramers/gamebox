using System.Linq;
using System.Numerics;
using Aether.Physics2D.Common;

namespace RenderCore
{
    public static class VertexObjectExtensions
    {
        public static Vertices GetVertices(this IVertexObject _polygon)
        {
            Vertices vertices = new Vertices(_polygon.Count());

            foreach (Vector2 vertex in _polygon)
            {
                vertices.Add(vertex.GetVector2());
            }

            return vertices;
        }
    }
}