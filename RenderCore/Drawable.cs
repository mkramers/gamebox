using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class Drawable<T> : IPositionDrawable where T : Transformable, Drawable
    {
        protected readonly T m_renderObject;

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
            return m_renderObject.Position.GetVector2();
        }

        public void SetPosition(Vector2 _position)
        {
            m_renderObject.Position = _position.GetVector2F();
        }
    }
}