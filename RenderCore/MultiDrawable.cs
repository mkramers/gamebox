using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class MultiDrawable<T> : List<Tuple<T, Matrix3x2>>, IDrawable where T : IDrawable
    {
        public MultiDrawable(IEnumerable<Tuple<T, Matrix3x2>> _children) : base(_children)
        {
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            foreach (Tuple<T, Matrix3x2> item in this)
            {
                T drawable = item.Item1;

                drawable.Draw(_target, _states);
            }
        }

        public void SetRenderPosition(Vector2 _position)
        {
            foreach (Tuple<T, Matrix3x2> item in this)
            {
                T drawable = item.Item1;
                Vector2 relativePosition = item.Item2.Translation;

                drawable.SetRenderPosition(relativePosition + _position);
            }
        }
    }
}