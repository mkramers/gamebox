﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using SFML.Graphics;

namespace RenderCore
{
    public class RenderCoreWindow : RenderCoreWindowBase, IDisposable
    {
        private readonly BlockingCollection<IDrawable> m_drawables;
        private readonly IEnumerable<IRenderCoreWindowWidget> m_widgets;

        public RenderCoreWindow(RenderWindow _renderWindow, IEnumerable<IRenderCoreWindowWidget> _widgets) : base(
            _renderWindow)
        {
            m_drawables = new BlockingCollection<IDrawable>();
            m_widgets = _widgets;
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

        protected override void DrawScene(RenderWindow _renderWindow)
        {
            _renderWindow.Clear(Color.Green);

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
    }
}