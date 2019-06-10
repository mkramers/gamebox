using System.Collections.Generic;
using System.Numerics;

namespace RenderCore
{
    public class Polygon : List<Vector2>, IVertexObject
    {
        public Polygon(int _capacity) : base(_capacity)
        {
        }
    }
}