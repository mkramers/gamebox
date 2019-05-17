﻿using System;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class TickableView : View, IViewProvider
    {
        private readonly float m_windowRatio;

        public TickableView(View _view, float _windowRatio) : base(_view)
        {
            m_windowRatio = _windowRatio;
        }

        public void SetParentSize(Vector2u _parentSize)
        {
            float width = _parentSize.X * m_windowRatio;
            float height = _parentSize.Y * m_windowRatio;

            Vector2f size = new Vector2f(width, height);

            Size = size;
            Viewport = new FloatRect(0.0f, 0, 1f, 1);
        }

        public virtual void Tick(TimeSpan _elapsed)
        {
        }

        public View GetView()
        {
            return this;
        }
    }
}