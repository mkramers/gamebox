using System.Collections.Generic;
using System.Numerics;
using SFML.System;

namespace RenderCore
{
    public static class LandscapeFactory
    {
        public static IEnumerable<Vector2> GetPyramid(int _size)
        {
            List<Vector2> positions = new List<Vector2>();

            for (int i = 0; i < _size; i++)
            {
                int height = i + 1;
                for (int j = 0; j < height; j++)
                {
                    Vector2 position = new Vector2(i, -j);
                    positions.Add(position);
                }
            }

            return positions;
        }

        public static IEnumerable<Vector2> GetBox(Vector2u _size, float _stepSize)
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

                    Vector2 position = _stepSize * new Vector2(i, -j);
                    positions.Add(position);
                }
            }

            return positions;
        }
    }
}