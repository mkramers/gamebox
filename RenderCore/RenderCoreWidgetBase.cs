using System;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public abstract class RenderWidget<T> : RenderCoreWidgetBase where T : Transformable, Drawable
    {
        private readonly T m_renderObject;

        protected RenderWidget(T _renderObject)
        {
            m_renderObject = _renderObject;
        }

        public override void Draw(RenderTarget _target, RenderStates _states)
        {
            m_renderObject.Draw(_target, _states);
        }

        public override void Dispose()
        {
            m_renderObject.Dispose();
        }

        public T GetRenderObject()
        {
            return m_renderObject;
        }
    }

    public abstract class RenderCoreWidgetBase : IRenderCoreWidget
    {
        protected Vector2 m_position;

        public abstract void Draw(RenderTarget _target, RenderStates _states);
        public abstract void Dispose();

        public void SetRenderPosition(Vector2 _positionScreen)
        {
            m_position = _positionScreen;
        }

        public abstract void Tick(TimeSpan _elapsed);
    }
}