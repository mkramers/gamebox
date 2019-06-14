using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Common;
using Common.VertexObject;

namespace LibExtensions
{
    public static class AetherMathsExtensions
    {
        public static Vector2 GetVector2(this Aether.Physics2D.Common.Maths.Vector2 _vector)
        {
            return new Vector2(_vector.X, _vector.Y);
        }

        public static Aether.Physics2D.Common.Maths.Vector2 GetVector2(this Vector2 _vector)
        {
            return new Aether.Physics2D.Common.Maths.Vector2(_vector.X, _vector.Y);
        }

        public static Vertices GetVertices(this IVertexObject _polygon)
        {
            IEnumerable<Aether.Physics2D.Common.Maths.Vector2> vectors = _polygon.Select(_vertex => _vertex.GetVector2());

            Vertices vertices = new Vertices(vectors);

            return vertices;
        }
    }
}