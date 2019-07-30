using System.Collections.Concurrent;
using System.Collections.Generic;
using Common.Containers;
using RenderCore.Drawable;
using SFML.Graphics;

namespace RenderCore.Render
{
    public class RenderObjectContainer : IRenderObjectContainer
    {
        private readonly List<IDrawable> m_drawables;

        public RenderObjectContainer()
        {
            m_drawables = new List<IDrawable>();
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

        public void RemoveDrawable(IDrawable _drawable)
        {
            m_drawables.Remove(_drawable);
        }
    }
}