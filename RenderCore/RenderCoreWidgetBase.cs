using System;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public abstract class RenderCoreWidgetBase : IRenderCoreWidget
    {
        protected Vector2 m_position;

        public abstract void Draw(RenderTarget _target, RenderStates _states);
        public abstract void Dispose();

        public Vector2 GetPosition()
        {
            return m_position;
        }

        public void SetPosition(Vector2 _positionScreen)
        {
            m_position = _positionScreen;
        }

        public abstract void Tick(TimeSpan _elapsed);
    }
}