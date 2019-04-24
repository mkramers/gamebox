using System.Collections.Generic;
using System.Diagnostics;
using SFML.Graphics;

namespace RenderCore
{
    public class RenderCoreWindow : RenderCoreWindowBase
    {
        private readonly List<IBodyRepresentation> m_bodyRepresentations;

        public RenderCoreWindow(RenderWindow _renderWindow) : base(_renderWindow)
        {
            m_bodyRepresentations = new List<IBodyRepresentation>();
        }

        public void AddBodyRepresentation(IBodyRepresentation _bodyRepresentation)
        {
            Debug.Assert(_bodyRepresentation!=null);

            m_bodyRepresentations.Add(_bodyRepresentation);
        }

        public override void DrawScene(RenderWindow _renderWindow)
        {
            _renderWindow.Clear(Color.Black);

            foreach (IBodyRepresentation bodyRepresentation in m_bodyRepresentations)
            {
                bodyRepresentation.Draw(_renderWindow);
            }

            _renderWindow.Display();
        }
    }
}