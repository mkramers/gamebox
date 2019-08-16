using System;
using System.Collections.Generic;
using System.Linq;
using Common.Tickable;
using RenderCore.Drawable;
using SFML.Graphics;
using SFML.System;
using TGUI;

namespace RenderCore.Render
{
    public class GameRenderWindow : ITickable
    {
        private readonly float m_aspectRatio;
        private readonly Gui m_gui;
        private readonly RenderWindow m_renderWindow;
        private readonly List<IWidgetProvider> m_widgetProviders;
        private ITextureProvider m_textureProvider;

        public GameRenderWindow(float _aspectRatio, Vector2u _windowSize)
        {
            m_widgetProviders = new List<IWidgetProvider>();

            m_aspectRatio = _aspectRatio;
            m_renderWindow = RenderWindowFactory.CreateRenderWindow("", _windowSize);
            m_renderWindow.Resized += (_sender, _e) => Resize(new Vector2u(_e.Width, _e.Height));

            m_gui = new Gui(m_renderWindow);

            Resize(_windowSize);
        }

        public void Tick(TimeSpan _elapsed)
        {
            Draw();
        }

        public void AddWidgetProvider(IWidgetProvider _provider)
        {
            m_widgetProviders.Add(_provider);
        }

        public void SetTextureProvider(ITextureProvider _textureProvider)
        {
            m_textureProvider = _textureProvider;

            View currentView = m_renderWindow.GetView();
            Vector2f size = currentView.Size;
            m_textureProvider?.SetSize((uint)Math.Round(size.X), (uint)Math.Round(size.Y));
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

            m_textureProvider?.SetSize(adjustedWidth, adjustedHeight);

            m_gui.View = renderWindowView;
        }

        private void Draw()
        {
            Texture sceneTexture = m_textureProvider.GetTexture();

            Vector2f size = new Vector2f(sceneTexture.Size.X, sceneTexture.Size.Y);
            Vector2f center = size / 2.0f;

            View currentView = m_renderWindow.GetView();
            currentView.Size = size;
            currentView.Center = center;

            IEnumerable<TGUI.Widget> allWidgets = m_widgetProviders.SelectMany(_widgetProvider => _widgetProvider.GetWidgets());
            m_gui.UpdateCurrentWidgets(allWidgets);

            m_renderWindow.DispatchEvents();

            m_renderWindow.Clear();

            m_renderWindow.SetView(currentView);

            m_renderWindow.Draw(sceneTexture, RenderStates.Default);
            
            m_gui.View = currentView;

            m_gui.Draw();

            m_renderWindow.Display();
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