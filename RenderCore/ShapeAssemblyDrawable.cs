using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace RenderCore
{
    public class ShapeAssemblyDrawable : Transformable, Drawable, IDisposable
    {
        private readonly IEnumerable<Shape> m_shapes;

        public ShapeAssemblyDrawable(IEnumerable<Shape> _shapes)
        {
            m_shapes = _shapes;
        }

        public void Draw(RenderTarget _target, RenderStates _state)
        {
            foreach (Shape shape in m_shapes)
            {
                _target.Draw(shape, _state);
            }
        }

        public new void Dispose()
        {
            base.Dispose();

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool _disposing)
        {
            if (_disposing)
            {
                foreach (Shape shape in m_shapes)
                {
                    shape.Dispose();
                }
            }
        }
    }
}