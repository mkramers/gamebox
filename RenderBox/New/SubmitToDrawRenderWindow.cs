using System;
using System.Numerics;
using Common.Tickable;
using LibExtensions;
using RenderCore.Font;
using RenderCore.Render;
using RenderCore.ViewProvider;
using RenderCore.Widget;
using SFML.Graphics;
using SFML.System;
using TGUI;

namespace RenderBox.New
{
    public class SubmitToDrawRenderWindow : ITickable
    {
        private readonly float m_aspectRatio;
        private readonly RenderWindow m_renderWindow;
        private readonly Scene m_scene;
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

        public void InvokeGui(Action<Gui> _guiAction)
        {
            _guiAction(m_gui);
        }

        private void Resize(Vector2u _windowSize)
        {
            float aspectRatio = m_aspectRatio;

            View renderWindowView = m_renderWindow.GetView();
            renderWindowView.Viewport = WindowResizeUtilities.GetViewPort(_windowSize, aspectRatio);

            m_renderWindow.SetView(renderWindowView);
            m_gui.View = renderWindowView;
        }

        public void Tick(TimeSpan _elapsed)
        {
            Draw();
        }

        private void Draw()
        {
            m_renderWindow.DispatchEvents();

            m_renderWindow.Clear();

            m_renderWindow.SetView(m_viewProvider);

            m_scene.Draw(m_renderWindow, RenderStates.Default);

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