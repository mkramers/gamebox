using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class MultiDrawable<T> : IPositionDrawable where T : Transformable, Drawable
    {
        private Vector2 m_position;
        private readonly List<T> m_renderObjects;

        public MultiDrawable(List<T> _renderObjects)
        {
            m_renderObjects = _renderObjects;
        }

        protected MultiDrawable()
        {
            m_renderObjects = new List<T>();
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            foreach (T drawable in m_renderObjects)
            {
                drawable.Draw(_target, _states);
            }
        }

        public Vector2 GetPosition()
        {
            return m_position;
        }

        protected void AddRange(IEnumerable<T> _drawables)
        {
            m_renderObjects.AddRange(_drawables);
        }

        public void SetPosition(Vector2 _position)
        {
            Vector2 offset = m_position - _position;

            foreach (T drawable in m_renderObjects)
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
            foreach (T drawable in m_renderObjects)
            {
                drawable.Dispose();
            }

            m_renderObjects.Clear();
        }
    }
}