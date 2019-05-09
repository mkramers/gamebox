using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SFML.Graphics;

namespace RenderCore
{
    public class RenderCoreWindow : RenderCoreWindowBase, IDisposable
    {
        private readonly BlockingCollection<IDrawable> m_drawables;
        private readonly BlockingCollection<IRenderCoreWindowWidget> m_widgets;

        public RenderCoreWindow(RenderWindow _renderWindow) : base(_renderWindow)
        {
            m_drawables = new BlockingCollection<IDrawable>();
            m_widgets = new BlockingCollection<IRenderCoreWindowWidget>();
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

            m_drawables.Add(_drawable);
        }

        public void AddWidget(IRenderCoreWindowWidget _widget)
        {
            Debug.Assert(_widget != null);

            m_widgets.Add(_widget);
        }

        protected override void DrawScene(RenderWindow _renderWindow)
        {
            _renderWindow.Clear(Color.Black);

            foreach (IRenderCoreWindowWidget widget in m_widgets)
            {
                _renderWindow.Draw(widget);
            }

            foreach (IDrawable shape in m_drawables)
            {
                _renderWindow.Draw(shape);
            }

            _renderWindow.Display();
        }

        public override void Tick(TimeSpan _elapsed)
        {
            View view = m_renderWindow.GetView();
            foreach (IRenderCoreWindowWidget widget in m_widgets)
            {
                widget.SetView(view);
                widget.Tick(_elapsed);
            }

            base.Tick(_elapsed);
        }
    }
}