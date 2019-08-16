using System.Collections.Generic;
using RenderCore.Widget;
using SFML.Graphics;
using TGUI;

namespace RenderCore.Render
{
    public class GuiWrapper : IGui
    {
        private readonly Gui m_gui;
        private readonly List<IGuiWidget> m_guiWidgets;

        public GuiWrapper(RenderWindow _renderWindow)
        {
            m_gui = new Gui(_renderWindow);
            m_guiWidgets = new List<IGuiWidget>();
        }

        public void Add(IGuiWidget _widget)
        {
            m_guiWidgets.Add(_widget);
            m_gui.Add(_widget.GetWidget());
        }

        public void Remove(IGuiWidget _widget)
        {
            m_guiWidgets.Remove(_widget);
            m_gui.Remove(_widget.GetWidget());
        }

        public void SetView(View _view)
        {
            m_gui.View = _view;
        }

        public void Draw()
        {
            m_gui.Draw();
        }

        public IEnumerable<IGuiWidget> GetWidgets()
        {
            return m_guiWidgets;
        }
    }
}