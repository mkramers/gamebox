using System;
using RenderCore.Drawable;
using RenderCore.ViewProvider;
using SFML.Graphics;
using SFML.System;

namespace RenderCore.Render
{
    public class RenderCoreTarget : IRenderCoreTarget
    {
        private readonly Color m_clearColor;
        private readonly RenderObjectContainer m_renderObjectContainer;
        private readonly RenderTexture m_renderTexture;
        private IViewProvider m_viewProvider;

        public RenderCoreTarget(Vector2u _textureSize, Color _clearColor)
        {
            m_renderTexture = new RenderTexture(_textureSize.X, _textureSize.Y);

            m_clearColor = _clearColor;

            SetViewProvider(new ViewProviderBase());

            m_renderObjectContainer = new RenderObjectContainer();
        }

        public void SetViewProvider(IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;
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