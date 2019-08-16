using System.Numerics;
using LibExtensions.TGUI;
using SFML.Graphics;

namespace RenderCore.Widget
{
    public class GuiWidget : IGuiWidget
    {
        private readonly Vector2 m_relativePosition;
        private readonly TGUI.Widget m_widget;

        public GuiWidget(TGUI.Widget _widget, Vector2 _relativePosition)
        {
            m_widget = _widget;
            m_relativePosition = _relativePosition;
        }

        public void OnViewChanged(View _view)
        {
            Vector2 position = _view.GetAbsolutePosition(m_relativePosition);

            m_widget.SetPosition(position);
        }

        public TGUI.Widget GetWidget()
        {
            return m_widget;
        }
    }
}