using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class ShapeDrawable : IDrawable
    {
        private readonly Shape m_shape;

        public ShapeDrawable(Shape _shape)
        {
            m_shape = _shape;
        }

        public void SetRenderPosition(Vector2 _position)
        {
            m_shape.Position = _position.GetVector2F();
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            _target.Draw(m_shape, _states);
        }

        public void Dispose()
        {
            m_shape.Dispose();
        }

        public void SetColor(Color _color)
        {
            m_shape.FillColor = _color;
        }
    }
}