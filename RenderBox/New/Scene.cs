using System.Collections.Generic;
using System.Linq;
using RenderCore.Drawable;
using SFML.Graphics;

namespace RenderBox.New
{
    public class Scene
    {
        private readonly List<IDrawableProvider> m_drawableProviders;

        public Scene()
        {
            m_drawableProviders = new List<IDrawableProvider>();
        }

        public void AddDrawableProvider(IDrawableProvider _provider)
        {
            m_drawableProviders.Add(_provider);
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            IEnumerable<IDrawable> drawables = m_drawableProviders.SelectMany(_provider => _provider.GetDrawables());
            foreach (IDrawable drawable in drawables)
            {
                _target.Draw(drawable, _states);
            }
        }
    }
}