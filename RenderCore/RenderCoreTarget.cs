using System;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class RenderCoreTarget : IRenderCoreTarget
    {
        private readonly Color m_clearColor;
        private readonly RenderObjectContainer m_renderObjectContainer;
        private readonly RenderTexture m_renderTexture;
        private IViewProvider m_viewProvider;

        public RenderCoreTarget(Vector2u _size, Color _clearColor)
        {
            m_clearColor = _clearColor;

            m_viewProvider = new ViewProviderBase();

            m_renderTexture = new RenderTexture(_size.X, _size.Y);

            m_renderObjectContainer = new RenderObjectContainer();
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            m_renderTexture.Clear(m_clearColor);

            m_renderObjectContainer.Draw(m_renderTexture, _states);

            m_renderTexture.Display();

            Texture texture = m_renderTexture.Texture;
            _target.Draw(texture, _states);
        }

        public void Dispose()
        {
            m_renderTexture.Dispose();
            m_renderObjectContainer.Dispose();
        }

        public void SetViewProvider(IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;

            //Vector2u size = m_renderTexture.Size;
            //m_viewProvider.SetSize(new Vector2f(size.X * RATIO, size.Y * RATIO));
        }

        public void AddDrawable(IDrawable _drawable)
        {
            m_renderObjectContainer.AddDrawable(_drawable);
        }

        public void Tick(TimeSpan _elapsed)
        {
            View view = m_viewProvider.GetView();
            m_renderTexture.SetView(view);
        }
    }
}