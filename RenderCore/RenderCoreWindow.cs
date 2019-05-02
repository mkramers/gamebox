using System;
using System.Collections.Generic;
using System.Diagnostics;
using SFML.Graphics;

namespace RenderCore
{
    public class RenderCoreWindow : RenderCoreWindowBase, IDisposable
    {
        private readonly List<Drawable> m_drawables;
        private readonly object m_drawLock;
        private readonly IEnumerable<IRenderCoreWindowWidget> m_widgets;

        public RenderCoreWindow(RenderWindow _renderWindow, IEnumerable<IRenderCoreWindowWidget> _widgets) : base(
            _renderWindow)
        {
            m_drawables = new List<Drawable>();
            m_widgets = _widgets;
            m_drawLock = new object();
        }

        public void Dispose()
        {
            foreach (IRenderCoreWindowWidget widget in m_widgets)
            {
                widget.Dispose();
            }

            m_renderWindow.Dispose();
        }

        public void Add(IDrawable _drawable)
        {
            Debug.Assert(_drawable != null);

            lock (m_drawLock)
            {
                m_drawables.Add(_drawable.GetDrawable());
            }
        }

        protected override void DrawScene(RenderWindow _renderWindow)
        {
            lock (m_drawLock)
            {
                _renderWindow.Clear(Color.Green);

                foreach (IRenderCoreWindowWidget widget in m_widgets)
                {
                    _renderWindow.Draw(widget);
                }

                foreach (Drawable shape in m_drawables)
                {
                    _renderWindow.Draw(shape);
                }

                _renderWindow.Display();
            }
        }
    }
}