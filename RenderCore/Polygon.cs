using System.Collections.Generic;
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

        public void Translate(Vector2 _translation)
        {
            for (int i = 0; i < Count; i++)
            {
                this[i] += _translation;
            }
        }
    }
}