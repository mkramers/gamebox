using System;
using RenderCore.ViewProvider;
using SFML.Graphics;

namespace RenderCore.Render
{
    public class Scene2 : Scene
    {
        private IViewProvider m_viewProvider;
        private RenderTexture m_sceneRenderTexture;

        public Scene2(uint _width, uint _height, IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;
            m_sceneRenderTexture = new RenderTexture(_width, _height);
        }
        
        public Texture UpdateTexture()
        {
            if (m_sceneRenderTexture == null)
            {
                return null;
            }

            m_sceneRenderTexture.Clear();

            m_sceneRenderTexture.SetView(m_viewProvider.GetView());

            base.Draw(m_sceneRenderTexture, RenderStates.Default);

            m_sceneRenderTexture.Display();

            return m_sceneRenderTexture.Texture;
        }

        public void Dispose()
        {
            m_sceneRenderTexture?.Dispose();
        }

        public void SetViewProvider(IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;
        }

        public void SetSize(uint _width, uint _height)
        {
            m_sceneRenderTexture?.Dispose();
            m_sceneRenderTexture = new RenderTexture(_width, _height);
        }
    }

    public class SceneTexture : IDisposable
    {
        private RenderTexture m_sceneRenderTexture;

        public void Dispose()
        {
            m_sceneRenderTexture?.Dispose();
        }

        public void SetSize(uint _width, uint _height)
        {
            m_sceneRenderTexture?.Dispose();

            m_sceneRenderTexture = new RenderTexture(_width, _height);
        }

        public Texture RenderToTexture(Scene _scene)
        {
            m_sceneRenderTexture.Clear();

            _scene.Draw(m_sceneRenderTexture, RenderStates.Default);

            m_sceneRenderTexture.Display();

            return m_sceneRenderTexture.Texture;
        }
    }
}