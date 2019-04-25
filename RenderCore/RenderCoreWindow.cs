using System.Collections.Generic;
using System.Diagnostics;
using SFML.Graphics;

namespace RenderCore
{
    public class RenderCoreWindow : RenderCoreWindowBase
    {
        private readonly List<IRenderable> m_renderables;

        public RenderCoreWindow(RenderWindow _renderWindow) : base(_renderWindow)
        {
            m_renderables = new List<IRenderable>();
        }

        public void AddBodyRepresentation(IRenderable _renderable)
        {
            Debug.Assert(_renderable!=null);

            m_renderables.Add(_renderable);
        }

        public override void DrawScene(RenderWindow _renderWindow)
        {
            _renderWindow.Clear(Color.Black);

            foreach (IRenderable renderable in m_renderables)
            {
                renderable.Draw(_renderWindow);
            }

            _renderWindow.Display();
        }
    }
}