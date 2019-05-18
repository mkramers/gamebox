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
    
    public class Drawable<T> : IPositionDrawable, IDisposable where T : Transformable, Drawable
    {
        public readonly T m_renderObject;

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

    public interface IWidget : IPositionDrawable, ITickable
    {

    }

    public interface IDrawable : Drawable, IDisposable
    {
    }

    public interface IPositionDrawable : IPosition, IDrawable
    {
    }
}