﻿using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public abstract class ViewControllerBase : IViewController
    {
        private readonly View m_view;
        private readonly float m_windowRatio;

        protected ViewControllerBase(View _view, float _windowRatio)
        {
            m_view = _view;
            m_windowRatio = _windowRatio;
        }

        public View GetView()
        {
            return m_view;
        }

        public void SetParentSize(Vector2u _parentSize)
        {
            float width = _parentSize.X * m_windowRatio;
            float height = _parentSize.Y * m_windowRatio;

            Vector2f size = new Vector2f(width, height);

            m_view.Size = size;
            m_view.Viewport = new FloatRect(0.0f, 0, 1f, 1);
        }

        public abstract void Tick(TimeSpan _elapsed);

        protected void SetCenter(Vector2 _center)
        {
            m_view.Center = _center.GetVector2F();
        }
    }
}