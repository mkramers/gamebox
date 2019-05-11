using System;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public abstract class RenderCoreViewWidgetBase : IRenderCoreWindowWidget
    {
        protected View m_view;

        public abstract void Draw(RenderTarget _target, RenderStates _states);
        public abstract void Dispose();

        public abstract void SetRenderPosition(Vector2 _positionScreen);

        public void SetView(View _view)
        {
            m_view = _view;
        }

        public virtual void Tick(TimeSpan _elapsed)
        {
        }
    }
}