using System.Collections.Generic;
using RenderCore.Drawable;

namespace RenderCore.Render
{
    public class DrawableProvider : IDrawableProvider
    {
        private readonly IDrawable m_drawable;

        public DrawableProvider(IDrawable _drawable)
        {
            m_drawable = _drawable;
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            return new[] { m_drawable };
        }
    }
}