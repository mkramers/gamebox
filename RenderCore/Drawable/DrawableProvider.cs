using System.Collections.Generic;

namespace RenderCore.Drawable
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
            return new[] {m_drawable};
        }
    }
}