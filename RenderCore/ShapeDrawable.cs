using SFML.Graphics;

namespace RenderCore
{
    public class ShapeDrawable : Drawable<Shape>
    {
        public ShapeDrawable(Shape _renderObject) : base(_renderObject)
        {
        }

        public void SetFillColor(Color _color)
        {
            m_renderObject.FillColor = _color;
        }
    }
}