using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using RenderCore;
using SFML.System;

namespace GameBox
{
    public class SampleMap : IMap
    {
        public IEnumerable<IEntity> GetEntities(IPhysics _physics)
        {
            IEnumerable<IEntity> woodEntities = CreateLandscape(_physics);
            return woodEntities;
        }

        private static IEnumerable<IEntity> CreateLandscape(IPhysics _physics)
        {
            const int range = 20;

            Vector2 position = new Vector2(-10, 5);
            List<Vector2> positions = LandscapeFactory.GetPyramid(position, range).ToList();

            uint boxSize = (uint)Math.Round(1.5f * range);
            IEnumerable<Vector2> box = LandscapeFactory.GetBox(position, new Vector2u(boxSize, boxSize), 1);

            positions.AddRange(box);

            return positions.Select(_pyramidPosition =>
                EntityFactory.CreateEntity(1, _pyramidPosition, _physics, ResourceId.WOOD, BodyType.Static)).ToList();
        }

    }
}