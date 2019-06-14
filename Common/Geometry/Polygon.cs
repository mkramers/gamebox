using System.Collections.Generic;
using System.Numerics;
using Common.VertexObject;

namespace Common.Geometry
{
    public class Polygon : List<Vector2>, IVertexObject
    {
        public Polygon(int _capacity) : base(_capacity)
        {
        }
    }
}