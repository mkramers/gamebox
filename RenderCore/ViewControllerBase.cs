using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class StaticViewControllerBase : ViewControllerBase
    {
        public StaticViewControllerBase(View _view, float _windowRatio) : base(_view, _windowRatio)
        {
        }

        public override void Tick(TimeSpan _elapsed)
        {
        }
    }

    public abstract class ViewControllerBase : IViewController
    {
        private readonly View m_view;
        private readonly float m_windowRatio;

        protected ViewControllerBase(View _view, float _windowRatio)
        {
            m_view = _view;
            m_windowRatio = _windowRatio;
        }

        public View GetView()
        {
            return m_view;
        }

        public void SetParentSize(Vector2u _parentSize)
        {
            float width = _parentSize.X * m_windowRatio;
            float height = _parentSize.Y * m_windowRatio;

            Vector2f size = new Vector2f(width, height);
            m_view.Size = size;
            m_view.Center = new Vector2f(width / 2, height / 2);
            m_view.Viewport = new FloatRect(0.0f, 0, 0.8f, 1);
        }

        public abstract void Tick(TimeSpan _elapsed);

        protected void SetCenter(Vector2 _center)
        {
            m_view.Center = _center.GetVector2F();
        }
    }
}