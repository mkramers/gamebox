using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class MultiDrawable : List<Tuple<IDrawable, Matrix3x2>>, IDrawable
    {
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

        public void SetRenderPosition(Vector2 _position)
        {
            foreach ((IDrawable drawable, Matrix3x2 transform) in this)
            {
                Vector2 relativePosition = transform.Translation;

                drawable.SetRenderPosition(relativePosition + _position);
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