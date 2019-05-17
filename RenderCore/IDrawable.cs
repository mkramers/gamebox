using System;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public abstract class TickableDrawable<T> : ITickableDrawable where T : Transformable, Drawable
    {
        private readonly T m_renderObject;

        protected TickableDrawable(T _renderObject)
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

        public void SetRenderPosition(Vector2 _positionScreen)
        {
            m_renderObject.Position = _positionScreen.GetVector2F();
        }

        public abstract void Tick(TimeSpan _elapsed);
    }

    public interface ITickableDrawable : IDrawable, ITickable
    {

    }

    public interface IDrawable : Drawable, IDisposable
    {
        void SetRenderPosition(Vector2 _positionScreen);
    }
}