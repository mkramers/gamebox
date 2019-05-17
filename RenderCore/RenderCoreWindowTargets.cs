using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class RenderCoreWindowTargets : IDrawable
    {
        private RenderTexture m_overlayRenderTarget;
        private RenderTexture m_sceneRenderTarget;

        public RenderCoreWindowTargets(Vector2u _size)
        {
            SetSize(_size);
        }

        public void Dispose()
        {
            m_sceneRenderTarget.Dispose();
            m_overlayRenderTarget.Dispose();
        }

        public void SetRenderPosition(Vector2 _positionScreen)
        {
            throw new NotImplementedException();
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            _target.Draw(m_sceneRenderTarget, _states);
            _target.Draw(m_overlayRenderTarget, _states);
        }

        public void SetSize(Vector2u _size)
        {
            m_overlayRenderTarget = new RenderTexture(_size.X, _size.Y);
            m_sceneRenderTarget = new RenderTexture(_size.X, _size.Y);
        }

        public void SetSceneView(View _view)
        {
            m_sceneRenderTarget.SetView(_view);
        }

        public void DrawToScene(IDrawable _drawable)
        {
            m_sceneRenderTarget.Draw(_drawable);
        }

        public void Clear(Color _color)
        {
            m_sceneRenderTarget.Clear(_color);
            m_overlayRenderTarget.Clear(Color.Transparent);
        }

        public void Display()
        {
            m_sceneRenderTarget.Display();
            m_overlayRenderTarget.Display();
        }
    }
}