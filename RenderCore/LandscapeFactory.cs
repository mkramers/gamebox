using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using SFML.System;

namespace RenderCore
{
    public class LandscapeFactory
    {
        public static IEnumerable<IEntity> CreateLandscape(IPhysics _physics)
        {
            const int range = 20;
            
            Vector2 position = new Vector2(-10, 5);
            List<Vector2> positions = GetPyramid(position, range).ToList();

            uint boxSize = (uint) Math.Round(1.5f * range);
            IEnumerable<Vector2> box = GetBox(position, new Vector2u(boxSize, boxSize), 1);

            positions.AddRange(box);

            return positions.Select(_pyramidPosition =>
                EntityFactory.CreateEntity(1, _pyramidPosition, _physics, ResourceId.WOOD, BodyType.Static)).ToList();
        }

        public static IEnumerable<Vector2> GetPyramid(Vector2 _position, int _size)
        {
            List<Vector2> positions = new List<Vector2>();

            for (int i = 0; i < _size; i++)
            {
                int height = i + 1;
                for (int j = 0; j < height; j++)
                {
                    Vector2 position = _position + new Vector2(i, -j);
                    positions.Add(position);
                }
            }

            return positions;
        }

        public static IEnumerable<Vector2> GetBox(Vector2 _position, Vector2u _size, float _stepSize)
        {
            List<Vector2> positions = new List<Vector2>();

            for (uint i = 0; i < _size.X; i++)
            {
                for (uint j = 0; j < _size.Y; j++)
                {
                    bool isOutsideRow = i == 0 || i == _size.X - 1;
                    bool isOutsideColumn = j == 0 || j == _size.Y - 1;
                    if (!isOutsideRow && !isOutsideColumn)
                    {
                        continue;
                    }

                    Vector2 position = _position + _stepSize * new Vector2(i, -j);
                    positions.Add(position);
                }
            }

            return positions;
        }
    }
}