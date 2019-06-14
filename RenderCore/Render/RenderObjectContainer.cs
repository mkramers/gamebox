using System.Collections.Concurrent;
using Common.Containers;
using RenderCore.Drawable;
using SFML.Graphics;

namespace RenderCore.Render
{
    public class RenderObjectContainer : IRenderObjectContainer
    {
        private readonly BlockingCollection<IDrawable> m_drawables;

        public RenderObjectContainer()
        {
            m_drawables = new BlockingCollection<IDrawable>();
        }

        public void AddDrawable(IDrawable _drawable)
        {
            m_drawables.Add(_drawable);
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            foreach (IDrawable drawable in m_drawables)
            {
                _target.Draw(drawable, _states);
            }
        }

        public void Dispose()
        {
            foreach (IDrawable drawable in m_drawables)
            {
                drawable.Dispose();
            }

            m_drawables.Clear();
        }
    }
}