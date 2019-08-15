using System;
using RenderCore.Drawable;
using RenderCore.ViewProvider;
using SFML.Graphics;

namespace RenderCore.Render
{
    public class Scene2 : Scene, IDrawable
    {
        private readonly IViewProvider m_viewProvider;
        private readonly RenderTexture m_sceneRenderTexture;

        public Scene2(uint _width, uint _height, IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;
            m_sceneRenderTexture = new RenderTexture(_width, _height);
        }

        public override void Draw(RenderTarget _target, RenderStates _states)
        {
            m_sceneRenderTexture.Clear();

            m_sceneRenderTexture.SetView(m_viewProvider.GetView());

            base.Draw(m_sceneRenderTexture, _states);

            m_sceneRenderTexture.Display();

            _target.Draw(m_sceneRenderTexture.Texture, _states);
        }

        public void Dispose()
        {
            m_sceneRenderTexture.Dispose();
        }
    }

    public class SceneTexture : IDisposable
    {
        private RenderTexture m_sceneRenderTexture;

        public void Dispose()
        {
            m_sceneRenderTexture?.Dispose();
        }

        public void SetSize(uint _width, uint _height, View _view)
        {
            m_sceneRenderTexture?.Dispose();

            m_sceneRenderTexture = new RenderTexture(_width, _height);
        }

        public Texture RenderToTexture(Scene _scene, View _view)
        {
            if (_view == null)
            {
                throw new ArgumentNullException(nameof(_view));
            }

            m_sceneRenderTexture.Clear();

            m_sceneRenderTexture.SetView(_view);

            _scene.Draw(m_sceneRenderTexture, RenderStates.Default);

            m_sceneRenderTexture.Display();

            return m_sceneRenderTexture.Texture;
        }
    }
}