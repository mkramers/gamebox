﻿using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class MultiDrawable<T> : List<T>, IPositionDrawable where T : Transformable, Drawable
    {
        private Vector2 m_position;

        public MultiDrawable(IEnumerable<T> _renderObjects) : base(_renderObjects)
        {
        }

        protected MultiDrawable()
        {
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            foreach (T drawable in this)
            {
                drawable.Draw(_target, _states);
            }
        }

        public Vector2 GetPosition()
        {
            return m_position;
        }

        public void SetPosition(Vector2 _position)
        {
            Vector2 offset = m_position - _position;

            foreach (T drawable in this)
            {
                Vector2 position = drawable.Position.GetVector2() + offset;
                drawable.Position = position.GetVector2F();
            }

            m_position = _position;
        }

        public void Dispose()
        {
            DisposeItemsAndClear();
        }

        protected void DisposeItemsAndClear()
        {
            foreach (T drawable in this)
            {
                drawable.Dispose();
            }

            Clear();
        }
    }
}