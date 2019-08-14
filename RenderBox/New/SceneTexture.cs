using System;
using SFML.Graphics;

namespace RenderBox.New
{
    public class SceneTexture : IDisposable
    {
        private RenderTexture m_sceneRenderTexture;

        public void SetSize(uint _width, uint _height, View _view)
        {
            m_sceneRenderTexture?.Dispose();

            m_sceneRenderTexture = new RenderTexture(_width, _height);
        }

        public Texture RenderToTexture(Scene _scene, View _view)
        {
            m_sceneRenderTexture.Clear();

            m_sceneRenderTexture.SetView(_view);

            _scene.Draw(m_sceneRenderTexture, RenderStates.Default);

            m_sceneRenderTexture.Display();

            return m_sceneRenderTexture.Texture;
        }

        public void Dispose()
        {
            m_sceneRenderTexture?.Dispose();
        }
    }
}