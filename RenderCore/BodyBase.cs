using SFML.Graphics;

namespace RenderCore
{
    public abstract class BodyBase : IBody
    {
        private readonly Drawable m_drawable;

        protected BodyBase(Drawable _drawable)
        {
            m_drawable = _drawable;
        }

        public Drawable GetDrawable()
        {
            return m_drawable;
        }
    }
}