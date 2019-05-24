using System.Collections.Generic;

namespace RenderCore
{
    public class PolygonList : List<Polygon>
    {
        public PolygonList()
        {
        }

        public PolygonList(IEnumerable<Polygon> _collection) : base(_collection)
        {
        }

        public PolygonList(int _capacity) : base(_capacity)
        {
        }
    }
}