using System;
using RenderCore.Drawable;
using RenderCore.ViewProvider;
using SFML.Graphics;

namespace RenderCore.Widget
{
    public abstract class ViewWidgetBase : IWidget, IDrawable
    {
        protected readonly IViewProvider m_viewProvider;

        protected ViewWidgetBase(IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;
        }

        public abstract void Draw(RenderTarget _target, RenderStates _states);
        public abstract void Dispose();

        public abstract void Tick(TimeSpan _elapsed);
    }
}