using System;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public abstract class TickableDrawable<T> : Drawable<T>, ITickable where T : Transformable, Drawable
    {
        protected TickableDrawable(T _renderObject) : base(_renderObject)
        {
        }

        public abstract void Tick(TimeSpan _elapsed);
    }
    
    public class Drawable<T> : IDrawable where T : Transformable, Drawable
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

        public void SetRenderPosition(Vector2 _positionScreen)
        {
            m_renderObject.Position = _positionScreen.GetVector2F();
        }
    }

    public interface ITickableDrawable : IDrawable, ITickable
    {

    }

    public interface IDrawable : Drawable, IDisposable
    {
        void SetRenderPosition(Vector2 _positionScreen);
    }
}