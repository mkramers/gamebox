using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class MultiDrawable<T> : List<Drawable<T>>, IDrawable where T : Transformable, Drawable
    {
        private Vector2 m_position;

        public MultiDrawable(IEnumerable<Drawable<T>> _children) : base(_children)
        {
        }

        protected MultiDrawable()
        {
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            foreach (IDrawable drawable in this)
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

            foreach (Drawable<T> drawable in this)
            {
                Vector2 position = drawable.GetPosition() + offset;
                drawable.SetPosition(position);
            }

            m_position = _position;
        }

        public void Dispose()
        {
            DisposeItemsAndClear();
        }

        public void DisposeItemsAndClear()
        {
            foreach (IDrawable drawable in this)
            {
                drawable.Dispose();
            }

            Clear();
        }
    }
}