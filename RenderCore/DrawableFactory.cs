using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public static class DrawableFactory
    {
        public static MultiDrawable<RectangleShape> GetCrossHair(Vector2 _size, float _thickness)
        {
            IEnumerable<LineSegment> lines = GetCrossHairLineSegments(_size);

            RectangleShape[] shapes = lines.Select(_line => ShapeFactory.GetLineShape(_line, _thickness)).ToArray();
            Debug.Assert(shapes.Length == 2);

            shapes[0].FillColor = Color.Red;
            shapes[1].FillColor = Color.Green;

            return new MultiDrawable<RectangleShape>(shapes);
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

        public static MultiDrawable<RectangleShape> GetBox(Vector2 _size, float _thickness)
        {
            IEnumerable<RectangleShape> shapes = GetBoxLineSegments(_size)
                .Select(_segment => ShapeFactory.GetLineShape(_segment, _thickness));
            return new MultiDrawable<RectangleShape>(shapes.ToList());
        }
    }
}