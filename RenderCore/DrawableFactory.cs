using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public static class DrawableFactory
    {
        private static IEnumerable<LineSegment> GetBoxLineSegments(Vector2 _boxSize)
        {
            yield return new LineSegment(new Vector2(0, 0), new Vector2(_boxSize.X, 0));
            yield return new LineSegment(new Vector2(0, 0), new Vector2(0, _boxSize.Y));
            yield return new LineSegment(new Vector2(_boxSize.X, 0), new Vector2(_boxSize.X, _boxSize.Y));
            yield return new LineSegment(new Vector2(0, _boxSize.Y), new Vector2(_boxSize.X, _boxSize.Y));
        }

        public static MultiDrawable<RectangleShape> GetBox(Vector2 _size, float _thickness)
        {
            IEnumerable<RectangleShape> shapes = GetBoxLineSegments(_size)
                .Select(_segment => ShapeFactory.GetLineShape(_segment, _thickness));
            return new MultiDrawable<RectangleShape>(shapes.ToList());
        }
    }
}