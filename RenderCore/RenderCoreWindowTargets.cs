using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class RenderCoreTexture : IDrawable
    {
        private RenderTexture m_renderTexture;

        public RenderCoreTexture(Vector2u _size)
        {
            SetSize(_size);
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            _target.Draw(m_renderTexture, _states);
        }

        public void Dispose()
        {
            m_renderTexture.Dispose();
        }
        
        public void SetSize(Vector2u _size)
        {
            m_renderTexture = new RenderTexture(_size.X, _size.Y);
        }

        public void SetView(View _view)
        {
            m_renderTexture.SetView(_view);
        }

        public RenderTarget GetRenderTarget()
        {
            return m_renderTexture;
        }

        public void Clear(Color _color)
        {
            m_renderTexture.Clear(_color);
        }

        public void Display()
        {
            m_renderTexture.Display();
        }
    }
}