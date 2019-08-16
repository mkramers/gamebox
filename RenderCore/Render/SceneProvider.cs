using System.Collections.Generic;
using System.Linq;
using RenderCore.Drawable;
using RenderCore.ViewProvider;
using SFML.Graphics;

namespace RenderCore.Render
{
    public class SceneProvider : ISceneProvider
    {
        private IViewProvider m_viewProvider;
        private readonly IRenderTextureWrapper m_sceneRenderTexture;
        private readonly List<IDrawableProvider> m_drawableProviders;

        public SceneProvider(IRenderTextureWrapper _renderTextureWrapper, IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;
            m_sceneRenderTexture = _renderTextureWrapper;
            m_drawableProviders = new List<IDrawableProvider>();
        }

        public void AddDrawableProvider(IDrawableProvider _provider)
        {
            m_drawableProviders.Add(_provider);
        }

        public Texture GetTexture()
        {
            if (m_sceneRenderTexture == null)
            {
                return null;
            }

            m_sceneRenderTexture.Clear(Color.Blue);

            m_sceneRenderTexture.SetView(m_viewProvider.GetView());

            Draw(m_sceneRenderTexture.GetRenderTarget(), RenderStates.Default);

            m_sceneRenderTexture.Display();

            return m_sceneRenderTexture.GetTexture();
        }

        public void Dispose()
        {
            m_sceneRenderTexture?.Dispose();
        }

        private void Draw(RenderTarget _target, RenderStates _states)
        {
            IEnumerable<IDrawable> drawables = m_drawableProviders.SelectMany(_provider => _provider.GetDrawables());
            foreach (IDrawable drawable in drawables)
            {
                _target.Draw(drawable, _states);
            }
        }

        public void SetViewProvider(IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;
        }

        public void SetSize(uint _width, uint _height)
        {
            m_sceneRenderTexture.SetSize(_width, _height);
        }
    }
}