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
        private readonly BlockingCollection<IDrawable> m_drawables;
        private readonly RenderWindow m_renderWindow;
        private readonly BlockingCollection<IRenderCoreWindowWidget> m_widgets;
        private IViewController m_viewController;

        public RenderCoreWindow(RenderWindow _renderWindow)
        {
            m_renderWindow = _renderWindow;
            m_renderWindow.Closed += (_sender, _e) => m_renderWindow.Close();
            m_renderWindow.Resized += RenderWindowOnResized;

            m_drawables = new BlockingCollection<IDrawable>();
            m_widgets = new BlockingCollection<IRenderCoreWindowWidget>();

            m_viewController = new StaticViewControllerBase(m_renderWindow.GetView(), 0.03f);
        }

        public bool IsOpen => m_renderWindow.IsOpen;

        public void Dispose()
        {
            foreach (IRenderCoreWindowWidget widget in m_widgets)
            {
                widget.Dispose();
            }

            foreach (IDrawable drawable in m_drawables)
            {
                drawable.Dispose();
            }

            m_renderWindow.Dispose();
        }

        public void Tick(TimeSpan _elapsed)
        {
            if (!m_renderWindow.IsOpen)
            {
                return;
            }

            m_renderWindow.DispatchEvents();

            m_viewController.Tick(_elapsed);

            View view = m_viewController.GetView();

            foreach (IRenderCoreWindowWidget widget in m_widgets)
            {
                widget.SetView(view);
            }

            m_renderWindow.SetView(view);

            DrawScene(m_renderWindow);
        }

        public void Add(IDrawable _drawable)
        {
            Debug.Assert(_drawable != null);

            m_drawables.Add(_drawable);
        }

        public void AddWidget(IRenderCoreWindowWidget _widget)
        {
            Debug.Assert(_widget != null);

            m_widgets.Add(_widget);
            Add(_widget);
        }

        public void SetViewController(IViewController _viewController)
        {
            m_viewController = _viewController;
        }

        private void RenderWindowOnResized(object _sender, SizeEventArgs _e)
        {
            Vector2u windowSize = new Vector2u(_e.Width, _e.Height);
            m_viewController.SetParentSize(windowSize);
        }

        private void DrawScene(RenderWindow _renderWindow)
        {
            _renderWindow.Clear(Color.Black);

            foreach (IDrawable drawable in m_drawables)
            {
                _renderWindow.Draw(drawable);
            }

            _renderWindow.Display();
        }
    }
}