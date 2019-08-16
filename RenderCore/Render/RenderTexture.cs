using SFML.Graphics;

namespace RenderCore.Render
{
    public class RenderTexture : IRenderTexture
    {
        private SFML.Graphics.RenderTexture m_renderTexture;

        public RenderTexture(uint _width, uint _height)
        {
            SetSize(_width, _height);
        }

        public void Clear(Color _color)
        {
            m_renderTexture?.Clear(_color);
        }

        public void SetView(View _view)
        {
            m_renderTexture?.SetView(_view);
        }

        public void Display()
        {
            m_renderTexture?.Display();
        }

        public RenderTarget GetRenderTarget()
        {
            return m_renderTexture;
        }

        public void Dispose()
        {
            m_renderTexture?.Dispose();
        }

        public Texture GetTexture()
        {
            return m_renderTexture?.Texture;
        }

        public void SetSize(uint _width, uint _height)
        {
            m_renderTexture?.Dispose();
            m_renderTexture = new SFML.Graphics.RenderTexture(_width, _height);
        }
    }
}