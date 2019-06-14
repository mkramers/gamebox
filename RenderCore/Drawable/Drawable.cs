using System.Numerics;
using LibExtensions;
using SFML.Graphics;

namespace RenderCore.Drawable
{
    public class Drawable<T> : IPositionDrawable where T : Transformable, SFML.Graphics.Drawable
    {
        private readonly Vector2 m_origin;
        protected readonly T m_renderObject;

        public Drawable(T _renderObject) : this(_renderObject, Vector2.Zero)
        {
        }

        public Drawable(T _renderObject, Vector2 _origin)
        {
            m_origin = _origin;
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
            Vector2 position = m_renderObject.Position.GetVector2() - m_origin;
            return position;
        }

        public void SetPosition(Vector2 _position)
        {
            Vector2 position = _position + m_origin;
            m_renderObject.Position = position.GetVector2F();
        }
    }
}