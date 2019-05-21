using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public static class DrawableFactory
    {
        private static ShapeDrawable GetLineSegment(LineSegment _line, float _thickness)
        {
            RectangleShape shape = ShapeFactory.GetLineShape(_line, _thickness);
            ShapeDrawable drawable = new ShapeDrawable(shape);
            return drawable;
        }

        public static void DrawBox(Vector2 _sceneSize, IRenderObjectContainer _scene)
        {
            {
                LineSegment lineSegment = new LineSegment(new Vector2(0, 0), new Vector2(_sceneSize.X, 0));
                DrawLine(_scene, lineSegment);
            }
            {
                LineSegment lineSegment = new LineSegment(new Vector2(0, 0), new Vector2(0, _sceneSize.Y));
                DrawLine(_scene, lineSegment);
            }
            {
                LineSegment lineSegment =
                    new LineSegment(new Vector2(_sceneSize.X, 0), new Vector2(_sceneSize.X, _sceneSize.Y));
                DrawLine(_scene, lineSegment);
            }
            {
                LineSegment lineSegment =
                    new LineSegment(new Vector2(0, _sceneSize.Y), new Vector2(_sceneSize.X, _sceneSize.Y));
                DrawLine(_scene, lineSegment);
            }
        }

        private static void DrawLine(IRenderObjectContainer _scene, LineSegment _lineSegment)
        {
            ShapeDrawable lineSegmentDrawable = GetLineSegment(_lineSegment, 1);
            lineSegmentDrawable.SetFillColor(Color.Red);
            _scene.AddDrawable(lineSegmentDrawable);
        }
    }
}