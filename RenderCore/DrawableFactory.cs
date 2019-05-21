using SFML.Graphics;

namespace RenderCore
{
    public static class DrawableFactory
    {
        public static ShapeDrawable GetLineSegment(LineSegment _line, float _thickness)
        {
            RectangleShape shape = ShapeFactory.GetLineShape(_line, _thickness);
            ShapeDrawable drawable = new ShapeDrawable(shape);
            return drawable;
        }
    }
}