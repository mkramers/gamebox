using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RenderCore
{
    public class RenderCoreWindowTargets : IDrawable
    {
        private RenderTexture m_sceneRenderTarget;
        private RenderTexture m_overlayRenderTarget;

        public RenderCoreWindowTargets(Vector2u _size)
        {
            SetSize(_size);
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

    public class RenderCoreWindow : ITickable, IDisposable
    {
        private readonly BlockingCollection<IDrawable> m_drawables;
        private readonly RenderWindow m_renderWindow;
        private readonly BlockingCollection<IRenderCoreWidget> m_viewWidgets;
        private IViewProvider m_viewProvider;
        private readonly RenderCoreWindowTargets m_renderCoreWindowTargets;

        public RenderCoreWindow(RenderWindow _renderWindow, IViewProvider _viewProvider)
        {
            m_renderWindow = _renderWindow;
            m_renderWindow.Closed += (_sender, _e) => m_renderWindow.Close();
            m_renderWindow.Resized += RenderWindowOnResized;

            m_drawables = new BlockingCollection<IDrawable>();
            m_viewWidgets = new BlockingCollection<IRenderCoreWidget>();

            Vector2u windowSize = m_renderWindow.Size;

            SetViewProvider(_viewProvider);
            m_renderCoreWindowTargets = new RenderCoreWindowTargets(windowSize);
        }

        private void RenderWindowOnResized(object _sender, SizeEventArgs _e)
        {
            Vector2u windowSize = new Vector2u(_e.Width, _e.Height);

            m_viewProvider.SetParentSize(windowSize);

            m_renderCoreWindowTargets.SetSize(windowSize);
        }

        public bool IsOpen => m_renderWindow.IsOpen;

        public void Dispose()
        {
            foreach (IRenderCoreWidget widget in m_viewWidgets)
            {
                widget.Dispose();
            }

            foreach (IDrawable drawable in m_drawables)
            {
                drawable.Dispose();
            }

            m_renderWindow.Dispose();

            m_renderCoreWindowTargets.Dispose();
        }

        public void Tick(TimeSpan _elapsed)
        {
            if (!m_renderWindow.IsOpen)
            {
                return;
            }

            m_renderWindow.DispatchEvents();

            m_viewProvider.Tick(_elapsed);

            foreach (IRenderCoreWidget widget in m_viewWidgets)
            {
                widget.Tick(_elapsed);
            }

            View view = m_viewProvider.GetView();
            m_renderCoreWindowTargets.SetSceneView(view);

            DrawScene(m_renderWindow);
        }

        public void AddDrawable(IDrawable _drawable)
        {
            Debug.Assert(_drawable != null);

            m_drawables.Add(_drawable);
        }

        public void AddViewWidget(IRenderCoreWidget _widget)
        {
            Debug.Assert(_widget != null);

            m_viewWidgets.Add(_widget);
            AddDrawable(_widget);
        }

        private void DrawScene(RenderWindow _renderWindow)
        {
            Color clearColor = Color.Black;

            m_renderCoreWindowTargets.Clear(clearColor);

            foreach (IDrawable drawable in m_drawables)
            {
                m_renderCoreWindowTargets.DrawToScene(drawable);
            }

            m_renderCoreWindowTargets.Display();

            _renderWindow.Clear();

            _renderWindow.Draw(m_renderCoreWindowTargets);

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