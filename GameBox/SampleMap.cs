using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using RenderCore;
using SFML.Graphics;
using SFML.System;

namespace GameBox
{
    public class SampleMap : IMap
    {
        public IEnumerable<IEntity> GetEntities(IPhysics _physics, Vector2 _position)
        {
            IEnumerable<IEntity> woodEntities = CreateLandscape2(_physics, _position);
            return woodEntities;
        }

        private static IEnumerable<IEntity> CreateLandscape(IPhysics _physics)
        {
            const int range = 20;

            Vector2 origin = new Vector2(0, 5);
            List<Vector2> positions =
                LandscapeFactory.GetPyramid(range).Select(_position => _position + origin).ToList();

            uint boxSize = (uint)Math.Round(1.5f * range);
            IEnumerable<Vector2> box = LandscapeFactory.GetBox(new Vector2u(3 * boxSize, 2 * boxSize), 1)
                .Select(_position => _position + origin);

            positions.AddRange(box);

            return positions.Select(_pyramidPosition =>
                EntityFactory.CreateEntity(1, _pyramidPosition, _physics, ResourceId.WOOD, BodyType.Static)).ToList();
        }
        private static IEnumerable<IEntity> CreateLandscape2(IPhysics _physics, Vector2 _position)
        {
            List<IEntity> entities = new List<IEntity>();

            Polygon floor = ShapeFactory.CreateRectangle(_position, new Vector2(100, 1));

            Entity entity = EntityFactory.CreatePolygonEntity(_physics, _position, floor);

            return new[] { entity };
        }
    }
}