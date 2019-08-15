using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly List<IWidgetProvider> m_widgetProviders;

        public SubmitToDrawRenderWindow(float _aspectRatio, Vector2u _windowSize)
        {
            m_widgetProviders = new List<IWidgetProvider>();
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

        public void AddWidgetProvider(IWidgetProvider _provider)
        {
            m_widgetProviders.Add(_provider);
        }

        private void Resize(Vector2u _windowSize)
        {
            float aspectRatio = m_aspectRatio;

            View renderWindowView = m_renderWindow.GetView();

            FloatRect viewPort = WindowResizeUtilities.GetViewPort(_windowSize, aspectRatio);
            renderWindowView.Viewport = viewPort;

            m_renderWindow.SetView(renderWindowView);

            uint adjustedWidth = (uint)Math.Round(viewPort.Width * _windowSize.X);
            uint adjustedHeight = (uint)Math.Round(viewPort.Height * _windowSize.Y);

            m_sceneTexture.SetSize(adjustedWidth, adjustedHeight);

            m_gui.View = renderWindowView;
        }

        private void Draw()
        {
            m_renderWindow.DispatchEvents();

            m_renderWindow.Clear();

            Texture sceneTexture = m_sceneTexture.RenderToTexture(m_scene);

            m_renderWindow.Draw(sceneTexture, RenderStates.Default);

            IEnumerable<TGUI.Widget> allWidgets = m_widgetProviders.SelectMany(_widgetProvider => _widgetProvider.GetWidgets());
            m_gui.UpdateCurrentWidgets(allWidgets);

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