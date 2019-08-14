using System;
using Common.Tickable;
using RenderCore.Drawable;
using RenderCore.ViewProvider;
using SFML.Graphics;
using SFML.System;
using TGUI;

namespace RenderCore.Render
{
    public class SubmitToDrawRenderWindow : ITickable
    {
        private readonly float m_aspectRatio;
        private readonly Gui m_gui;
        private readonly RenderWindow m_renderWindow;
        private readonly Scene m_scene;
        private readonly SceneTexture m_sceneTexture;
        private IViewProvider m_viewProvider;

        public SubmitToDrawRenderWindow(float _aspectRatio, Vector2u _windowSize)
        {
            m_viewProvider = new ViewProviderBase();

            m_aspectRatio = _aspectRatio;
            m_renderWindow = RenderWindowFactory.CreateRenderWindow("", _windowSize);
            m_renderWindow.Resized += (_sender, _e) => Resize(new Vector2u(_e.Width, _e.Height));

            m_scene = new Scene();

            m_gui = new Gui(m_renderWindow);

            m_sceneTexture = new SceneTexture();

            Resize(_windowSize);
        }

        public void Tick(TimeSpan _elapsed)
        {
            Draw();
        }

        public void AddDrawableProvider(IDrawableProvider _provider)
        {
            m_scene.AddDrawableProvider(_provider);
        }

        public Gui GetGui()
        {
            return m_gui;
        }

        private void Resize(Vector2u _windowSize)
        {
            float aspectRatio = m_aspectRatio;

            View renderWindowView = m_renderWindow.GetView();

            FloatRect viewPort = WindowResizeUtilities.GetViewPort(_windowSize, aspectRatio);
            renderWindowView.Viewport = viewPort;

            m_renderWindow.SetView(renderWindowView);

            uint adjustedWidth = (uint) Math.Round(viewPort.Width * _windowSize.X);
            uint adjustedHeight = (uint) Math.Round(viewPort.Height * _windowSize.Y);

            m_sceneTexture.SetSize(adjustedWidth, adjustedHeight, renderWindowView);

            m_gui.View = renderWindowView;
        }

        private void Draw()
        {
            m_renderWindow.DispatchEvents();
            
            m_renderWindow.Clear();

            View view = m_viewProvider.GetView();
            if (view != null)
            {
                Texture sceneTexture = m_sceneTexture.RenderToTexture(m_scene, view);

                m_renderWindow.Draw(sceneTexture, RenderStates.Default);
            }

            m_gui.Draw();

            m_renderWindow.Display();
        }

        public void SetViewProvider(IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;
        }

        public event EventHandler Closed
        {
            add => m_renderWindow.Closed += value;
            remove => m_renderWindow.Closed -= value;
        }

        public Vector2u GetWindowSize()
        {
            return m_renderWindow.Size;
        }
    }
}