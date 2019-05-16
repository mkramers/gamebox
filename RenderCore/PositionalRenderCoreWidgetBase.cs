using System;
using System.Numerics;
using SFML.System;

namespace RenderCore
{
    public abstract class PositionalRenderCoreWidgetBase : RenderCoreViewWidgetBase
    {
        private Vector2 m_normalizedPosition;
        protected Vector2f m_screenPosition;

        public override void SetRenderPosition(Vector2 _positionScreen)
        {
            m_normalizedPosition = _positionScreen;
        }

        public override void Tick(TimeSpan _elapsed)
        {
            if (m_view == null)
            {
                return;
            }

            Vector2f size = m_view.Size;
            Vector2f position = m_view.Center - size / 2;

            m_screenPosition =
                new Vector2f(size.X * m_normalizedPosition.X, size.Y * m_normalizedPosition.Y) + position;
        }
    }
}