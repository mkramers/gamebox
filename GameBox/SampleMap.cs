using System.Collections.Generic;
using System.Numerics;
using RenderCore;

namespace GameBox
{
    public class SampleMap : IMap
    {
        public IEnumerable<IEntity> GetEntities(IPhysics _physics, Vector2 _position)
        {
            Polygon floor = ShapeFactory.CreateRectangle(_position, new Vector2(10, 0.5f));

            Entity entity = EntityFactory.CreatePolygonEntity(_physics, _position, floor);

            return new[] { entity };
        }
    }
}