using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RenderCore
{
    public class RenderCoreWindow : ITickable, IDisposable
    {
        private readonly IRenderCoreTarget m_sceneTarget;
        private readonly IRenderCoreTarget m_overlayTarget;
        private readonly RenderWindow m_renderWindow;
        private readonly BlockingCollection<ITickablePositionDrawable> m_viewWidgets;
        private IViewProvider m_viewProvider;

        public RenderCoreWindow(RenderWindow _renderWindow, IViewProvider _viewProvider)
        {
            m_renderWindow = _renderWindow;
            m_renderWindow.Closed += (_sender, _e) => m_renderWindow.Close();
            m_renderWindow.Resized += RenderWindowOnResized;

            m_viewWidgets = new BlockingCollection<ITickablePositionDrawable>();

            Vector2u windowSize = m_renderWindow.Size;

            SetViewProvider(_viewProvider);
            m_sceneTarget = new RenderCoreTarget(windowSize, Color.Black);
            m_overlayTarget = new RenderCoreTarget(windowSize, Color.Transparent);
        }

        public bool IsOpen => m_renderWindow.IsOpen;

        public void Dispose()
        {
            m_renderWindow.Dispose();

            m_sceneTarget.Dispose();
            m_overlayTarget.Dispose();
        }

        public void Tick(TimeSpan _elapsed)
        {
            if (!m_renderWindow.IsOpen)
            {
                return;
            }

            m_renderWindow.DispatchEvents();

            m_viewProvider.Tick(_elapsed);

            foreach (ITickablePositionDrawable widget in m_viewWidgets)
            {
                widget.Tick(_elapsed);
            }

            View view = m_viewProvider.GetView();
            m_sceneTarget.SetView(view);

            DrawScene(m_renderWindow);
        }

        private void RenderWindowOnResized(object _sender, SizeEventArgs _e)
        {
            Vector2u windowSize = new Vector2u(_e.Width, _e.Height);

            m_viewProvider.SetParentSize(windowSize);

            m_sceneTarget.SetSize(windowSize);
            m_overlayTarget.SetSize(windowSize);
        }

        public void AddToScene(IPositionDrawable _drawable)
        {
            Debug.Assert(_drawable != null);

            m_sceneTarget.AddDrawable(_drawable);
        }

        public void AddWidgetToScene(ITickablePositionDrawable _widget)
        {
            Debug.Assert(_widget != null);

            m_viewWidgets.Add(_widget);
            AddToScene(_widget);
        }

        private void DrawScene(RenderWindow _renderWindow)
        {
            _renderWindow.Clear();

            _renderWindow.Draw(m_sceneTarget);
            _renderWindow.Draw(m_overlayTarget);

            _renderWindow.Display();
        }

        public IViewProvider GetViewProvider()
        {
            return m_viewProvider;
        }

        public void SetViewProvider(IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;
            m_viewProvider.SetParentSize(m_renderWindow.Size);
        }
    }
}