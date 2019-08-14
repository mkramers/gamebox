using System;
using Common.Tickable;
using RenderCore.Drawable;
using RenderCore.Render;
using RenderCore.ViewProvider;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using TGUI;

namespace RenderBox.New
{
    public class SubmitToDrawRenderWindow : ITickable
    {
        private readonly float m_aspectRatio;
        private readonly RenderWindow m_renderWindow;
        private readonly Scene m_scene;
        private RenderTexture m_sceneRenderTexture;
        private readonly Gui m_gui;
        private IViewProvider m_viewProvider;

        public SubmitToDrawRenderWindow(float _aspectRatio, Vector2u _windowSize)
        {
            m_viewProvider = new ViewProviderBase();

            m_aspectRatio = _aspectRatio;
            m_renderWindow = RenderWindowFactory.CreateRenderWindow("", _windowSize);
            m_renderWindow.Resized += (_sender, _e) => Resize(new Vector2u(_e.Width, _e.Height));

            m_scene = new Scene();

            m_gui = new Gui(m_renderWindow);

            Resize(_windowSize);
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

            m_sceneRenderTexture?.Dispose();

            uint adjustedWidth = (uint)Math.Round(viewPort.Width * _windowSize.X);
            uint adjustedHeight = (uint)Math.Round(viewPort.Height * _windowSize.Y);

            m_sceneRenderTexture = new RenderTexture(adjustedWidth, adjustedHeight);
            m_sceneRenderTexture.SetView(renderWindowView);

            m_gui.View = renderWindowView;
        }

        public void Tick(TimeSpan _elapsed)
        {
            Draw();
        }

        private void Draw()
        {
            m_renderWindow.DispatchEvents();
            
            m_sceneRenderTexture.Clear();

            m_sceneRenderTexture.SetView(m_viewProvider.GetView());

            m_scene.Draw(m_sceneRenderTexture, RenderStates.Default);

            m_sceneRenderTexture.Display();
            
            m_renderWindow.Clear();

            m_renderWindow.Draw(m_sceneRenderTexture.Texture, RenderStates.Default);

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