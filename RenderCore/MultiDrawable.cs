using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class MultiDrawable : List<Tuple<IDrawable, Matrix3x2>>, IDrawable
    {
        private Vector2 m_position;

        public MultiDrawable(IEnumerable<Tuple<IDrawable, Matrix3x2>> _children) : base(_children)
        {
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            foreach ((IDrawable drawable, Matrix3x2 _) in this)
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
            m_position = _position;

            foreach ((IDrawable drawable, Matrix3x2 transform) in this)
            {
                Vector2 relativePosition = transform.Translation;

                drawable.SetPosition(relativePosition + m_position);
            }
        }

        public void Dispose()
        {
            foreach ((IDrawable drawable, Matrix3x2 _) in this)
            {
                drawable.Dispose();
            }

            Clear();
        }
    }
}