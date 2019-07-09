using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Common.Geometry;
using SFML.Graphics;

namespace RenderCore.Drawable
{
    public static class DrawableFactory
    {
        public static MultiDrawable<LineShape> GetCrossHair(Vector2 _size, float _thickness)
        {
            LineSegment[] lines = GetCrossHairLineSegments(_size).ToArray();

            List<LineShape> shapes = new List<LineShape>
            {
                LineShape.Factory.CreateLineShape(lines[0], Color.Red),
                LineShape.Factory.CreateLineShape(lines[1], Color.Green)
            };

            return new MultiDrawable<LineShape>(shapes);
        }

        private static IEnumerable<LineSegment> GetCrossHairLineSegments(Vector2 _size)
        {
            yield return new LineSegment(new Vector2(-_size.X / 2, 0), new Vector2(_size.X / 2, 0));
            yield return new LineSegment(new Vector2(0, -_size.Y / 2), new Vector2(0, _size.Y / 2));
        }

        private static IEnumerable<LineSegment> GetBoxLineSegments(Vector2 _boxSize)
        {
            yield return new LineSegment(new Vector2(0, 0), new Vector2(_boxSize.X, 0));
            yield return new LineSegment(new Vector2(0, 0), new Vector2(0, _boxSize.Y));
            yield return new LineSegment(new Vector2(_boxSize.X, 0), new Vector2(_boxSize.X, _boxSize.Y));
            yield return new LineSegment(new Vector2(0, _boxSize.Y), new Vector2(_boxSize.X, _boxSize.Y));
        }

        public static MultiDrawable<LineShape> GetBox(Vector2 _size, Color _color)
        {
            IEnumerable<LineShape> shapes = GetBoxLineSegments(_size)
                .Select(_segment => LineShape.Factory.CreateLineShape(_segment, _color));
            return new MultiDrawable<LineShape>(shapes.ToList());
        }
    }
}