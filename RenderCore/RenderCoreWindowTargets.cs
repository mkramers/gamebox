using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public interface IRenderCoreTarget : IDrawable
    {
        void SetSize(Vector2u _size);
        void SetView(View _view);
        RenderTarget GetRenderTarget();
        void Clear(Color _color);
        void Display();
    }

    public class RenderCoreTarget : IRenderCoreTarget
    {
        private RenderTexture m_renderTexture;

        public RenderCoreTarget(Vector2u _size)
        {
            SetSize(_size);
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            Texture texture = m_renderTexture.Texture;
            _target.Draw(texture, _states);
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