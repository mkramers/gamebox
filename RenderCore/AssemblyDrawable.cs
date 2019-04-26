using System.Collections.Generic;
using SFML.Graphics;

namespace RenderCore
{
    public class AssemblyDrawable : Drawable
    {
        private readonly IEnumerable<Drawable> m_drawables;

        public AssemblyDrawable(IEnumerable<Drawable> _drawables)
        {
            m_drawables = _drawables;
        }

        public void Draw(RenderTarget _target, RenderStates _state)
        {
            foreach (Drawable drawable in m_drawables)
            {
                _target.Draw(drawable, _state);
            }
        }
    }
}