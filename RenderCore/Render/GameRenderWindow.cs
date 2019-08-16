using System;
using System.Collections.Generic;
using System.Linq;
using Common.Tickable;
using RenderCore.Drawable;
using RenderCore.Widget;
using SFML.Graphics;
using SFML.System;

namespace RenderCore.Render
{
    public class GameRenderWindow : ITickable, IWidgetConsumer
    {
        private readonly IGui m_gui;
        private readonly IRenderWindow m_renderWindow;
        private readonly List<IWidgetProvider> m_widgetProviders;
        private ITextureProvider m_textureProvider;

        public GameRenderWindow(IRenderWindow _renderWindow, IGui _gui, Vector2u _windowSize)
        {
            m_widgetProviders = new List<IWidgetProvider>();

            m_renderWindow = _renderWindow;
            m_renderWindow.Resized += (_sender, _e) => Resize(new Vector2u(_e.Width, _e.Height));

            m_gui = _gui;

            Resize(_windowSize);
        }

        public void Tick(TimeSpan _elapsed)
        {
            try
            {
                Draw();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void AddWidgetProvider(IWidgetProvider _widgetProvider)
        {
            m_widgetProviders.Add(_widgetProvider);
        }

        public void RemoveWidgetProvider(IWidgetProvider _widgetProvider)
        {
            m_widgetProviders.Remove(_widgetProvider);
        }

        public void SetTextureProvider(ITextureProvider _textureProvider)
        {
            m_textureProvider = _textureProvider;

            View currentView = m_renderWindow.GetView();
            Vector2f size = currentView.Size;
            m_textureProvider?.SetSize((uint) Math.Round(size.X), (uint) Math.Round(size.Y));
        }

        private void Resize(Vector2u _windowSize)
        {
            FloatRect viewPort = WindowResizeUtilities.GetViewPort(_windowSize, 1.0f);

            m_renderWindow.SetViewport(viewPort);

            uint adjustedWidth = (uint) Math.Round(viewPort.Width * _windowSize.X);
            uint adjustedHeight = (uint) Math.Round(viewPort.Height * _windowSize.Y);

            m_textureProvider?.SetSize(adjustedWidth, adjustedHeight);
        }

        private void Draw()
        {
            Texture sceneTexture = m_textureProvider?.GetTexture();
            if (sceneTexture == null)
            {
                return;
            }

            Vector2f size = new Vector2f(sceneTexture.Size.X, sceneTexture.Size.Y);
            Vector2f center = size / 2.0f;

            View currentView = m_renderWindow.GetView();
            currentView.Size = size;
            currentView.Center = center;

            IGuiWidget[] allWidgets =
                m_widgetProviders.SelectMany(_widgetProvider => _widgetProvider.GetWidgets()).ToArray();
            foreach (IGuiWidget allWidget in allWidgets)
            {
                allWidget.OnViewChanged(currentView);
            }

            m_renderWindow.DispatchEvents();

            m_renderWindow.Clear(Color.Black);

            m_renderWindow.SetView(currentView);

            m_renderWindow.Draw(sceneTexture, RenderStates.Default);

            m_gui.UpdateCurrentWidgets(allWidgets);

            m_gui.SetView(currentView);

            m_gui.Draw();

            m_renderWindow.Display();
        }

        public event EventHandler Closed
        {
            add => m_renderWindow.Closed += value;
            remove => m_renderWindow.Closed -= value;
        }
    }
}