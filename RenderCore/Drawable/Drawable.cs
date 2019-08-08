using System.Numerics;
using LibExtensions;
using SFML.Graphics;

namespace RenderCore.Drawable
{
    public class Drawable<T> : IPositionDrawable where T : Transformable, SFML.Graphics.Drawable
    {
        private readonly T m_renderObject;

        public Drawable(T _renderObject)
        {
            m_renderObject = _renderObject;
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            _target.Draw(m_renderObject, _states);
        }

        public void Dispose()
        {
            m_renderObject.Dispose();
        }

        public Vector2 GetPosition()
        {
            Vector2 position = m_renderObject.Position.GetVector2();
            return position;
        }

        public void SetPosition(Vector2 _position)
        {
            m_renderObject.Position = _position.GetVector2F();
        }
    }
}